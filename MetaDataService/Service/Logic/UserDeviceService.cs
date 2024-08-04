using AutoMapper;
using AutoWrapper.Wrappers;
using Contracts;
using Entities.DataModels;
using Entities.Handlers;
using Shared.DataTransferObjects.UserDevice;

namespace Service.Logic
{
    internal sealed class UserDeviceService : IUserDeviceService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UserDeviceService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserDeviceDto> Create_Device(int userId, UserDeviceForCreationDto userDeviceForCreation)
        {
            var userDeviceDb = _mapper.Map<UserDevice>(userDeviceForCreation);
            userDeviceDb.UserId = userId;
            _repository.UserDevice.Create_Device(userDeviceDb);
            await _repository.SaveAsync();
            var userDeviceDto = _mapper.Map<UserDeviceDto>(userDeviceDb);
            return userDeviceDto;
        }
        public async Task Delete_Device(int userId,int id, bool trackChanges)
        {
            var userDb = await CheckIfUserDeviceExists(userId,id, trackChanges);
            _repository.UserDevice.Delete_Device(userDb);
            await _repository.SaveAsync();
        }
        public async Task<UserDeviceDto> Get_Device_Async(int userId, int id, bool trackChanges)
        {
            var userDeviceDb = await CheckIfUserDeviceExists(userId, id, trackChanges);
            var userDeviceDto = _mapper.Map<UserDeviceDto>(userDeviceDb);
            return userDeviceDto;
        }
        public async Task<IEnumerable<UserDeviceDto>> Get_User_Devices_Async(int userId, bool trackChanges)
        {
            var userDevicesDb = await _repository.UserDevice.Get_User_Devices_Async(userId, trackChanges);
            var userDevicesDto = _mapper.Map<IEnumerable<UserDeviceDto>>(userDevicesDb);
            return userDevicesDto;
        }
        private async Task<UserDevice> CheckIfUserDeviceExists(int userId, int id, bool trackChanges)
        {
            var objDb = await _repository.UserDevice.Get_Device_Async(userId, id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"User Device doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }
    }
}

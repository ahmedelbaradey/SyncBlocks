using AutoMapper;
using AutoWrapper.Wrappers;
using Contracts;
using Entities.DataModels;
using Entities.Handlers;
using Shared.DataTransferObjects.UserDevice;
using Shared.DataTransferObjects.UserObject;

namespace Service.Logic
{
    internal sealed class UserObjectService : IUserObjectService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UserObjectService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserObjectDto> Create_UserObject(UserObjectForCreationDto userObjectForCreation)
        {
            var userObjectDb = _mapper.Map<UserObject>(userObjectForCreation);
            _repository.UserObject.Create_UserObject(userObjectDb);
            await _repository.SaveAsync();
            var userObjectDto = _mapper.Map<UserObjectDto>(userObjectDb);
            return userObjectDto;
        }
        public async Task Delete_UserObject(int userId,int id,bool trackChanges)
        {
            var userObjectDb = await CheckIfUserObjectExists(userId, id, trackChanges);
            _repository.UserObject.Delete_UserObject(userObjectDb);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<UserObjectDto>> Get_SharedObject_Users_Async(int sharedObject, bool trackChanges)
        {
            await CheckIfSharedObjectExists(sharedObject, trackChanges);
            var sharedObjectUsersDb = await _repository.UserObject.Get_SharedObject_Users_Async(sharedObject, trackChanges);
            var sharedObjectUsersDto = _mapper.Map<IEnumerable<UserObjectDto>>(sharedObjectUsersDb);
            return sharedObjectUsersDto;
        }
        public async Task<IEnumerable<UserObjectDto>> Get_User_SharedObjects_Async(int userId, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);
            var userSharedObjectDb = await _repository.UserObject.Get_User_SharedObjects_Async(userId, trackChanges);
            var userSharedObjectsDto = _mapper.Map<IEnumerable<UserObjectDto>>(userSharedObjectDb);
            return userSharedObjectsDto;
        }
        private async Task<UserObject> CheckIfUserObjectExists(int userId, int id, bool trackChanges)
        {
            var objDb = await _repository.UserObject.Get_User_SharedObject_Async(userId, id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"User Device doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }
        private async Task<SharedObject> CheckIfSharedObjectExists(int id, bool trackChanges)
        {
            var objDb = await _repository.SharedObject.Get_SharedObject_Async(id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"File Or Folder doesn't exist", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }
        private async Task<User> CheckIfUserExists(int id, bool trackChanges)
        {
            var userDb = await _repository.User.Get_User_Async(id, trackChanges);
            if (userDb is null)
                throw new ApiException(new GenericError($"User with id: {id} doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return userDb;
        }
    }
}

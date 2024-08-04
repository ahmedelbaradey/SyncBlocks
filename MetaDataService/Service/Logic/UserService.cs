using AutoMapper;
using AutoWrapper.Wrappers;
using Contracts;
using Entities.DataModels;
using Entities.Handlers;
using Entities.LinkModels;
using Service.Contracts;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.User;
using Shared.DataTransferObjects.UserDevice;
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service.Logic
{
    internal sealed class UserService : IUserService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UserService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserDto> Create_User(UserForCreationDto userForCreation)
        {
            var userDb = _mapper.Map<User>(userForCreation);
            _repository.User.Create_User(userDb);
            await _repository.SaveAsync();
            var userDto = _mapper.Map<UserDto>(userDb);
            return userDto;
        }
        public async Task Delete_User(int id, bool trackChanges)
        {
            var userDb = await CheckIfUserExists(id, trackChanges);
            _repository.User.Delete_User(userDb);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<UserDto>> Get_SharedObject_Users_Async(int sharedObjectId, bool trackChanges)
        {
            await CheckIfSharedObjectExists(sharedObjectId, trackChanges);
            var sharedObjectUsersDb = await _repository.User.Get_SharedObject_Users_Async(sharedObjectId, trackChanges);
            var sharedObjectUsersDto = _mapper.Map<IEnumerable<UserDto>>(sharedObjectUsersDb);
            return sharedObjectUsersDto;
        }
        public async Task<IEnumerable<SharedObjectDto>> Get_User_SharedObjects_Async(int userId, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);
            var userSharedObjectDb = await _repository.SharedObject.Get_User_SharedObjects_Async(userId, trackChanges);
            var userSharedObjectsDto = _mapper.Map<IEnumerable<SharedObjectDto>>(userSharedObjectDb);
            return userSharedObjectsDto;
        }
        public async Task<UserDto> Get_User_Async(int id, bool trackChanges)
        {
            var user = await CheckIfUserExists( id, trackChanges);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
        private async Task<User> CheckIfUserExists( int id, bool trackChanges)
        {
            var userDb = await _repository.User.Get_User_Async( id, trackChanges);
            if (userDb is null)
                throw new ApiException(new GenericError($"User with id: {id} doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return userDb;
        }
        private async Task<SharedObject> CheckIfSharedObjectExists(int id, bool trackChanges)
        {
            var objDb = await _repository.SharedObject.Get_SharedObject_Async(id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"File Or Folder doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }
    }
}

using AutoMapper;
using AutoWrapper.Wrappers;
using Contracts;
using Entities.DataModels;
using Entities.Handlers;
using Shared.DataTransferObjects.UserDevice;
using Shared.DataTransferObjects.UserObjectPermission;

namespace Service.Logic
{
    internal sealed class UserObjectPermissionService : IUserObjectPermissionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public UserObjectPermissionService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserObjectPermissionDto> Create_UserObject(UserObjectPermissionForCreationDto userObjectPermissionForCreation)
        {
            var userObjectPermissionDb = _mapper.Map<UserObjectPermission>(userObjectPermissionForCreation);
            _repository.UserObjectPermission.Create_UserObject(userObjectPermissionDb);
            await _repository.SaveAsync();
            var userObjectPermissionDto = _mapper.Map<UserObjectPermissionDto>(userObjectPermissionDb);
            return userObjectPermissionDto;
        }
        public async Task Delete_UserObjectPermission(int userId, int sharedObjectId, int id,bool trackChanges)
        {
            await CheckIfSharedObjectExists(sharedObjectId, trackChanges);
            await CheckIfUserExists(userId, trackChanges);
          var userObjectPermissionDb = await CheckIfUserObjectPermissionExists(id, trackChanges);
            _repository.UserObjectPermission.Delete_UserObject(userObjectPermissionDb);
            await _repository.SaveAsync();
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
        private async Task<UserObjectPermission> CheckIfUserObjectPermissionExists(int id, bool trackChanges)
        {
            var objDb = await _repository.UserObjectPermission.Get_User_SharedObject_Permission_Async(id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"User with id: {id} doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }
    }
}

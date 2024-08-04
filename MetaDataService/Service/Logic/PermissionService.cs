using AutoMapper;
using AutoWrapper.Wrappers;
using Contracts;
using Entities.DataModels;
using Entities.Handlers;
using Entities.LinkModels;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Permisson;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service.Logic
{
    internal sealed class PermissionService : IPermissionService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public PermissionService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task Create_Permission(PermissionForCreationDto permissionForCreation)
        {
            var permissionDto = _mapper.Map<Permission>(permissionForCreation);
            _repository.Permission.Create_Permission(permissionDto);
            await _repository.SaveAsync();
        }
        public async Task<IEnumerable<PermissionDto>> Get_Permissions_Async(bool trackChanges)
        {
            var permissionsDb = await _repository.Permission.Get_Permissions_Async(trackChanges);
            var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissionsDb);
            return permissionsDto;
        }
        public async Task<PermissionDto> Get_Permission_Async(int id, bool trackChanges)
        {
            var permissionDb = await _repository.Permission.Get_Permission_Async(id,trackChanges);
            var permissionDto = _mapper.Map<PermissionDto>(permissionDb);
            return permissionDto;
        }
        public async Task<IEnumerable<PermissionDto>> Get_User_SharedObject_Permissions_Async(int sharedObjectId, int userId, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);
            await CheckIfSharedObjectExists(sharedObjectId, trackChanges);
            var permissionsDb = await _repository.Permission.Get_User_SharedObject_Permissions_Async(sharedObjectId, userId,trackChanges);
            var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissionsDb);
            return permissionsDto;
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

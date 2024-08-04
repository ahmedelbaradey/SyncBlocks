using AutoMapper;
using AutoWrapper.Wrappers;
using Contracts;
using Entities.DataModels;
using Entities.Handlers;
using Entities.LinkModels;
using Service.Contracts;
using Service.Contracts.DataShaping;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Block;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.User;
using Shared.DataTransferObjects.UserObject;
using Shared.RequestFeatures;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace Service.Logic
{
    internal sealed class SharedObjectService : ISharedObjectService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public SharedObjectService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<SharedObjectDto> Create_SharedObject(int userId,string parentObject, bool trackChanges, SharedObjectForCreationDto sharedObjectForCreation)
        {
            var parentSharedObjectDb = await _repository.SharedObject.Get_SharedObject_Async(userId,parentObject, trackChanges);
            var sharedObjectDb = _mapper.Map<SharedObject>(sharedObjectForCreation);
            _repository.SharedObject.Create_SharedObject(parentSharedObjectDb.Id, sharedObjectDb);
            await _repository.SaveAsync();
            var sharedObjectDto = _mapper.Map<SharedObjectDto>(sharedObjectDb);
            return sharedObjectDto;
        }
        public async Task Delete_SharedObject(int id, bool trackChanges )
        {
            var userDb = await CheckIfSharedObjectExists(id, trackChanges);
            _repository.SharedObject.Delete_SharedObject(userDb);
            await _repository.SaveAsync();
        }
        public async Task<PagedList<SharedObjectDto>> Get_SharedObjects_Async(RequestParameters requestParameters, bool trackChanges)
        {
            var sharedObjectsDb = await _repository.SharedObject.Get_SharedObjects_Async(requestParameters, trackChanges);
            var sharedObjectsDto = _mapper.Map<IEnumerable<SharedObjectDto>>(sharedObjectsDb);
             return PagedList<SharedObjectDto>.ToPagedList(sharedObjectsDto, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public async Task<SharedObjectDto> Get_SharedObject_Async(int id, bool trackChanges)
        {
            var sharedObjectDb = await CheckIfSharedObjectExists(id, trackChanges);
            var sharedObjectDto = _mapper.Map<SharedObjectDto>(sharedObjectDb);
            return sharedObjectDto;
        }
        public async Task<IEnumerable<SharedObjectDto>> Get_User_SharedObjects_Async(int userId, bool trackChanges)
        {
            await CheckIfUserExists(userId, trackChanges);
            var userSharedObjectDb = await _repository.SharedObject.Get_User_SharedObjects_Async(userId, trackChanges);
            var userSharedObjectsDto = _mapper.Map<IEnumerable<SharedObjectDto>>(userSharedObjectDb);
            return userSharedObjectsDto;
        }
        private async Task<User> CheckIfUserExists(int id, bool trackChanges)
        {
            var userDb = await _repository.User.Get_User_Async(id, trackChanges);
            if (userDb is null)
                throw new ApiException(new GenericError($"User with id: {id} doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return userDb;
        }
        private async Task<SharedObject> CheckIfSharedObjectExists( int id, bool trackChanges)
        {
            var objDb = await _repository.SharedObject.Get_SharedObject_Async( id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"File Or Folder doesn't exist in the database.", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }

        public async Task<SharedObjectDto> Get_SharedObject_Async(int userId, string name, bool trackChanges)
        {
             await CheckIfUserExists(userId, trackChanges);
             var userSharedObjectDb = await _repository.SharedObject.Get_SharedObject_Async(userId, name, trackChanges);
            var userSharedObjectsDto = _mapper.Map<SharedObjectDto>(userSharedObjectDb);
            return userSharedObjectsDto;
        }
    }
}

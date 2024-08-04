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
using Shared.DataTransferObjects.Journal;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service.Logic
{
    internal sealed class JournalService : IJournalService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public JournalService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<PagedList<JournalDto>> Get_User_SharedObject_Journals_Async(int userId, int sharedObject, RequestParameters requestParameters, bool trackChanges)
        {
            var journalsDb = await _repository.Journal.Get_User_SharedObject_Journals_Async(userId, sharedObject, requestParameters, trackChanges);
            var journalsDto = _mapper.Map<IEnumerable<JournalDto>>(journalsDb);
            return PagedList<JournalDto>.ToPagedList(journalsDto, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public async Task<PagedList<JournalDto>> Get_SharedObject_Journals_Async(int sharedObject, RequestParameters requestParameters, bool trackChanges)
        {
            var journalsDb = await _repository.Journal.Get_SharedObject_Journals_Async(sharedObject, requestParameters, trackChanges);
            var journalsDto = _mapper.Map<IEnumerable<JournalDto>>(journalsDb);
            return PagedList<JournalDto>.ToPagedList(journalsDto, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public async Task<JournalDto> Get_Journal_Async(int id, bool trackChanges)
        {
            var journalDb = await CheckIfJournalExists(id, trackChanges);
            var journalDto = _mapper.Map<JournalDto>(journalDb);
            return journalDto;
        }
        public async Task<JournalDto> Create_Journal(JournalForCreationDto journalforCreation)
        {
            var journalDb = _mapper.Map<Journal>(journalforCreation);
            _repository.Journal.Create_Journal(journalDb);
            await _repository.SaveAsync();
            var JournaDto = _mapper.Map<JournalDto>(journalDb);
            return JournaDto;
        }
        private async Task<Journal> CheckIfJournalExists(int id, bool trackChanges)
        {
            var journalDb = await _repository.Journal.Get_Journal_Async(id, trackChanges);
            if (journalDb is null)
                throw new ApiException(new GenericError($"Change not Exist", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return journalDb;
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

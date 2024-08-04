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
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service.Logic
{
    internal sealed class BlockService : IBlockService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public BlockService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<BlockDto> Create_Block(int sharedObjectId, BlockForCreationDto blockForCreation, bool trackChanges)
        {
            await CheckIfSharedObjectExists(sharedObjectId, trackChanges);
            var blockDb = _mapper.Map<Block>(blockForCreation);
            _repository.Block.Create_Block(blockDb);
            await _repository.SaveAsync();
            var blockDto = _mapper.Map<BlockDto>(blockDb);
            return blockDto;
        }
        public async Task Delete_SharedObject(int id, bool trackChanges )
        {
            var userDb = await CheckIfSharedObjectExists(id, trackChanges);
            _repository.SharedObject.Delete_SharedObject(userDb);
            await _repository.SaveAsync();
        }
        public async Task<BlockDto> Get_Block_Async(int id, bool trackChanges)
        {
            var blockDb = await CheckIfBlockExists(id, trackChanges);
            var blockDto = _mapper.Map<BlockDto>(blockDb);
            return blockDto;
        }
        public async Task<IEnumerable<BlockDto>> Get_SharedObject_AllBlocks_Async(int sharedObjectId, bool trackChanges)
        {
            await CheckIfSharedObjectExists(sharedObjectId, trackChanges);
            var blocksDb = await _repository.Block.Get_SharedObject_AllBlocks_Async(sharedObjectId, trackChanges);
            var blocksDto = _mapper.Map<IEnumerable<BlockDto>>(blocksDb);
            return blocksDto;
        }
        public async Task<IEnumerable<BlockDto>> Get_SharedObject_Journal_Blocks_Async(int sharedObjectId, int journalId, bool trackChanges)
        {
            await CheckIfJournalExists(journalId, trackChanges);
            var blocksDb = await _repository.Block.Get_SharedObject_Journal_Blocks_Async(sharedObjectId,journalId, trackChanges);
            var blocksDto = _mapper.Map<IEnumerable<BlockDto>>(blocksDb);
            return blocksDto;
        }
        public async Task<IEnumerable<BlockDto>> Get_SharedObject_Current_Journal_Blocks_Async(int sharedObjectId, bool trackChanges)
        {
            var currentJounal = await SharedObjectCurrentJourna(sharedObjectId, trackChanges);
            var currentBlocks= await _repository.Block.Get_SharedObject_Journal_Blocks_Async(sharedObjectId, currentJounal.Id, trackChanges);
            var blocksDto = _mapper.Map<IEnumerable<BlockDto>>(currentBlocks);
            return blocksDto;
        }
        private async Task<SharedObject> CheckIfSharedObjectExists( int id, bool trackChanges)
        {
            var objDb = await _repository.SharedObject.Get_SharedObject_Async( id, trackChanges);
            if (objDb is null)
                throw new ApiException(new GenericError($"File Or Folder doesn't exist", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return objDb;
        }
        private async Task<Block> CheckIfBlockExists(int id, bool trackChanges)
        {
            var blockDb = await _repository.Block.Get_Block_Async(id, trackChanges);
            if (blockDb is null)
                throw new ApiException(new GenericError($"File Corrupted", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return blockDb;
        }
        private async Task<Journal> CheckIfJournalExists(int id, bool trackChanges)
        {
            var journalDb = await _repository.Journal.Get_Journal_Async(id, trackChanges);
            if (journalDb is null)
                throw new ApiException(new GenericError($"Change not Exist", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return journalDb;
        }

        private async Task<Journal> SharedObjectCurrentJourna(int sharedObjectId, bool trackChanges)
        {
            var journalDb = await _repository.Journal.Get_SharedObject_Current_Journal_Async(sharedObjectId, trackChanges);
            if (journalDb is null)
                throw new ApiException(new GenericError($"Change not Exist", "NO DATA", Guid.NewGuid(), DateTime.Now, false), 400);
            return journalDb;
        }
    }
}

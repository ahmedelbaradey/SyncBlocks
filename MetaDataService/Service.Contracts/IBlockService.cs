using Entities.DataModels;
using Entities.Handlers;
using Entities.LinkModels;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Block;


namespace Service.Contracts
{
    public interface IBlockService
    {
        Task<IEnumerable<BlockDto>> Get_SharedObject_AllBlocks_Async(int sharedObjectId, bool trackChanges);
        Task<IEnumerable<BlockDto>> Get_SharedObject_Journal_Blocks_Async(int sharedObjectId, int journalId, bool trackChanges);
        Task<IEnumerable<BlockDto>> Get_SharedObject_Current_Journal_Blocks_Async(int sharedObjectId,bool trackChanges);
        Task<BlockDto> Get_Block_Async(int id, bool trackChanges);
        Task<BlockDto> Create_Block(int sharedObjectId,  BlockForCreationDto blockForCreation, bool trackChanges);
    }
}

using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IBlockRepository
    {
        Task<IEnumerable<Block>> Get_SharedObject_AllBlocks_Async(int sharedObjectId,bool trackChanges);
        Task<IEnumerable<Block>> Get_SharedObject_Journal_Blocks_Async(int sharedObjectId, int journalId, bool trackChanges);
        Task<Block> Get_Block_Async(int id, bool trackChanges);
        void Create_Block(Block block);
    }
}

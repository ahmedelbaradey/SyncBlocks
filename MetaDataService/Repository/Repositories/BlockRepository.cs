using Contracts;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Repository.Repositories
{
    public class BlockRepository : RepositoryBase<Block>, IBlockRepository
    {
        public BlockRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
        public async Task<IEnumerable<Block>> Get_SharedObject_AllBlocks_Async(int sharedObjectId, bool trackChanges) => await FindByCondition(c => c.Journal.SharedObject.Id.Equals(sharedObjectId), trackChanges).ToListAsync();
        public async Task<IEnumerable<Block>> Get_SharedObject_Journal_Blocks_Async(int sharedObjectId, int journalId, bool trackChanges) => await FindByCondition(c =>c.Journal.SharedObject.Id.Equals(sharedObjectId) && c.JournalId.Equals(journalId), trackChanges).ToListAsync();
        public async Task<Block> Get_Block_Async(int blockId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(blockId), trackChanges).FirstAsync();
        public void Create_Block(Block block) => Create(block);
    }
}

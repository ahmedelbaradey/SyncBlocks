using Contracts;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;


namespace Repository.Repositories
{
    public class JournalRepository : RepositoryBase<Journal>, IJournalRepository
    {
        public JournalRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
         public async  Task<IEnumerable<Journal>> Get_SharedObject_Journals_Async(int sharedObject, RequestParameters requestParameters, bool trackChanges)
        {
            var companies = await FindAll(trackChanges).Sort(requestParameters.OrderBy).Page(requestParameters.PageSize, requestParameters.PageNumber).ToListAsync();
            var count = await FindByCondition(c => c.SharedObjectId.Equals(sharedObject), trackChanges).CountAsync();
            return new PagedList<Journal>(companies, count, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public async  Task<IEnumerable<Journal>> Get_User_SharedObject_Journals_Async( int userId, int sharedObject, RequestParameters requestParameters, bool trackChanges)
        {
                var companies = await FindAll(trackChanges).Sort(requestParameters.OrderBy).Page(requestParameters.PageSize, requestParameters.PageNumber).ToListAsync();
                var count = await FindByCondition(c => c.SharedObjectId.Equals(sharedObject) && c.UserId.Equals(userId), trackChanges).CountAsync();
                return new PagedList<Journal>(companies, count, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public async Task<Journal> Get_Journal_Async(int journalId, bool trackChanges) => await FindByCondition(c => c.Id.Equals(journalId), trackChanges).FirstAsync();
        public async Task<Journal> Get_SharedObject_Current_Journal_Async(int sharedObject, bool trackChanges) => await FindByCondition(c => c.SharedObjectId.Equals(sharedObject), trackChanges).OrderByDescending(c=>c.Timestamp).FirstAsync();
        
        public void Create_Journal(Journal journal) => Create(journal);
    }
}

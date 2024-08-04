using Contracts.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Repository.Repositories
{
    public class SharedObjectRepository : RepositoryBase<SharedObject>,ISharedObjectRepository
    {
        public SharedObjectRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {        
        }
        public void Create_SharedObject(int? parentObjectId,SharedObject sharedObject) 
        { 
            sharedObject.ParentObjectId = parentObjectId;
            Create(sharedObject); 
        }
        public void Delete_SharedObject(SharedObject sharedObject) =>Delete(sharedObject);
        public async Task<PagedList<SharedObject>> Get_SharedObjects_Async(RequestParameters requestParameters, bool trackChanges)
        {
            var sharedObjects = await FindAll(trackChanges).Sort(requestParameters.OrderBy).Page(requestParameters.PageSize, requestParameters.PageNumber).ToListAsync();
            var count = await FindAll(trackChanges).CountAsync();
            return new PagedList<SharedObject>(sharedObjects, count, requestParameters.PageNumber, requestParameters.PageSize);
        }
        public async Task<SharedObject> Get_SharedObject_Async(int sharedObjectId, bool trackChanges)=>await FindByCondition(c=>c.Id.Equals(sharedObjectId), trackChanges).FirstAsync();
        public async Task<SharedObject> Get_SharedObject_Async( int userId, string name, bool trackChanges) => await FindByCondition(c => c.UserObjects.Any(x => x.SharedObject.Name.Equals(name) &&  x.UserId.Equals(userId) && x.SharedObjectId.Equals(c.Id)), trackChanges).FirstAsync();
        public async Task<IEnumerable<SharedObject>> Get_User_SharedObjects_Async(int userId, bool trackChanges) => await FindByCondition(c => c.UserObjects.Any(x => x.UserId.Equals(userId) && x.SharedObjectId.Equals(c.Id)), trackChanges).ToListAsync();
    }
}

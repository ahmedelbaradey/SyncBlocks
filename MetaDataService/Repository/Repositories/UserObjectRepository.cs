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
    public class UserObjectRepository : RepositoryBase<UserObject>, IUserObjectRepository
    {
        public UserObjectRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
        public async Task<IEnumerable<UserObject>> Get_SharedObject_Users_Async(int sharedObject, bool trackChanges) => await FindByCondition(c => c.SharedObjectId.Equals(sharedObject), trackChanges).ToListAsync();
        public async Task<IEnumerable<UserObject>> Get_User_SharedObjects_Async(int userId, bool trackChanges) =>await FindByCondition(c => c.UserId.Equals(userId), trackChanges).ToListAsync();

        public async Task<UserObject> Get_User_SharedObject_Async(int userId,int id, bool trackChanges) => await FindByCondition(c => c.UserId.Equals(userId) && c.Id.Equals(id), trackChanges).FirstAsync();
        public void Create_UserObject(UserObject userObject) => Create(userObject);
        public void Delete_UserObject(UserObject userObject)=> Delete(userObject);

    }
}

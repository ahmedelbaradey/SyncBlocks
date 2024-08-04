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
    public class UserObjectPermissionRepository : RepositoryBase<UserObjectPermission>, IUserObjectPermissionRepository
    {
        public UserObjectPermissionRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
        public async Task<IEnumerable<UserObjectPermission>> Get_User_SharedObject_Permissions_Async(int sharedObject,int userId, bool trackChanges) => await FindByCondition(c => c.UserObject.UserId.Equals(userId) && c.UserObject.SharedObjectId.Equals(sharedObject), trackChanges).ToListAsync();
        public async Task<UserObjectPermission> Get_User_SharedObject_Permission_Async(int id, bool trackChanges)=> await FindByCondition(c=>c.Id.Equals(id), trackChanges).FirstAsync();    
        public void Create_UserObject(UserObjectPermission userObjectPermission) => Create(userObjectPermission);
        public void Delete_UserObject(UserObjectPermission userObjectPermission) => Delete(userObjectPermission);

    }
}

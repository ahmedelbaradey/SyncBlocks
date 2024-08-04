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
    public class PermissionRepository : RepositoryBase<Permission>, IPermissionRepository
    {
        public PermissionRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
        public async Task<IEnumerable<Permission>> Get_Permissions_Async(bool trackChanges) => await FindAll(trackChanges).ToListAsync();
        public async Task<IEnumerable<Permission>> Get_User_SharedObject_Permissions_Async(int sharedObjectId, int userId, bool trackChanges) => await FindByCondition(c => c.UserObjectPermissions.Any(x=>x.UserObject.SharedObjectId.Equals(sharedObjectId) && x.UserObject.UserId.Equals(userId)) , trackChanges).ToListAsync();
        public async Task<Permission> Get_Permission_Async(int permissionId, bool trackChanges) => await FindByCondition(c=>c.Id.Equals(permissionId),trackChanges).FirstAsync();
        public void Create_Permission(Permission permission)=>Create(permission);
    }
}

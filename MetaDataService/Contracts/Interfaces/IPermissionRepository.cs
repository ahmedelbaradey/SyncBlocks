using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> Get_Permissions_Async(bool trackChanges);
        Task<IEnumerable<Permission>> Get_User_SharedObject_Permissions_Async(int sharedObjectId, int userId, bool trackChanges);
        Task<Permission> Get_Permission_Async(int permissionId, bool trackChanges);
        void Create_Permission(Permission permission);
    }
}

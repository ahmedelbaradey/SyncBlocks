using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUserObjectPermissionRepository
    {
        Task<IEnumerable<UserObjectPermission>> Get_User_SharedObject_Permissions_Async(int sharedObject, int userId, bool trackChanges);
        Task<UserObjectPermission> Get_User_SharedObject_Permission_Async(int id, bool trackChanges);
        void Create_UserObject(UserObjectPermission userObjectPermission);
        void Delete_UserObject(UserObjectPermission userObjectPermission);
    }
}

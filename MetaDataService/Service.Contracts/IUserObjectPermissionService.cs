using Entities.DataModels;
using Shared.DataTransferObjects.UserObjectPermission;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserObjectPermissionService
    {
        //Task<IEnumerable<UserObjectPermissionDto>> Get_User_SharedObject_Permissions_Async(int sharedObject, int userId, bool trackChanges);
        Task<UserObjectPermissionDto> Create_UserObject(UserObjectPermissionForCreationDto userObjectPermissionForCreation);
        Task Delete_UserObjectPermission(int userId,int sharedObjectId,int id, bool trackChanges);
    }
}

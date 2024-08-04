using Entities.DataModels;
using Shared.DataTransferObjects.Permisson;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IPermissionService
    {
        Task<IEnumerable<PermissionDto>> Get_Permissions_Async(bool trackChanges);
        Task<IEnumerable<PermissionDto>> Get_User_SharedObject_Permissions_Async(int sharedObjectId, int userId, bool trackChanges);
        Task<PermissionDto> Get_Permission_Async(int id, bool trackChanges);
        Task Create_Permission(PermissionForCreationDto permissionForCreation);
    }
}

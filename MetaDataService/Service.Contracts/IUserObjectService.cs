using Entities.DataModels;
using Shared.DataTransferObjects.UserObject;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserObjectService
    {
        Task<IEnumerable<UserObjectDto>> Get_SharedObject_Users_Async(int sharedObject, bool trackChanges);
        Task<IEnumerable<UserObjectDto>> Get_User_SharedObjects_Async(int userId, bool trackChanges);
        Task<UserObjectDto> Create_UserObject(UserObjectForCreationDto userObjectForCreation);
        Task Delete_UserObject(int userId,int id,bool trackChange);
    }
}

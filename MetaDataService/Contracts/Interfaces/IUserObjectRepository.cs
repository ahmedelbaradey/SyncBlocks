using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUserObjectRepository
    {
        Task<IEnumerable<UserObject>> Get_SharedObject_Users_Async(int sharedObject, bool trackChanges);
        Task<IEnumerable<UserObject>> Get_User_SharedObjects_Async(int userId, bool trackChanges);
        Task<UserObject> Get_User_SharedObject_Async(int userId,int id, bool trackChanges);
        void Create_UserObject(UserObject userObject);
        void Delete_UserObject(UserObject userObject);
    }
}

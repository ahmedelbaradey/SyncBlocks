using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Get_User_Async(int userId, bool trackChanges);
        Task<IEnumerable<User>> Get_SharedObject_Users_Async(int sharedobjectId, bool trackChanges);
        void Create_User(User user);
        void Delete_User(User user);
    }
}

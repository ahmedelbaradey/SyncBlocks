using Entities.DataModels;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.User;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> Get_SharedObject_Users_Async(int sharedObject, bool trackChanges);
        Task<UserDto> Get_User_Async(int id, bool trackChanges);
        Task<UserDto> Create_User(UserForCreationDto userForCreation);
        Task Delete_User(int id, bool trackChanges);
    }
}

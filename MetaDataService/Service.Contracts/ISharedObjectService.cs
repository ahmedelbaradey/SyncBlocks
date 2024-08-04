using Entities.DataModels;
using Shared.DataTransferObjects.SharedObject;
using Shared.DataTransferObjects.UserObject;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ISharedObjectService
    {

        Task<IEnumerable<SharedObjectDto>> Get_User_SharedObjects_Async(int userId, bool trackChanges); 
        Task<PagedList<SharedObjectDto>> Get_SharedObjects_Async(RequestParameters requestParameters, bool trackChanges);
        Task<SharedObjectDto> Get_SharedObject_Async(int id, bool trackChanges);
        Task<SharedObjectDto> Get_SharedObject_Async(int userId, string name, bool trackChanges);
        Task<SharedObjectDto> Create_SharedObject(int userId,string parentObject, bool trackChanges, SharedObjectForCreationDto sharedObjectForCreation);
        Task Delete_SharedObject(int id, bool trackChanges);
    }
}

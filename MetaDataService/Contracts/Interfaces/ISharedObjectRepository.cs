using Entities.DataModels;
using Shared.DataTransferObjects.SharedObject;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface ISharedObjectRepository
    {
        Task<PagedList<SharedObject>> Get_SharedObjects_Async(RequestParameters requestParameters, bool trackChanges);
        Task<IEnumerable<SharedObject>> Get_User_SharedObjects_Async(int userId, bool trackChanges);
        Task<SharedObject> Get_SharedObject_Async(int id, bool trackChanges);
        Task<SharedObject> Get_SharedObject_Async(int userId, string name, bool trackChanges);
        void Create_SharedObject(int? parentSharedObject, SharedObject sharedObject);
        void Delete_SharedObject(SharedObject sharedObject);
    }
}

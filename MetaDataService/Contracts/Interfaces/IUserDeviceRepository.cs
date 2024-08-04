using Entities.DataModels;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contracts.Interfaces
{
    public interface IUserDeviceRepository
    {
        Task<IEnumerable<UserDevice>> Get_User_Devices_Async(int userId, bool trackChanges);
        Task<UserDevice> Get_Device_Async(int userId,int id, bool trackChanges);
        void Create_Device(UserDevice device);
        void Delete_Device(UserDevice device);
    }

}

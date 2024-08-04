using Contracts.Interfaces;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Repository.Extensions;
using Shared.RequestFeatures;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace Repository.Repositories
{
    public class UserDeviceRepository : RepositoryBase<UserDevice>, IUserDeviceRepository
    {
        public UserDeviceRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
          
        }
        public async Task<UserDevice> Get_Device_Async(int userId ,int deviceId, bool trackChanges) => await FindByCondition(c =>c.UserId.Equals(userId) &&  c.Id.Equals(deviceId), trackChanges).FirstAsync();
        public async Task<IEnumerable<UserDevice>> Get_User_Devices_Async(int userId, bool trackChanges) => await FindByCondition(c => c.UserId.Equals(userId), trackChanges).ToListAsync();
        public void Create_Device(UserDevice device) =>   Create(device);
        public void Delete_Device(UserDevice device)=> Delete(device);  
    }
}

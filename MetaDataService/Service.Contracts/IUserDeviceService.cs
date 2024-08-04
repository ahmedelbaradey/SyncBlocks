using Entities.DataModels;
using Shared.DataTransferObjects.UserDevice;

namespace Contracts
{
    public interface IUserDeviceService
    {
        Task<IEnumerable<UserDeviceDto>> Get_User_Devices_Async(int userId, bool trackChanges);
        Task<UserDeviceDto> Get_Device_Async(int userId,int id, bool trackChanges);
        Task<UserDeviceDto> Create_Device(int userId, UserDeviceForCreationDto userDeviceForCreation);
        Task Delete_Device(int userId, int id, bool trackChanges);
    }
   
}

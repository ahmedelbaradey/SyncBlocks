using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; }
        IEmployeeService EmployeeService { get; }
        IPermissionService PermissionService { get; }
        IUserService UserService { get; }
        IUserDeviceService UserDeviceService { get; }
        ISharedObjectService SharedObjectService { get; }
        IJournalService JournalService { get; }
        IBlockService BlockService { get; }
        IUserObjectService UserObjectService { get; }
        IUserObjectPermissionService UserObjectPermissionService { get; }

    }
}

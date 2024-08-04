using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Interfaces;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IPermissionRepository Permission { get; }
        IUserRepository User { get; }
        IUserDeviceRepository UserDevice { get; }
        ISharedObjectRepository SharedObject { get; }
        IBlockRepository Block { get; }
        IJournalRepository Journal { get; }
        IUserObjectRepository UserObject { get; }
        IUserObjectPermissionRepository UserObjectPermission { get; }
        Task SaveAsync();
    }
}

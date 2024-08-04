using AutoMapper;
using Contracts;
using Service.Contracts;
using Service.Logic;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IPermissionService> _permissionService;
        private readonly Lazy<IUserService> _userService;
        private readonly Lazy<IUserDeviceService> _userDeviceService;
        private readonly Lazy<ISharedObjectService> _sharedObjectService;
        private readonly Lazy<IBlockService> _blockService;
        private readonly Lazy<IJournalService> _journalService;
        private readonly Lazy<IUserObjectService> _userObjectService;
        private readonly Lazy<IUserObjectPermissionService> _userObjectPermission;

        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager logger,IMapper mapper, IDataShaper<EmployeeDto> employeedataShaper, IDataShaper<CompanyDto> companydataShaper, IEmployeeLinks employeeLinks, ICompanyLinks companyLinks)
       {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repositoryManager, logger, mapper, companydataShaper, companyLinks));
            _employeeService = new Lazy<IEmployeeService>(() =>new EmployeeService(repositoryManager, logger, mapper, employeedataShaper, employeeLinks));

            _permissionService = new Lazy<IPermissionService>(() => new PermissionService(repositoryManager, logger, mapper));
            _userService = new Lazy<IUserService>(() => new UserService(repositoryManager, logger, mapper));
            _userDeviceService = new Lazy<IUserDeviceService>(() => new UserDeviceService(repositoryManager, logger, mapper));
            _sharedObjectService = new Lazy<ISharedObjectService>(() => new SharedObjectService(repositoryManager, logger, mapper));
            _blockService = new Lazy<IBlockService>(() => new BlockService(repositoryManager, logger, mapper));
            _journalService = new Lazy<IJournalService>(() => new JournalService(repositoryManager, logger, mapper));
            _userObjectService = new Lazy<IUserObjectService>(() => new UserObjectService(repositoryManager, logger, mapper));
            _userObjectPermission = new Lazy<IUserObjectPermissionService>(() => new UserObjectPermissionService(repositoryManager, logger, mapper));
        }
        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IPermissionService PermissionService=> _permissionService.Value;
        public IUserService UserService => _userService.Value;
        public IUserDeviceService UserDeviceService => _userDeviceService.Value;
        public ISharedObjectService SharedObjectService => _sharedObjectService.Value;
        public IBlockService BlockService => _blockService.Value;
        public IJournalService JournalService => _journalService.Value;
        public IUserObjectService UserObjectService => _userObjectService.Value;
        public IUserObjectPermissionService UserObjectPermissionService => _userObjectPermission.Value;

    }
}

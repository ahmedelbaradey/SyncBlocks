using Contracts;
using Contracts.Interfaces;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;

        private readonly Lazy<IPermissionRepository> _permissionRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        private readonly Lazy<IUserDeviceRepository> _userDeviceRepository;
        private readonly Lazy<ISharedObjectRepository> _sharedObjectRepository;
        private readonly Lazy<IBlockRepository> _blockRepository;
        private readonly Lazy<IJournalRepository> _journalRepository;
        private readonly Lazy<IUserObjectRepository> _userObjectRepository;
        private readonly Lazy<IUserObjectPermissionRepository> _userObjectPermissionRepository;


        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
            _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
            _permissionRepository = new Lazy<IPermissionRepository>(() => new PermissionRepository(repositoryContext));
            _userRepository = new Lazy<IUserRepository>(() => new UserRepository(repositoryContext));
            _userDeviceRepository = new Lazy<IUserDeviceRepository>(() => new UserDeviceRepository(repositoryContext));
            _sharedObjectRepository = new Lazy<ISharedObjectRepository>(() => new SharedObjectRepository(repositoryContext));
            _blockRepository = new Lazy<IBlockRepository>(() => new BlockRepository(repositoryContext));
            _journalRepository = new Lazy<IJournalRepository>(() => new JournalRepository(repositoryContext));
            _userObjectRepository = new Lazy<IUserObjectRepository>(() => new UserObjectRepository(repositoryContext));
            _userObjectPermissionRepository = new Lazy<IUserObjectPermissionRepository>(() => new UserObjectPermissionRepository(repositoryContext));

        }
        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IPermissionRepository Permission => _permissionRepository.Value;
        public IUserRepository User => _userRepository.Value;
        public IUserDeviceRepository UserDevice =>_userDeviceRepository.Value;
        public ISharedObjectRepository SharedObject =>  _sharedObjectRepository.Value;
        public IBlockRepository Block => _blockRepository.Value;    
        public IJournalRepository Journal => _journalRepository.Value;
        public IUserObjectRepository UserObject => _userObjectRepository.Value;
        public IUserObjectPermissionRepository UserObjectPermission => _userObjectPermissionRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}

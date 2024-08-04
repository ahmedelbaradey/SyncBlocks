using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
        : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new SharedObjectConfiguration());
            modelBuilder.ApplyConfiguration(new BlockConfiguration());
            modelBuilder.ApplyConfiguration(new JournalConfiguration());
            modelBuilder.ApplyConfiguration(new UserObjectConfiguration());
            modelBuilder.ApplyConfiguration(new UserObjectPermissionConfiguration());
        }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<Permission>? Permissions { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserDevice>? UserDevices { get; set; }
        public DbSet<SharedObject>? SharedObjects { get; set; }
        public DbSet<Block>? Blocks { get; set; }
        public DbSet<Journal>? Journals { get; set; }
        public DbSet<UserObject>? UserObjects { get; set; }
        public DbSet<UserObjectPermission>? UserObjectPermissions { get; set; }

    }
}

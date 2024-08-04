using Entities.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class UserObjectPermissionConfiguration : IEntityTypeConfiguration<UserObjectPermission>
    {
        public void Configure(EntityTypeBuilder<UserObjectPermission> builder) { }
    }
}

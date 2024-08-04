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
    public class SharedObjectConfiguration : IEntityTypeConfiguration<SharedObject>
    {
        public void Configure(EntityTypeBuilder<SharedObject> builder) { }
    }
}

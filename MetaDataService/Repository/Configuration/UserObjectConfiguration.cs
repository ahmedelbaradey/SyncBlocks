﻿using Entities.DataModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class UserObjectConfiguration : IEntityTypeConfiguration<UserObject>
    {
        public void Configure(EntityTypeBuilder<UserObject> builder) { }
    }
}

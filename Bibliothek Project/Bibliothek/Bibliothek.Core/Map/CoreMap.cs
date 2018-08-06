﻿using Bibliothek.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bibliothek.Core.Map
{
    public class CoreMap<T> : EntityTypeConfiguration<T> where T : CoreEntity
    {
        public CoreMap()
        {
            Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.CreatedUserName).HasColumnName("CreatedUserName").HasMaxLength(30).IsOptional();
            Property(x => x.CreatedComputerName).HasColumnName("CreatedComputerName").HasMaxLength(30).IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedIP).HasColumnName("CreatedIP").IsOptional();

            Property(x => x.ModifiedUserName).HasColumnName("ModifiedUserName").HasMaxLength(30).IsOptional();
            Property(x => x.ModifiedComputerName).HasColumnName("ModifiedComputerName").HasMaxLength(30).IsOptional();
            Property(x => x.ModifiedDate).HasColumnName("ModifiedDate").IsOptional();
            Property(x => x.ModifiedIP).HasColumnName("ModifiedIP").IsOptional();
        }
    }
}

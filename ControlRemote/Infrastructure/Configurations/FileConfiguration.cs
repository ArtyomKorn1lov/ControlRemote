using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FileConfiguration : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            builder.ToTable(nameof(FileEntity));

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Name).IsRequired();
            builder.Property(f => f.Path).IsRequired();
        }
    }
}

using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class MangerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable(nameof(Manager));

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired();
            builder.Property(m => m.Login).IsRequired();
            builder.Property(m => m.Password).IsRequired();
            builder.HasMany(m => m.Employers).WithOne().HasForeignKey(e => e.ManagerId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

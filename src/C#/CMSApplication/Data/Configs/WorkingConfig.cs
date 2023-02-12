using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Data.Configs
{
    public class WorkingConfig : IEntityTypeConfiguration<Working>
    {
        public void Configure(EntityTypeBuilder<Working> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RoleOffDate).IsRequired();
            builder.Property(x => x.RoleOnDate).IsRequired();
            //builder
            //    .HasOne(x => x.Employee)
            //    .WithMany(x => x.Workings)
            //    .HasForeignKey(x => x.EmployeeId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }

}

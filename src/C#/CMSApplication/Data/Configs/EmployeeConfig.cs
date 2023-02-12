using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSApplication.Data.Configs
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
       public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(x => x.Id);
            builder
                .Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255);
            builder.Property(x => x.ContactNo).IsRequired().HasMaxLength(20);
            builder.Property(x => x.BaseLocation).IsRequired().HasMaxLength(100);
            builder.Property(x => x.ContactNo).IsRequired().HasMaxLength(20);
            builder.Property(x => x.PrimarySkill).IsRequired();
            builder.Property(x => x.Feedback).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.ProfileFile).IsRequired(false);

            //builder
            //    .HasOne(x => x.User)
            //    .WithOne(x => x.Employee)
            //    .HasForeignKey<User>(x => x.Id);

            builder.HasMany(x => x.Workings)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId);
        }
    }
}

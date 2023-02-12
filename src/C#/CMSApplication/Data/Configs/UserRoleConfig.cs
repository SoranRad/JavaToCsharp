using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Data.Configs
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            //builder
            //    .HasKey(x => x.Id)
            //    ;

            builder
                .Property(x => x.Id).ValueGeneratedOnAdd();


            builder
                .HasOne(x => x.AppRole)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.userRole)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserId);
             
        }
    }
}

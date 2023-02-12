using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Data.Configs
{
    public class ApplicationRoleConfig : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id).ValueGeneratedOnAdd();


            //builder
            //    .Ignore(x => x.CreatedDate)
            //    .Ignore(x => x.ModifiedDate)
            //    ;

            //builder
            //    .HasMany(x => x.UserRoles)
            //    .WithOne(x => x.AppRole)
            //    .HasForeignKey(x => x.RoleId)
            //    .IsRequired();


        }
    }
}

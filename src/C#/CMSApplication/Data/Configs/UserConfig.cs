using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSApplication.Data.Configs
{
    public class UserConfig  : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id)
                ;

            
            builder
                .Property(x => x.Id).ValueGeneratedOnAdd();



            // removed unnesserry fields

            builder
                .Ignore(x => x.AccessFailedCount)
                .Ignore(x => x.ConcurrencyStamp)
                .Ignore(x => x.EmailConfirmed)
                .Ignore(x => x.LockoutEnabled)
                .Ignore(x => x.LockoutEnd)
                //.Ignore(x => x.NormalizedEmail)
                .Ignore(x => x.PhoneNumberConfirmed)
                .Ignore(x => x.TwoFactorEnabled)
                .Ignore(x=>x.PhoneNumber)
                ;

            builder
                .HasOne(x => x.Employee)
                .WithOne(x => x.User)
                .HasForeignKey<Employee>(x => x.Id);
        }
    }
}

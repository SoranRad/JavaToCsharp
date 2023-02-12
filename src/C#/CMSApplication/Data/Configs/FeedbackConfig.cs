using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSApplication.Data.Configs
{
    public class FeedbackConfig : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id).ValueGeneratedOnAdd();

            builder
                .HasIndex(x => x.ContactNumber)
                .IsUnique();

            builder
                .Property(x => x.Commnets)
                .HasMaxLength(200)
                .IsRequired()
                ;

            builder
                .Property(x => x.ContactNumber)
                .HasMaxLength(10)
                .IsUnicode()
                .IsRequired()
                ;

            builder
                .Property(x => x.Email)
                .IsRequired()
                ;

        }
    }
}

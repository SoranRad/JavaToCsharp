using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSApplication.Data.Configs
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder
                .HasKey(x => x.Id)
                ;


            builder
                .Property(x => x.Id).ValueGeneratedOnAdd();

            builder
                .Property(x => x.description)
                .IsRequired(false)
                .HasMaxLength(5000);

            builder.HasMany(x => x.Quizzes)
                .WithOne(x => x.Category)
                .HasForeignKey(x => x.CategorId)
                .IsRequired();

        }
    }
}

using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CMSApplication.Data.Configs
{
    public class ScoresConfig : IEntityTypeConfiguration<Scores>
    {
        public void Configure(EntityTypeBuilder<Scores> builder)
        {
            builder
                .HasKey         (x => x.Id);

            builder
                .Property       (x => x.Id).ValueGeneratedOnAdd();

            builder
                .Property(x => x.Score)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18,2)
                ;

            builder
                .HasOne         (x => x.User)
                .WithMany       (x => x.UserScores)
                .HasForeignKey  (x => x.UserId)
                .IsRequired     ();

             builder
                .HasOne         (x => x.Quiz)
                .WithMany       (x => x.Scores)
                .HasForeignKey  (x => x.QuizId)
                .IsRequired     ();
        }
    }
}

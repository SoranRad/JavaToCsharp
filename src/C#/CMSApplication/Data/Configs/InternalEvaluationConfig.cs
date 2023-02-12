using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Data.Configs
{
    public class InternalEvaluationConfig : IEntityTypeConfiguration<InternalEvaluation>
    {
        public void Configure(EntityTypeBuilder<InternalEvaluation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.HasOne(x => x.Employee).WithMany(x => x.InternalEvaluations).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        }
  
    }
}

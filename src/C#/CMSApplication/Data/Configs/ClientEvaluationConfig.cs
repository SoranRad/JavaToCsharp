using CMSApplication.Data.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Data.Configs
{
    public class ClientEvaluationConfig : IEntityTypeConfiguration<ClientEvaluation>
    {
        public void Configure(EntityTypeBuilder<ClientEvaluation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description).HasMaxLength(100);
            builder.HasOne(x => x.Employee).WithMany(x => x.ClientEvaluations).HasForeignKey(x => x.EmployeeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
   
}

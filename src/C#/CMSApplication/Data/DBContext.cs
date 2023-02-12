using CMSApplication.Data.Configs;
using CMSApplication.Data.Entity;
using CMSApplication.Services.Abstraction;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Threading;
using Category = CMSApplication.Data.Entity.Category;

namespace CMSApplication.Data
{
    public class DBContext //: IdentityDbContext<User,IdentityRole,string>

        : IdentityDbContext
            //<UserAccount,  ApplicationRole, int,
            <   User,
                ApplicationRole,
                int,
                IdentityUserClaim<int>, // TUserClaim
                UserRole,               // TUserRole,
                IdentityUserLogin<int>, // TUserLogin
                IdentityRoleClaim<int>, // TRoleClaim
                IdentityUserToken<int>  // TUserToken
            >

    {
        public DBContext(DbContextOptions options): base(options)
        {

        }

        public virtual DbSet<Quiz>      Quizzes         { get; set; }
        public virtual DbSet<Category>  Categories      { get; set; }
        public virtual DbSet<Employee>  Employees       { get; set; }
        public virtual DbSet<Working>   Workings        { get; set; }
        public virtual DbSet<Question>  Questions       { get; set; }
        public virtual DbSet<Feedback>  Feedbacks       { get; set; }
        public virtual DbSet<Scores>    Scores          { get; set; }


        public virtual DbSet<InternalEvaluation> InternalEvaluations { get; set; }
        public virtual DbSet<ClientEvaluation> ClientEvaluations { get; set; }

     

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationRoleConfig());
            builder.ApplyConfiguration(new UserConfig());
            builder.ApplyConfiguration(new UserRoleConfig());

            builder.ApplyConfiguration(new QuizConfig());
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new QuestionConfig());
            builder.ApplyConfiguration(new FeedbackConfig());
            builder.ApplyConfiguration(new ScoresConfig());

            builder.ApplyConfiguration(new EmployeeConfig());
            builder.ApplyConfiguration(new WorkingConfig());
            builder.ApplyConfiguration(new InternalEvaluationConfig());
            builder.ApplyConfiguration(new ClientEvaluationConfig());

            builder.Ignore<IdentityUserLogin<int>>();
            builder.Ignore<IdentityUserClaim<int>>();
            builder.Ignore<IdentityRoleClaim<int>>();
            builder.Ignore<IdentityUserToken<int>>();
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entries = ChangeTracker.Entries();

            var now = DateTime.UtcNow;
            foreach (var item in entries)
            {
                if (item.Entity is IBaseEntity)
                {
                    switch (item.State)
                    {
                        case EntityState.Modified:
                            {
                                item.Property(nameof(IBaseEntity.ModifiedDate)).CurrentValue = now;
                            }
                            break;
                        case EntityState.Added:
                            {
                                item.Property(nameof(IBaseEntity.CreatedDate)).CurrentValue = now;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}

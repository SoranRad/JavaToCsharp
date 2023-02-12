using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using CMSApplication.Data;

namespace CMSApplication.DesignTime
{
    public class DesignTimeContextFactory : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DbContext>();

            //==Local or Network Address
            optionsBuilder.UseSqlServer(@"Server=.\sqlnegin;Database=CMS;User Id=sa;Password=123456;MultipleActiveResultSets=True;Trust Server Certificate=true");

            return new DBContext(optionsBuilder.Options);
        }
    }
}

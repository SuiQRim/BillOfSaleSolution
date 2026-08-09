using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BillSale.DAL.Context
{
    public class BIllSaleDesignTimeContextFactory : IDesignTimeDbContextFactory<BillSaleContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context
        /// </summary>
        /// <remarks>
        /// 1) dotnet tool install --global dotnet-ef
        /// 2) dotnet tool update --global dotnet-ef
        /// 3) dotnet ef migrations add [name] --project DataAccessLayer/FinalExercise.Context/FinalExercise.Context.csproj
        /// 4) dotnet ef database update --project DataAccessLayer/FinalExercise.Context/FinalExercise.Context.csproj
        /// 5) dotnet ef database update [targetMigrationName] --project DataAccessLayer/FinalExercise.Context/FinalExercise.Context.csproj
        /// </remarks>
        public BillSaleContext CreateDbContext(string[] args)
        {
            var connectionString = "Host=localhost;Port=5432;Database=BillSale;Username=postgres;Password=12345";
            var options = new DbContextOptionsBuilder<BillSaleContext>()
                .UseNpgsql(connectionString)
                .LogTo(Console.WriteLine)
                .Options;

            return new BillSaleContext(options);
        }
    }
}

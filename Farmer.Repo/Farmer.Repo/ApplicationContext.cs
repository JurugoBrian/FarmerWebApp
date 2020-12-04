/* Repository Layer of the Onion Architecture creates an abstraction layer between the Domain Entities Layer
   and the Business Logic Layer of the application allowing loosely coupled approach to data access. This repositoy
   queries the data source for data, maps the data from the data source to the business entity, and persists changes
   in the business entity to the data source*/

using Microsoft.EntityFrameworkCore;
using Farmer.Data;

namespace Farmer.Repo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new FarmersMap(modelBuilder.Entity<Farmers>());
            new FarmerProfileMap(modelBuilder.Entity<FarmerProfile>());
        }

    }
}

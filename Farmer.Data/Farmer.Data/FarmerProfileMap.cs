using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmer.Data
{
    public class FarmerProfileMap
    {
        public FarmerProfileMap(EntityTypeBuilder<FarmerProfile> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.FirstName).IsRequired();
            entityBuilder.Property(t => t.LastName).IsRequired();
            entityBuilder.Property(t => t.Address);
        }
    }
}

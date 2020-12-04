using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Farmer.Data
{
    public class FarmersMap
    {
        public FarmersMap(EntityTypeBuilder<Farmers> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Email).IsRequired();
            entityBuilder.Property(t => t.Password).IsRequired();
            entityBuilder.Property(t => t.Email).IsRequired();
            entityBuilder.HasOne(t => t.FarmerProfile).WithOne(u => u.Farmers).HasForeignKey<FarmerProfile>(x => x.Id);
        }
    }
}

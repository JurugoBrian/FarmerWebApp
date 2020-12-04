

namespace Farmer.Data
{
    public class FarmerProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public virtual Farmers Farmers { get; set; }
    }
}

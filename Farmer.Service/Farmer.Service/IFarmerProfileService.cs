using Farmer.Data;

namespace Farmer.Service
{
    public interface IFarmerProfileService
    {
        FarmerProfile GetFarmerProfile(long id);
    }
}

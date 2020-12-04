using Farmer.Data;
using Farmer.Repo;

namespace Farmer.Service
{
    public class FarmerProfileService : IFarmerProfileService
    {
        private IRepository<FarmerProfile> farmerProfileRepository;
        public FarmerProfileService(IRepository<FarmerProfile> farmerProfileRepository)
        {
            this.farmerProfileRepository = farmerProfileRepository;
        }
        public FarmerProfile GetFarmerProfile(long id)
        {
            return farmerProfileRepository.Get(id);
        }

    }
}

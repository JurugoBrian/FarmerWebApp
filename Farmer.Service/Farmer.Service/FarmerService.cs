/* The Service Layer of the onion architecture holds interfaces and classes which have an implementation of interfaces. 
   This layer is intended to build loosely coupled applications. It communicates to both the Core Web Applications Layer and 
   Repository Layer.
*/

using System;
using Farmer.Data;
using Farmer.Repo;
using System.Collections.Generic;

namespace Farmer.Service
{
    public class FarmerService : IFarmerService
    {
        private IRepository<Farmers> farmerRepository;
        private IRepository<FarmerProfile> farmerProfileRepository;

        public FarmerService(IRepository<Farmer.Data.Farmers> farmerRepository, IRepository<FarmerProfile> farmerProfileRepository)
        {
            this.farmerRepository = farmerRepository;
            this.farmerProfileRepository = farmerProfileRepository;
        }

        public IEnumerable<Farmers> GetFarmers()
        {
            return farmerRepository.GetAll();
        }
        public Farmers GetFarmer(long id)
        {
            return farmerRepository.Get(id);
        }
        public void InsertFarmer(Farmers farmers)
        {
            farmerRepository.Insert(farmers);
        }
        public void UpdateFarmer(Farmers farmers)
        {
            farmerRepository.Update(farmers);
        }
        public void DeleteFarmer(long id)
        {
            FarmerProfile farmerProfile = farmerProfileRepository.Get(id);
            farmerProfileRepository.Remove(farmerProfile);
            Farmers farmers = GetFarmer(id);
            farmerRepository.Remove(farmers);
            farmerRepository.SaveChanges();
        }
    }
}

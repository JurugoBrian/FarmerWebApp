using Farmer.Data;
using System.Collections.Generic;

namespace Farmer.Service
{
    public interface IFarmerService
    {
        IEnumerable<Farmers> GetFarmers();
        Farmers GetFarmer(long id);
        void InsertFarmer(Farmers farmers);
        void UpdateFarmer(Farmers farmers);
        void DeleteFarmer(long id);
    }
}

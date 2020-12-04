/*
 This is the Domain Entities Layer which is a core and central part of the Onion Architecture. It is implemented as a class library 
 project and holds POCO classes along with the configuration classes which are used to create the database tables.
 Application domain objects include the BaseEntity, Farmer, and FarmerProfile*
 */

namespace Farmer.Data
{
    public class Farmers : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual FarmerProfile FarmerProfile { get; set; }
    }
}

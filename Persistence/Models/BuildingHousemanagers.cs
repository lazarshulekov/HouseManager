namespace Persistence.Models
{
    using DAL.Models;

    using Persistence.Models.Identity;
    public class BuildingHousemanagers
    {
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; }

        public int HouseManagerId { get; set; }

        public virtual AppUser HouseManager { get; set; }
    }
}
namespace HouseManager.ViewModels
{
    using System.ComponentModel;

    using BLL.Models;

    public class PropertyViewModel
    {
        public int Id { get; set; }

        public decimal Area { get; set; }

        public string Comments { get; set; }

        [DisplayName("PropertyType")]
        public int PropertyTypeId { get; set; }

        public PropertyTypeViewModel PropertyType { get; set; }

        [DisplayName("Owner")]
        public int AppUserId { get; set; }

        public AppUserViewModel AppUser { get; set; }

        public int BuildingId { get; set; }

        public string BuildingName { get; set; }
    }
}

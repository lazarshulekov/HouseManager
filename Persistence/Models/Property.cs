namespace Persistence.Models
{
    using System.Collections.Generic;

    using DAL.Models;

    using Persistence.Models.Identity;

    public class Property
    {
        public int Id { get; set; }

        public decimal Area { get; set; }

        public string Comments { get; set; }

        public int PropertyTypeId { get; set; }

        public PropertyType PropertyType { get; set; }

        public int? AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public ICollection<PropertiesExpenses> PropertiesExpenses { get; set; }

        public ICollection<BuildingProperties> BuildingProperties { get; set; }
    }
}
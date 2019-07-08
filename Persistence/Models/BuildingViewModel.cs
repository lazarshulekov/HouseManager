namespace DAL.Models
{
    using System.Collections.Generic;

    public class BuildingViewModel
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public ICollection<BuildingHousemanagers> BuildingHouseManagers { get; set; }

        public ICollection<BuildingProperties> BuildingProperties { get; set; }

        public ICollection<int> SelectedManagers { get; set; }

        public ICollection<int> SelectedProperties { get; set; }
    }
}
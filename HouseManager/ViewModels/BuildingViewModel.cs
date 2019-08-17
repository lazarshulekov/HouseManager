namespace HouseManager.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using DAL.Models;

    public class BuildingViewModel
    {
        public int Id { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public int Number { get; set; }

        [DisplayName("HouseManagers")]
        public ICollection<BuildingHousemanagers> BuildingHouseManagers { get; set; }

        public ICollection<BuildingProperties> BuildingProperties { get; set; }

        public ICollection<int> SelectedManagers { get; set; }

        public ICollection<int> SelectedProperties { get; set; }
    }
}
namespace HouseManager.ViewModels
{
    using System.ComponentModel;

    public class PropertyTypeViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Type { get; set; }
    }

    public class IdNameViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

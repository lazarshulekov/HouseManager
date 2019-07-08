namespace DAL.Models
{
    using System.Collections.Generic;

    public class PropertyType
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
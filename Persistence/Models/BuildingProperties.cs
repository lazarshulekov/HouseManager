namespace DAL.Models
{
    public class BuildingProperties

    {
        public int BuildingId { get; set; }

        public virtual Building Building { get; set; }

        public int PropertyId { get; set; }

        public virtual Property Property { get; set; }
    }
}
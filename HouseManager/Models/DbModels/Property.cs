namespace HouseManager.Models
{
    public class Property
    {
        public int Id { get; set; }
        public int Area { get; set; }
        public int Comments { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}

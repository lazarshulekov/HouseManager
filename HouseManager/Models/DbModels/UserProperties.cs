namespace HouseManager.Models
{
    public class UserProperties
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int PropertyId { get; set; }
        public virtual Property Property { get; set; }
    }
}

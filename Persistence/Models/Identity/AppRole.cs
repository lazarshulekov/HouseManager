namespace DAL.Models.Identity
{
    using System.Collections.Generic;

    public class AppRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<AppUsersRoles> AppUsersRoles { get; set; }
    }
}
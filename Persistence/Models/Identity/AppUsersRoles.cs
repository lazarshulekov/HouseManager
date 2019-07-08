namespace DAL.Models.Identity
{
    public class AppUsersRoles
    {
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public int AppRoleId { get; set; }

        public AppRole AppRole { get; set; }
    }
}
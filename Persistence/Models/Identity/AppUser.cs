namespace DAL.Models.Identity
{
    using System.Collections.Generic;

    public class AppUser // : IdentityUser<int>
    {
        // <int, IdentityUserClaim<AppUser>, IdentityUserRole<AppUser>, IdentityUserLogin<AppUser>, IdentityUserToken<AppUser>>
        public int Id { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<AppUsersRoles> AppUsersRoles { get; set; }

        public ICollection<QuestionnaireUserVotes> QuestionnaireUserVotes { get; set; }

        public bool Banned { get; set; } // banned users cannot create Questionnaires

        public ICollection<Property> Properties { get; set; }

        public ICollection<BuildingHousemanagers> BuildingHouseManagers { get; set; }
    }
}
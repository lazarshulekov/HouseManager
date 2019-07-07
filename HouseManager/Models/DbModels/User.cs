using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

namespace HouseManager.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Rank { get; set; }

        public bool Banned { get; set; } //banned users cannot create Questionnaires
        public ICollection<Questionnaire> FavouriteQuestionnaires { get; set; }
        public ICollection<Meeting> Meetings { get; set; } 
    }
}

namespace Persistence.Models
{
    using Persistence.Models.Identity;

    public class UsersFavouriteQuestionnaires
    {
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public int QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }
    }
}
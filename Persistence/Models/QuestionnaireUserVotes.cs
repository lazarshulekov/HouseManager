namespace DAL.Models
{
    using DAL.Models.Identity;

    public class QuestionnaireUserVotes
    {
        public int QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public int UserId { get; set; }

        public AppUser AppUser { get; set; }

        public bool Agrees { get; set; }
    }
}
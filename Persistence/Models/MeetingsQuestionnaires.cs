namespace DAL.Models
{
    using Models;

    public class MeetingsQuestionnaires
    {
        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }

        public int QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public bool Accepted { get; set; }
    }
}
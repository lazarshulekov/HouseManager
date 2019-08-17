namespace HouseManager.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class QuestionnaireViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        public int Likes { get; set; }

        public bool Voted { get; set; }

        public bool IsActive { get; set; }
    }

    public class MeetingQuestionnaireViewModel
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public int Likes { get; set; }

        public string QuestionAndLikes => $"{Question} {Likes}";
    }
}
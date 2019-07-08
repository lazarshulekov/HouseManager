namespace HouseManager.ViewModels
{
    public class QuestionnaireViewModel
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public int Likes { get; set; }

        public bool Voted { get; set; }

        public bool IsActive { get; set; }
    }
}
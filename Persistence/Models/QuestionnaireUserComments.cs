namespace Persistence.Models
{
    using System;
    using System.Collections.Generic;

    using Persistence.Models.Identity;

    public class QuestionnaireUserComments
    {
        public int QuestionnaireId { get; set; }

        public Questionnaire Questionnaire { get; set; }

        public int AppUserId { get; set; }

        public AppUser User { get; set; }

        public string Comment { get; set; }

        public DateTime CommentDate { get; set; }
    }
}
﻿namespace DAL.Models
{
    using System;
    using System.Collections.Generic;

    using DAL.Models.Identity;

    public class Questionnaire
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public int UserId { get; set; } // User who created the questionnaire

        public virtual AppUser CreatedByAppUser { get; set; }

        public ICollection<QuestionnaireUserVotes> QuestionnaireUserVotes { get; set; }

        public ICollection<MeetingsQuestionnaires> MeetingsQuestionnaires { get; set; }
    }
}
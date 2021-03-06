﻿namespace HouseManager.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using DAL.Models;

    public class MeetingViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Comments { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateTime { get; set; }

        [Required]
        public string Location { get; set; }

        public ICollection<int> SelectedIssues { get; set; }

        public ICollection<QuestionnaireViewModel> MeetingQuestionnaires { get; set; }
    }
}
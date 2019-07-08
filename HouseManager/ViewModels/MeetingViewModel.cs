namespace HouseManager.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Persistence.Models;

    public class MeetingViewModel
    {
        public int Id { get; set; }

        public string Comments { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datetime { get; set; }

        public string Location { get; set; }
    }

    public class MeetingIssuesViewModel
    {
        public int Id { get; set; }
        public List<MeetingIssues> MeetingIssues { get; set; }
    }

    public class MeetingIssues
    {
    }
}
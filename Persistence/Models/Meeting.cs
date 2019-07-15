namespace DAL.Models
{
    using System;
    using System.Collections.Generic;

    public class Meeting
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public string Location { get; set; }

        public string Comments { get; set; }

        public ICollection<MeetingsQuestionnaires> MeetingsQuestionnaires { get; set; }
    }
}
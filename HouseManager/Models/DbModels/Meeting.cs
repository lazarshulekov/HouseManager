using System;
using System.Collections.Generic;

namespace HouseManager.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Location { get; set; }
        public string Comments { get; set; }

        public ICollection<Issue> Issues { get; set; }

    }
}

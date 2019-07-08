namespace Persistence.Models
{
    using System.Collections.Generic;

    using DAL.Models;

    public class Issue
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Accepted { get; set; }

        public ICollection<MeetingsIssues> MeetingsIssues { get; set; }
    }
}
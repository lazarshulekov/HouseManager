namespace DAL.Models
{
    using Persistence.Models;

    public class MeetingsIssues
    {
        public int MeetingId { get; set; }

        public Meeting Meeting { get; set; }

        public int IssueId { get; set; }

        public Issue Issue { get; set; }
    }
}
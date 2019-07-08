namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    using Microsoft.EntityFrameworkCore;

    using Persistence.Models;

    public class MeetingService : IMeetingService
    {
        private readonly TimeSpan questionnairesInterval = TimeSpan.FromDays(30);
        private readonly AppDbContext context;

        public MeetingService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Meeting>> GetAllMeetings()
        {
            return await context.Meetings.ToListAsync();
        }

        public async Task AddAsync(Meeting meeting)
        {
            foreach (var issue in meeting.MeetingsIssues)
            {
                var quest = await context.Questionnaires.FindAsync(issue.IssueId);
                var issueEntity = await this.context.Issues.FindAsync(issue.IssueId);
                if (issueEntity == null)
                {
                    issueEntity = new Issue() { Id = quest.Id, Name = quest.Question };
                    await context.Issues.AddAsync(issueEntity);
                    await context.SaveChangesAsync();
                }
            }
            
            context.Meetings.Add(meeting);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            var meetingEntity = await context.Meetings.FindAsync(meeting.Id);
            var meetingIssues = meetingEntity.MeetingsIssues;
            foreach (var issue in meetingIssues)
            {
                var issueEntity = this.context.Issues.FindAsync(issue.IssueId);
                if (issueEntity == null)
                {
                    var ques
                    this.context.Issues.AddAsync(new Issue())
                }
            }
            meetingEntity.DateTime = meeting.DateTime;
            meetingEntity.Location = meeting.Location;
            meetingEntity.Comments = meeting.Comments;
            meetingEntity.MeetingsIssues = meeting.MeetingsIssues;
            context.Meetings.Update(meetingEntity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meeting = await context.Meetings.FindAsync(id);
            context.Meetings.Remove(meeting);

            await context.SaveChangesAsync();
        }

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return await context.Meetings.FindAsync(id);
        }

        public async Task<List<Questionnaire>> GetAllQuestionnaires()
        {
            var startTime = DateTime.UtcNow - questionnairesInterval;
            return await context.Questionnaires.Where(q => q.DateTimeCreated > startTime).ToListAsync();
        }
    }
}
namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    using Microsoft.EntityFrameworkCore;

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
            await context.MeetingsQuestionnaires.AddRangeAsync(meeting.MeetingsQuestionnaires);
            await context.Meetings.AddAsync(meeting);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            var mqs = context.MeetingsQuestionnaires.Where(x => x.MeetingId == meeting.Id);

            context.MeetingsQuestionnaires.RemoveRange(mqs);
            await context.SaveChangesAsync();

            var meetingEntity = await context.Meetings.FindAsync(meeting.Id);

            meetingEntity.DateTime = meeting.DateTime;
            meetingEntity.Location = meeting.Location;
            meetingEntity.Comments = meeting.Comments;
            meetingEntity.MeetingsQuestionnaires = meeting.MeetingsQuestionnaires;
            
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var meeting = await context.Meetings.FindAsync(id);
            var quests = meeting.MeetingsQuestionnaires;
            context.Meetings.Remove(meeting);
            if (quests != null && quests.Any())
            {
                context.MeetingsQuestionnaires.RemoveRange(quests);
            }

            await context.SaveChangesAsync();
        }

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return context.Meetings.Where(x => x.Id == id ).Include(x => x.MeetingsQuestionnaires).SingleOrDefault();
        }

        public async Task<List<Questionnaire>> GetAllQuestionnaires()
        {
            var startTime = DateTime.UtcNow - questionnairesInterval;
            return await context.Questionnaires.Where(q => q.DateTimeCreated > startTime).ToListAsync();
        }
    }
}
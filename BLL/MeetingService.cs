namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Persistence.Models;

    public class MeetingService : IMeetingService
    {
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
            context.Meetings.Add(meeting);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            var meetingEntity = await context.Meetings.FindAsync(meeting.Id);
            meetingEntity.DateTime = meeting.DateTime;
            meetingEntity.Location = meeting.Location;
            meetingEntity.Comments = meeting.Comments;

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
    }
}
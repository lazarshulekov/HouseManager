namespace BLL
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using DAL.Models;
    using log4net;

    public class MeetingServiceLoggerDecorator : IMeetingService
    {
        private readonly ILog logger;
        private readonly IMeetingService meetingService;

        public MeetingServiceLoggerDecorator(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
            this.logger = LogManager.GetLogger(GetType());
        }
        public async Task<List<Meeting>> GetAllMeetings()
        {
            return await meetingService.GetAllMeetings();
        }

        public async Task AddAsync(Meeting meeting)
        {
            logger.Info("Adding new meeting");
            await meetingService.AddAsync(meeting);
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            logger.Info($"Updating meeting {meeting.Id}");
            await meetingService.UpdateAsync(meeting);
        }

        public async Task DeleteAsync(int id)
        {
            logger.Info($"Deleting meeting {id}");
            await meetingService.DeleteAsync(id);
        }

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return await meetingService.GetMeetingByIdAsync(id);
        }

        public async Task<List<Questionnaire>> GetAllQuestionnaires()
        {
            return await meetingService.GetAllQuestionnaires();
        }
    }
}
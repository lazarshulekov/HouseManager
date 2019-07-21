namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    using log4net;

    public class MeetingServiceExceptionHandlingDecorator : IMeetingService
    {
        private readonly ILog logger;
        private readonly IMeetingService meetingService;

        public MeetingServiceExceptionHandlingDecorator(IMeetingService meetingService)
        {
            this.meetingService = meetingService;
            logger = LogManager.GetLogger(GetType());
        }
        public async Task<List<Meeting>> GetAllMeetings()
        {
            try
            {
                return await meetingService.GetAllMeetings();
            }
            catch (Exception ex)
            {
                logger.Error($"Exception occured in GetAllMeetings", ex);
                return new List<Meeting>();
            }
        }

        public async Task AddAsync(Meeting meeting)
        {
            try
            {
                await meetingService.AddAsync(meeting);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception occured in AddAsync", ex);
            }
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            try
            {
                await meetingService.UpdateAsync(meeting);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception occured in UpdateAsync", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await meetingService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception occured in DeleteAsync", ex);
            }
        }

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            try
            {
                return await meetingService.GetMeetingByIdAsync(id);
            }
            catch (Exception ex)
            {
                logger.Error($"Exception occured in GetMeetingByIdAsync", ex);
                return null;
            }
        }

        public async Task<List<Questionnaire>> GetAllQuestionnaires()
        {
            try
            {
                return await meetingService.GetAllQuestionnaires();
            }
            catch (Exception ex)
            {
                logger.Error($"Exception occured in GetAllQuestionnaires", ex);
                return new List<Questionnaire>();
            }
        }
    }
}
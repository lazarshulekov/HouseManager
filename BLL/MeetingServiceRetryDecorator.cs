namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    using log4net;

    using Polly;
    using Polly.Retry;

    public class MeetingServiceRetryDecorator : IMeetingService
    {
        private readonly IMeetingService meetingService;

        private readonly AsyncRetryPolicy policy;
        public MeetingServiceRetryDecorator(IMeetingService meetingService)
        {
            var logger = LogManager.GetLogger(GetType());
            policy = Policy.Handle<Exception>().WaitAndRetryAsync(
                3,
                i => TimeSpan.FromSeconds(2),
                ((exception, span, retry, context) =>
                    {
                        logger.Warn($"Execution failed with exception {exception.Message}. Waiting {span} before next retry. Retry attempt {retry}");
                    }));

            this.meetingService = meetingService;
        }
        public async Task<List<Meeting>> GetAllMeetings()
        {
            return await policy.ExecuteAsync(async () => await meetingService.GetAllMeetings());
        }

        public async Task AddAsync(Meeting meeting)
        {
            await policy.ExecuteAsync(async () => await meetingService.AddAsync(meeting));
        }

        public async Task UpdateAsync(Meeting meeting)
        {
            await policy.ExecuteAsync(async () => await meetingService.UpdateAsync(meeting));
        }

        public async Task DeleteAsync(int id)
        {
            await policy.ExecuteAsync(async () => await meetingService.DeleteAsync(id));
        }

        public async Task<Meeting> GetMeetingByIdAsync(int id)
        {
            return await policy.ExecuteAsync(async () => await meetingService.GetMeetingByIdAsync(id));
        }

        public async Task<List<Questionnaire>> GetAllQuestionnaires()
        {
            return await policy.ExecuteAsync(async () => await meetingService.GetAllQuestionnaires());
        }
    }
}
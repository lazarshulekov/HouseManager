namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    public class QuestionnaireService : IQuestionnairesService
    {
        private readonly TimeSpan activePeriod = TimeSpan.FromDays(30);
        private readonly AppDbContext context;
        private readonly IAppUserService appUserService;

        public QuestionnaireService(AppDbContext context, IAppUserService appUserService)
        {
            this.context = context;
            this.appUserService = appUserService;
        }

        public async Task AddAsync(string question, string userName)
        {
            var user = await appUserService.GetAppUserByUserNameAsync(userName);

            var quest = new Questionnaire()
                            {
                                CreatedByAppUser = user,
                                DateTimeCreated = DateTime.UtcNow,
                                Question = question
                            };

            await context.Questionnaires.AddAsync(quest);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionnaireUserVotes>> GetVotes(int questId)
        {
            return context.QuestionnaireUserVotes.Where(q => q.QuestionnaireId == questId);
        }

        public async Task DeleteAsync(int id)
        {
            var quest = await context.Questionnaires.FindAsync(id);
            context.Questionnaires.Remove(quest);
            await context.SaveChangesAsync();
        }

        public List<Questionnaire> GetAllQuestionnaires()
        {
            return context.Questionnaires.ToList();
        }

        public async Task<Questionnaire> GetQuestionnaireByIdAsync(int id)
        {
            return await context.Questionnaires.FindAsync(id);
        }

        public async Task UpdateAsync(int id, string question)
        {
            var quest = await  context.Questionnaires.FindAsync(id);
            quest.Question = question;

            await context.SaveChangesAsync();
        }

        public async Task ToggleVoteAsync(int questId, string userName)
        {
            var userId = await appUserService.GetUserIdByUserNameAsync(userName);
            var questVote = await context.QuestionnaireUserVotes.FindAsync(userId, questId);//SingleOrDefault(q => q.UserId == userId && q.QuestionnaireId == questId);
            if (questVote != null)
            {
                questVote.Agrees = !questVote.Agrees;
            }
            else
            {
                await context.QuestionnaireUserVotes.AddAsync(
                    new QuestionnaireUserVotes()
                        {
                            Agrees = true,
                            UserId = userId,
                            QuestionnaireId = questId
                        });
            }

            await context.SaveChangesAsync();
        }

        public async Task<bool> IsActive(int questId)
        {
            var quest = await context.Questionnaires.FindAsync(questId);

            return DateTime.UtcNow - quest.DateTimeCreated < this.activePeriod;
        }
    }
}
namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    using Persistence.Models;

    public interface IQuestionnairesService
    {
        Task AddAsync(string question, string userName);

        Task DeleteAsync(int questId);

        List<Questionnaire> GetAllQuestionnaires();

        Task<Questionnaire> GetQuestionnaireByIdAsync(int questId);

        Task UpdateAsync(int id, string question);

        Task ToggleVoteAsync(int questId, string userName);

        Task<IEnumerable<QuestionnaireUserVotes>> GetVotes(int questId);

        Task<bool> IsActive(int questId);

    }
}
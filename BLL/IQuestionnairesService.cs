namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    using Persistence.Models;

    public interface IQuestionnairesService
    {
        Task AddAsync(Questionnaire quest);

        Task DeleteAsync(int questId);

        List<Questionnaire> GetAllQuestionnaires();

        Task<Questionnaire> GetQuestionnaireByIdAsync(int questId);

        Task UpdateAsync(Questionnaire questionnaire);
    }
}
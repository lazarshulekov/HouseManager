namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using Persistence.Models;

    public class QuestionnaireService : IQuestionnairesService
    {
        private readonly AppDbContext context;

        private readonly IMapper mapper;

        public QuestionnaireService(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task AddAsync(Questionnaire quest)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(int questId)
        {
            throw new System.NotImplementedException();
        }

        public List<Questionnaire> GetAllQuestionnaires()
        {
            throw new System.NotImplementedException();
            //return await context.Questionnaires.Include(p => p.PropertyType).Include(p => p.AppUser).Include(p => p.BuildingProperties).ThenInclude(x => x.Building).ToListAsync();
        }

        public Task<Questionnaire> GetQuestionnaireByIdAsync(int questId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(Questionnaire questionnaire)
        {
            throw new System.NotImplementedException();
        }
    }
}
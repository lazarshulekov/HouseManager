using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace HouseManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using BLL;

    using global::AutoMapper;

    using HouseManager.ViewModels;

    public class QuestionnairesController : Controller
    {
        private readonly IMapper Mapper;
        private readonly IQuestionnairesService questionnairesService;
        private readonly IAppUserService appUserService;

        public QuestionnairesController(IQuestionnairesService questionnairesService, IAppUserService appUserService, IMapper mapper)
        {
            this.Mapper = mapper;
            this.questionnairesService = questionnairesService;
            this.appUserService = appUserService;
        }

        public async Task<ActionResult> Index()
        {
            var questViewModels = await GetQuestionnaireViewModels();
            ViewData["IsPropertyOwner"] =
                (await appUserService.GetUserRole(User.Identity.Name)) == "PropertyOwner";
            return View(questViewModels);
        }

        private async Task<List<QuestionnaireViewModel>> GetQuestionnaireViewModels()
        {
            var quests = this.questionnairesService.GetAllQuestionnaires().OrderByDescending(q => q.DateTimeCreated);
            var questViewModels = this.Mapper.Map<List<QuestionnaireViewModel>>(quests);
            var userId = await this.appUserService.GetUserIdByUserNameAsync(this.User.Identity.Name);
            foreach (var questionnaire in questViewModels)
            {
                var votes = (await questionnairesService.GetVotes(questionnaire.Id)).ToList();
                var isActive = await questionnairesService.IsActive(questionnaire.Id);
                questionnaire.Likes = votes.Count;
                questionnaire.IsActive = isActive;
                questionnaire.Voted = votes.Any(v => v.UserId == userId && v.Agrees);
            }

            return questViewModels;
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("Question")] QuestionnaireViewModel quest)
        {
            if (ModelState.IsValid)
            {
                var userName = User.Identity.Name;
                await questionnairesService.AddAsync(quest.Question, userName);

                return RedirectToAction("Index");
            }

            return View(quest);

        }

        public async Task<ActionResult> Edit(int id)
        {
            var quest = await questionnairesService.GetQuestionnaireByIdAsync(id);

            if (quest == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<QuestionnaireViewModel>(quest));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, string question)
        {
            await questionnairesService.UpdateAsync(id, question);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Delete(int id)
        {
            await questionnairesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ToggleVote(int id)
        {
            await questionnairesService.ToggleVoteAsync(id, User.Identity.Name);

            return RedirectToAction("Index", "Questionnaires");
        }

        public async Task<IActionResult> GetAll()
        {
            if (User.Identity.IsAuthenticated)
            {
                var questViewModels = await GetQuestionnaireViewModels();

                return Json(questViewModels);
            }

            return Json(new List<QuestionnaireViewModel>());
        }
    }
}
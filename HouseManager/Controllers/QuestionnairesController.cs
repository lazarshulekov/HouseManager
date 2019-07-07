using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace HouseManager.Controllers
{
    using BLL;

    using Persistence.Models;

    public class QuestionnairesController : Controller
    {
        private readonly IQuestionnairesService questionnairesService;

        private readonly IAppUserService appUserService;

        public QuestionnairesController(IQuestionnairesService questionnairesService, IAppUserService appUserService)
        {
            questionnairesService = questionnairesService;
            appUserService = appUserService;
        }

        // GET: Questionnaires
        public ActionResult Index()
        {
            var quests = questionnairesService.GetAllQuestionnaires();

            return View(quests);
        }

        // GET: Questionnaires/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Questionnaires/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questionnaires/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string question)
        {
            var userId = await appUserService.GetUserIdByUserName(User.Identity.Name);

            var quest = new Questionnaire() { UserId = userId, DateTimeCreated = DateTime.Now, Question = question };

            await questionnairesService.AddAsync(quest);

            return RedirectToAction("Index");

        }

        // GET: Questionnaires/Edit/5
        public ActionResult Edit(int id)
        {
            var quest = questionnairesService.GetQuestionnaireByIdAsync(id);

            if (quest == null)
            {
                return NotFound();
            }

            return View(quest);
        }

        // POST: Questionnaires/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, string quest)
        {
            var questEntity = await questionnairesService.GetQuestionnaireByIdAsync(id);

            questEntity.Question = quest;

            await questionnairesService.UpdateAsync(questEntity);

            return RedirectToAction("Index");
        }

        // GET: Questionnaires/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await questionnairesService.DeleteAsync(id);

            return RedirectToAction("Index");
        }

        //// POST: Questionnaires/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
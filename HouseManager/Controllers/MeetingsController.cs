using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HouseManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using BLL;

    using DAL.Migrations;
    using DAL.Models;

    using global::AutoMapper;

    using HouseManager.ViewModels;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class MeetingsController : Controller
    {
        private readonly IMapper mapper;

        private readonly IMeetingService meetingService;

        private readonly IQuestionnairesService questionnairesService;

        public MeetingsController(IMapper mapper, IMeetingService meetingService, IQuestionnairesService questionnairesService)
        {
            this.mapper = mapper;
            this.meetingService = meetingService;
            this.questionnairesService = questionnairesService;
        }
        // GET: Meetings
        public async Task<ActionResult> Index()
        {
            var meetings = await meetingService.GetAllMeetings();
            var meetingsVm = mapper.Map<List<MeetingViewModel>>(meetings.OrderByDescending(m => m.DateTime));

            return View(meetingsVm);
        }

        // GET: Meetings/Create
        public async Task<ActionResult> Create()
        {
            var quests = questionnairesService.GetAllQuestionnaires().OrderByDescending(q => q.DateTimeCreated);
            var questViewModels = mapper.Map<List<MeetingQuestionnaireViewModel>>(quests);
            foreach (var questionnaire in questViewModels)
            {
                var votes = (await questionnairesService.GetVotes(questionnaire.Id)).ToList();
                questionnaire.Likes = votes.Count;
            }

            ViewBag.AllIssues = questViewModels;
            return View();
        }

        // POST: Meetings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(
            [Bind("MeetingId,Comments,Datetime,Location,SelectedIssues")]
            MeetingViewModel meeting)
        {
            if (!ModelState.IsValid) return View(meeting);

            var meetingEntity = mapper.Map<Meeting>(meeting);

            await meetingService.AddAsync(meetingEntity);

            return RedirectToAction("Index");

        }

        // GET: Meetings/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var meeting = await meetingService.GetMeetingByIdAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            var quests = questionnairesService.GetAllQuestionnaires().OrderByDescending(q => q.DateTimeCreated);
            var questViewModels = mapper.Map<List<MeetingQuestionnaireViewModel>>(quests);
            foreach (var questionnaire in questViewModels)
            {
                var votes = (await questionnairesService.GetVotes(questionnaire.Id)).ToList();
                questionnaire.Likes = votes.Count;
            }

            var meetingIssues = meeting.MeetingsIssues != null ? meeting.MeetingsIssues.Select(q => q.IssueId): new List<int>();

             MultiSelectList options =
                new MultiSelectList(questViewModels, "Id", "QuestionAndLikes", meetingIssues);
            ViewBag.AllIssues = options;

            var vm = mapper.Map<MeetingViewModel>(meeting);

            return View(vm);
        }

        // POST: Meetings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Comments,Datetime,Location,SelectedIssues")] MeetingViewModel meeting)
        {
            if (ModelState.IsValid)
            {
                var meetingEntity = mapper.Map<Meeting>(meeting);
                await meetingService.UpdateAsync(meetingEntity);

                return RedirectToAction("Index");
            }
            return View(meeting);
        }

        // GET: Meetings/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await meetingService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
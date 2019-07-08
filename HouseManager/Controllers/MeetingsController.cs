using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HouseManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using BLL;

    using global::AutoMapper;

    using HouseManager.ViewModels;

    using Persistence.Models;

    public class MeetingsController : Controller
    {
        private readonly IMapper mapper;

        private readonly IMeetingService meetingService;

        public MeetingsController(IMapper mapper, IMeetingService meetingService)
        {
            this.mapper = mapper;
            this.meetingService = meetingService;
        }
        // GET: Meetings
        public async Task<ActionResult> Index()
        {
            var meetings = await meetingService.GetAllMeetings();
            var meetingsVm = mapper.Map<List<MeetingViewModel>>(meetings.OrderByDescending(m => m.DateTime));

            return View(meetingsVm);
        }

        // GET: Meetings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meetings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("MeetingId,Comments,Datetime,Location")] MeetingViewModel meeting)
        {
            if (ModelState.IsValid)
            {
                var meetingEntity = new Meeting()
                                        {
                                            Comments = meeting.Comments,
                                            DateTime = meeting.Datetime,
                                            Location = meeting.Location
                                        };

                await meetingService.AddAsync(meetingEntity);

                return RedirectToAction("Index");
            }

            return View(meeting);

        }

        // GET: Meetings/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var meeting = await meetingService.GetMeetingByIdAsync(id);

            if (meeting == null)
            {
                return NotFound();
            }

            return View(Mapper.Map<MeetingViewModel>(meeting));
        }

        // POST: Meetings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("Id,Comments,Datetime,Location")] MeetingViewModel meeting)
        {
            var meetingEntity = mapper.Map<Meeting>(meeting);
            await meetingService.UpdateAsync(meetingEntity);

            return RedirectToAction("Index");
        }

        // GET: Meetings/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            await meetingService.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}
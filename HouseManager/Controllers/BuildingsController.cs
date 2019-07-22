namespace HouseManager.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using BLL;

    using DAL.Models;

    using global::AutoMapper;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using BuildingViewModel = HouseManager.ViewModels.BuildingViewModel;

    [Authorize(Roles = "Administrator")]
    public class BuildingsController : Controller
    {
        private readonly IBuildingService bldService;

        private readonly IAppUserService userService;

        private readonly IMapper mapper;

        public BuildingsController(IBuildingService bldService, IAppUserService userService, IMapper mapper)
        {
            this.bldService = bldService;
            this.userService = userService;
            this.mapper = mapper;
        }

        // GET: Buildings
        public IActionResult Index()
        {
            var buildings = bldService.GetAllBuildings();
            return View(buildings);
        }

        // GET: Buildings/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AllHouseManagers = await userService.GetHouseManagersAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,City,Street,Number,SelectedManagers")]
            BuildingViewModel building)
        {
            if (ModelState.IsValid)
            {
                var bhm = building.SelectedManagers.Select(
                    b => new BuildingHousemanagers() { BuildingId = building.Id, HouseManagerId = b });

                var bld = new Building()
                              {
                                  Id = building.Id,
                                  City = building.City,
                                  Number = building.Number,
                                  Street = building.Street,
                                  BuildingHouseManagers = bhm.ToList()
                              };

                await bldService.AddAsync(bld);
                return RedirectToAction("Index");
            }

            return View(building);
        }

        // GET: Buildings/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var building = await bldService.GetBuildingByIdAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            var buildingHouseManagers = await userService.GetBuildingManagersAsync(id);
            var allHouseManagers = await userService.GetHouseManagersAsync();
            MultiSelectList options = 
                new MultiSelectList(allHouseManagers, "Id", "Email", buildingHouseManagers.Select(bhm => bhm.Id));
            ViewBag.AllHouseManagers = options;

            BuildingViewModel vm = new BuildingViewModel()
                                       {
                                           Id = building.Id,
                                           BuildingHouseManagers = building.BuildingHouseManagers,
                                           City = building.City,
                                           Number = building.Number,
                                           Street = building.Street
                                       };

            return View(vm);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Street,Number,SelectedManagers")] BuildingViewModel building)
        {
            if (id != building.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var buildingEntity = this.mapper.Map<Building>(building);

                await bldService.UpdateAsync(buildingEntity);

                return RedirectToAction("Index");
            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public async Task<IActionResult> Delete(int id)
        {

            var building = await bldService.GetBuildingByIdAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await bldService.DeleteAsync(id);

            return RedirectToAction("Index");

        }
    }
}

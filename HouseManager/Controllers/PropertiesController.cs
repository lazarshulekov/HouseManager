using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HouseManager.Controllers
{
    using System.Collections.Generic;

    using BLL;
    using DAL.Models;
    using global::AutoMapper;
    using HouseManager.ViewModels;

    public class PropertiesController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IPropertyTypeService propertyTypeService;
        private readonly IBuildingService buildingService;
        private readonly IAppUserService appUserService;
        private readonly IMapper mapper;

        public PropertiesController(
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            IAppUserService appUserService,
            IMapper mapper,
            IBuildingService buildingService)
        {
            this.propertyService = propertyService;
            this.propertyTypeService = propertyTypeService;
            this.appUserService = appUserService;
            this.mapper = mapper;
            this.buildingService = buildingService;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var properties = await propertyService.GetAllPropertiesAsync();
            var vm = mapper.Map<IEnumerable<Property>, IEnumerable<PropertyViewModel>>(properties);

            return View(vm);
        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        // GET: Properties/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.AllUsers = await appUserService.GetAllAppUsersAsync();
            ViewBag.AllPropTypes = await propertyTypeService.GetAllPropertyTypesAsync();
            ViewBag.AllBuildings = buildingService.GetAllBuildings().Select(x => new {Id = x.Id, Name = $"{x.Id} {x.City} {x.Street} {x.Number}"});

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Area,Comments,AppUserId,PropertyTypeId, BuildingId")] PropertyViewModel propertyVm)
        {
            if (ModelState.IsValid)
            {
                var property = mapper.Map<PropertyViewModel, Property>(propertyVm);
                await propertyService.AddAsync(property, propertyVm.BuildingId);

                return RedirectToAction("Index");
            }

            ViewBag.AllUsers = await appUserService.GetAllAppUsersAsync();
            ViewBag.AllPropTypes = await propertyTypeService.GetAllPropertyTypesAsync();
            ViewBag.AllBuildings = buildingService.GetAllBuildings().Select(x => new { Id = x.Id, Name = $"{x.Id} {x.City} {x.Street} {x.Number}" });

            return View(propertyVm);
        }

        //    // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var property = await propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            ViewBag.AllUsers = await appUserService.GetAllAppUsersAsync();
            ViewBag.AllPropTypes = await propertyTypeService.GetAllPropertyTypesAsync();

            var vm = mapper.Map<Property, PropertyViewModel>(property);

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Area,Comments")] PropertyViewModel propertyVm)
        {
            if (id != propertyVm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var property = mapper.Map<PropertyViewModel, Property>(propertyVm);
                await propertyService.UpdateAsync(property);
                
                return RedirectToAction("Index");
            }
            ViewBag.AllUsers = await appUserService.GetAllAppUsersAsync();
            ViewBag.AllPropTypes = await propertyTypeService.GetAllPropertyTypesAsync();
            return View(propertyVm);
        }

        //    // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var property = await propertyService.GetPropertyByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }


            var vm = mapper.Map<Property, PropertyViewModel>(property);
            return View(vm);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await propertyService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetPropertiesByBuildingId(int id)
        {
            var properties = await propertyService.GetAllPropertiesByBuildingIdAsync(id);
            var vm = mapper.Map<IList<Property>, IList<PropertyViewModel>>(properties.ToList());
            var items = vm.Select(p => new SelectListItem(){Text = $"{p.AppUser.Email} {p.PropertyType.Type} {p.Area}", Value = p.Id.ToString()}).ToList();
            
            return Json(items);
        }
    }
}

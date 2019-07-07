using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Persistence.Models;

namespace HouseManager.Controllers
{
    using System;

    using BLL;
    using BLL.Models;

    using DAL.Models;

    using global::AutoMapper;

    using HouseManager.ViewModels;

    public class ExpensesController : Controller
    {
        private readonly IBuildingService buildingService;

        private readonly IPropertyService propertyService;

        private readonly IExpensesService expensesService;

        private readonly IPropertyTypeService propertyTypeService;

        private readonly IAppUserService appUserService;

        private readonly IMapper mapper;

        public ExpensesController(
            IPropertyService propertyService,
            IPropertyTypeService propertyTypeService,
            IAppUserService appUserService,
            IMapper mapper,
            IBuildingService buildingService,
            IExpensesService expensesService)
        {
            propertyService = propertyService;
            propertyTypeService = propertyTypeService;
            appUserService = appUserService;
            mapper = mapper;
            buildingService = buildingService;
            expensesService = expensesService;
        }

        // GET: Properties
        public async Task<IActionResult> Index()
        {
            var userName = User.Identity.Name;
            var expenses = expensesService.GetExpensesPerUserName(userName);
            ViewData["IsPropertyOwner"] =
                (await appUserService.GetUserRole(User.Identity.Name)) == "PropertyOwner";

            var exps = expenses.Select(
                ex => new ExpenseViewModel()
                          {
                              BuildingId = ex.Building.Id,
                              BuildingView =
                                  new IdNameViewModel()
                                      {
                                          Id = ex.Building.Id,
                                          Name = $"{ex.Building.City} {ex.Building.Street} {ex.Building.Number}"
                                      },
                              ExpenseId = ex.Expense.Id,
                              Expense = ex.Expense.Name,
                              IsPaid = ex.Expense.IsPaid,
                              CreationDate = ex.Expense.CreationDate,
                              PropertyId = ex.Property.Id,
                              PropertyViewModel = new IdNameViewModel()
                                                      {
                                                          Id = ex.Property.Id,
                                                          Name =
                                                              $"{ex.Property.AppUser.FirstName} {ex.Property.AppUser.LastName} {ex.Property.PropertyType.Type} {ex.Property.Area} {ex.Property.Comments}"
                                                      }
                          });

            return View(exps);
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
            var allBuildings = buildingService.GetAllBuildings();

            ViewBag.AllBuildings = allBuildings.Select(
                b => new IdNameViewModel() { Id = b.Id, Name = $"{b.City} {b.Street} {b.Number}" });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("BuildingId,PropertyId,Expense")]
            ExpenseViewModel expenseModel)
        {
            if (ModelState.IsValid)
            {
                await expensesService.AddAsync(
                    new PropertyExpense()
                        {
                            Building = new Building() { Id = expenseModel.BuildingId },
                            Expense = new Expense() { Name = expenseModel.Expense, CreationDate = DateTime.Now},
                            Property = new Property() { Id = expenseModel.PropertyId },
                        });

                return RedirectToAction("Index");
            }

            ViewBag.AllUsers = await appUserService.GetAllAppUsersAsync();
            ViewBag.AllPropTypes = await propertyTypeService.GetAllPropertyTypesAsync();
            return View(expenseModel);
        }

        //    // GET: Properties/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var expense = expensesService.GetExpenseById(id);
            if (expense == null)
            {
                return NotFound();
            }

            //ViewBag.AllUsers = await appUserService.GetAllAppUsersAsync();
            //ViewBag.AllPropTypes = await propertyTypeService.GetAllPropertyTypesAsync();
            return View(expense);
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

        public async Task<IActionResult> TogglePayment(int id)
        {
            await expensesService.TogglepaymentAsync(id);

            return RedirectToAction("Index", "Expenses");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await expensesService.DeleteByIdAsync(id);
            return RedirectToAction("Index");
        }
    }
}

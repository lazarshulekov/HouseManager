namespace HouseManager.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using BLL;
    using BLL.Models;

    using DAL.Models.Identity;

    using global::AutoMapper;

    using HouseManager.ViewModels;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class AccountController : Controller
    {
        

        private readonly IMapper mapper;

        private readonly IAppUserService appUserService;

        public AccountController(
            IMapper mapper,
            IAppUserService appUserService)
        {
            this.mapper = mapper;
            this.appUserService = appUserService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["IsPropertyOwner"] =
                (await appUserService.GetUserRole(User.Identity.Name)) == "PropertyOwner";
            var users = await appUserService.GetAllAppUsersAsync();
            return View(users);
        }

        public async Task<IActionResult> HouseManagers()
        {
            var users = await appUserService.GetHouseManagersAsync();
            var vm = mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserViewModel>>(users);
            return View(vm);
        }

        public async Task<IActionResult> PropertyOwners()
        {
            var users = await appUserService.GetHouseManagersAsync();
            var vm = mapper.Map<IEnumerable<AppUser>, IEnumerable<AppUserViewModel>>(users);
            return View(vm);
        }

        public IActionResult LogIn(string returnUrl)
        {
            return View(new LogInViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LogInViewModel loginData)
        {
            var user = new AppUser { Email = loginData.UserName, Password = loginData.Password };
            var result = await appUserService.SignInAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return View(loginData);
        }

        public async Task<IActionResult> Logout()
        {
            await appUserService.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUserViewModel appUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var userEntity = mapper.Map<AppUserViewModel, AppUser>(appUserViewModel);
                var result = await appUserService.RegisterUserAsync(userEntity);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                foreach (var identityError in result.Errors)
                {
                    ModelState.AddModelError("", identityError.Description);
                }

            }

            return View(appUserViewModel);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult CreateHouseManager()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateHouseManager(
            [Bind("Email,Password,FirstName,LastName,Phone")] AppUserViewModel user)
        {
            var userEntity = mapper.Map<AppUserViewModel, AppUser>(user);
            //var result = await _userManager.CreateAsync(userEntity, userEntity.Password);

            var result = await appUserService.CreateHouseManagerAsync(userEntity);

            if (result.All(r => r.Succeeded))
            {
                return RedirectToAction("HouseManagers", "Account");
            }

            foreach (var identityError in result.SelectMany(x => x.Errors))
            {
                ModelState.AddModelError("", identityError.Description);
            }

            return View(user);
        }

        public IActionResult CreatePropertyOwner()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePropertyOwner(
            [Bind("Email,Password,FirstName,LastName,Phone")] AppUserViewModel user)
        {
            var userEntity = mapper.Map<AppUserViewModel, AppUser>(user);
            
            var result = await appUserService.CreatePropertyOwnerAsync(userEntity);

            if (result.All(r => r.Succeeded))
            {
                return RedirectToAction("Index", "Account");
            }

            foreach (var identityError in result.SelectMany(x => x.Errors))
            {
                ModelState.AddModelError("", identityError.Description);
            }

            return View(user);
        }

        public async Task<IActionResult> ToggleBanned(int id)
        {
            await appUserService.ToggleBannedAsync(id);

            return RedirectToAction("Index", "Account");
        }
    }
}

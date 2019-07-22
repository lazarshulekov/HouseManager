namespace BLL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;
    using DAL.Models.Identity;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AppUserService : IAppUserService
    {
        public enum UserRole
        {
            Administrator,
            HouseManager,
            PropertyOwner
        }

        private readonly AppDbContext auContext;

        private readonly UserManager<AppUser> userManager;

        private readonly SignInManager<AppUser> signInManager;

        public AppUserService(AppDbContext auContext, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.auContext = auContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<List<IdentityResult>> RegisterUserAsync(AppUser user)
        {
            List<IdentityResult> results = new List<IdentityResult>();

            results.Add(await userManager.CreateAsync(user, user.Password));
            results.Add(await userManager.AddToRoleAsync(user, UserRole.Administrator.ToString()));
            return results;
        }

        public async Task AddAsync(AppUser usr)
        {
            auContext.AppUsers.Add(usr);
            await auContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(AppUser usr)
        {
            auContext.AppUsers.Remove(usr);
            await auContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllAppUsersAsync()
        {
            return await auContext.AppUsers.ToListAsync<AppUser>();
        }

        public async Task<IEnumerable<AppUser>> GetHouseManagersAsync()
        {
            var houseManagers =
                from c in auContext.AppUsers
                join o in auContext.UsersRoles on c.Id equals o.AppUserId
                join d in auContext.AppRoles on o.AppRoleId equals d.Id
                where d.Name == UserRole.HouseManager.ToString()
                select c;

            return await houseManagers.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetBuildingManagersAsync(int buildingId)
        {
            var buildingManagers = from c in auContext.AppUsers
                                   join o in auContext.UsersRoles on c.Id equals o.AppUserId
                                   join d in auContext.AppRoles on o.AppRoleId equals d.Id
                                   join t in auContext.BuildingHousemanagers on c.Id equals t.HouseManagerId
                                   where d.Name == UserRole.HouseManager.ToString() && t.BuildingId == buildingId
                select c;


            return await buildingManagers.ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetPropertyOwnersAsync()
        {
            var propertyOwners =
                from c in auContext.AppUsers
                join o in auContext.UsersRoles on c.Id equals o.AppUserId
                join d in auContext.AppRoles on o.AppRoleId equals d.Id
                where d.Name == UserRole.PropertyOwner.ToString()
                select c;

            return await propertyOwners.ToListAsync();
        }

        public async Task<List<IdentityResult>> CreateHouseManagerAsync(AppUser user)
        {
            List<IdentityResult> results = new List<IdentityResult>();

                results.Add(await userManager.CreateAsync(user, user.Password));
                results.Add(await userManager.AddToRoleAsync(user, UserRole.HouseManager.ToString()));
            return results;
        }

        public async Task<List<IdentityResult>> CreatePropertyOwnerAsync(AppUser user)
        {
            List<IdentityResult> results = new List<IdentityResult>();

            results.Add(await userManager.CreateAsync(user, user.Password));
            results.Add(await userManager.AddToRoleAsync(user, UserRole.PropertyOwner.ToString()));
            return results;
        }

        public Task<AppUser> GetAppUserByIdAsync(int usrId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AppUser> GetAppUserByUserNameAsync(string userName)
        {
            return await userManager.FindByNameAsync(userName);
        }

        public Task UpdateAsync(AppUser usr)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SignInResult> SignInAsync(AppUser user)
        {
            return await signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<string> GetUserRole(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);

            var userRoles = await userManager.GetRolesAsync(user);

            return userRoles.Single();
        }

        public async Task<int> GetUserIdByUserNameAsync(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);
            return user.Id;
        }

        public async Task ToggleBannedAsync(int id)
        {
            var userEntity = await auContext.AppUsers.FindAsync(id);
            userEntity.Banned = !userEntity.Banned;

            await auContext.SaveChangesAsync();
        }
    }
}
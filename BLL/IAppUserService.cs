namespace BLL
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models.Identity;

    using Microsoft.AspNetCore.Identity;

    public interface IAppUserService
    {
        Task AddAsync(AppUser usr);

        Task DeleteAsync(AppUser usr);

        Task<IEnumerable<AppUser>> GetAllAppUsersAsync();

        Task<AppUser> GetAppUserByIdAsync(int usrId);

        Task<AppUser> GetAppUserByUserNameAsync(string userName);

        Task UpdateAsync(AppUser usr);

        Task<IEnumerable<AppUser>> GetHouseManagersAsync();

        Task<IEnumerable<AppUser>> GetPropertyOwnersAsync();

        Task<List<IdentityResult>> CreateHouseManagerAsync(AppUser user);

        Task<List<IdentityResult>> CreatePropertyOwnerAsync(AppUser user);

        Task<IdentityResult> RegisterUserAsync(AppUser user);

        Task<SignInResult> SignInAsync(AppUser user);

        Task SignOutAsync();

        Task<IEnumerable<AppUser>> GetBuildingManagersAsync(int buildingId);

        Task<string> GetUserRole(string userName);

        Task<int> GetUserIdByUserNameAsync(string userName);

        Task ToggleBannedAsync(int id);
    }
}
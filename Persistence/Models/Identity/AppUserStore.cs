namespace Persistence.Models.Identity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AppUserStore : IUserStore<AppUser>,
                                IUserPasswordStore<AppUser>,
                                IUserRoleStore<AppUser>,
                                IUserEmailStore<AppUser>
    {
        private readonly AppDbContext dbContext;

        public AppUserStore(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        #region IUserStore

        public async Task<string> GetUserIdAsync(AppUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }

        public async Task<string> GetUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task SetUserNameAsync(AppUser user, string userName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Email = userName;
            return Task.CompletedTask;

        }

        public async Task<string> GetNormalizedUserNameAsync(AppUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(AppUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Email = normalizedName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await dbContext.AppUsers.AddAsync(user, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            dbContext.Update(user);
            await dbContext.SaveChangesAsync(cancellationToken);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            dbContext.AppUsers.Remove(user);
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<AppUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("UserId empty");
            }
            
            return await dbContext.AppUsers.FindAsync(Convert.ToInt32(userId));
        }

        public async Task<AppUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(normalizedUserName))
            {
                throw new ArgumentNullException("UserId empty");
            }

            return await dbContext.AppUsers.FirstOrDefaultAsync(
                       p => p.Email == normalizedUserName,
                       cancellationToken);
        }

        #endregion

        #region IUserPasswordStore

        public Task SetPasswordHashAsync(AppUser user, string passwordHash, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Password = passwordHash);
        }

        public async Task<string> GetPasswordHashAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult<bool>(!string.IsNullOrEmpty(user.Password));
        }

        #endregion

        #region IUserRoleStore

        public async Task AddToRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var role = await dbContext.AppRoles.FirstOrDefaultAsync(x => x.Name == roleName, cancellationToken);
            var useRole = new AppUsersRoles() { AppUser = user, AppRole = role };
            await dbContext.UsersRoles.AddAsync(useRole, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var role = await dbContext.AppRoles.FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
            dbContext.UsersRoles.Remove(new AppUsersRoles() { AppUser = user, AppRole = role });
        }

        public async Task<IList<string>> GetRolesAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await dbContext.UsersRoles.Where(ur => ur.AppUser == user)
                       .Select(ur => ur.AppRole.Name)
                       .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(AppUser user, string roleName, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return await dbContext.UsersRoles.AnyAsync(
                       ur => ur.AppUser == user && ur.AppRole.Name == roleName,
                       cancellationToken);
        }

        public async Task<IList<AppUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await dbContext.UsersRoles.Where(ur => ur.AppRole.Name == roleName)
                       .Select(ur => ur.AppUser)
                       .ToListAsync(cancellationToken: cancellationToken);
        }

        #endregion

        #region IUserEmailStore

        public Task SetEmailAsync(AppUser user, string email, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email = email);
        }

        public Task<string> GetEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AppUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(AppUser user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<AppUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await dbContext.AppUsers.FirstOrDefaultAsync(u => u.Email == normalizedEmail,cancellationToken);
        }

        public async Task<string> GetNormalizedEmailAsync(AppUser user, CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(AppUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email = normalizedEmail);
        }

        #endregion
    }
}
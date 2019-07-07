namespace Persistence.Models.Identity
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public class AppRoleStore : IRoleStore<AppRole>
    {
        private readonly AppDbContext dbContext;

        public AppRoleStore(AppDbContext dbContext)
        {
            dbContext = dbContext;
        }

        public Task<IdentityResult> CreateAsync(AppRole role, CancellationToken cancellationToken)
        {
            if (role == null) throw new ArgumentException("Argument cannot be null or empty: AppRole.");

            dbContext.AppRoles.Add(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public Task<IdentityResult> DeleteAsync(AppRole role, CancellationToken cancellationToken)
        {
            dbContext.AppRoles.Remove(role);

            return Task.FromResult(IdentityResult.Success);
        }

        public void Dispose()
        {
        }

        public async Task<AppRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await dbContext.AppRoles.FindAsync(roleId);
        }

        public async Task<AppRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await dbContext.AppRoles.FirstOrDefaultAsync(
                       x => x.Name == normalizedRoleName,
                       cancellationToken);
        }

        public Task<string> GetNormalizedRoleNameAsync(AppRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(AppRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(AppRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(AppRole role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetRoleNameAsync(AppRole role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(AppRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
namespace DAL.Repositories
{
    using System;

    using Persistence.Models;
    using Persistence.Models.Identity;

    public class UnitOfWork : IDisposable
    {
        private readonly AppDbContext context;

        private GenericRepository<AppUser> appUserRepository;

        private GenericRepository<AppRole> appRoleRepository;

        public UnitOfWork(AppDbContext context)
        {
            context = context;
        }

        public GenericRepository<AppUser> AppUserRepository
        {
            get
            {
                if (appUserRepository == null)
                {
                    appUserRepository = new GenericRepository<AppUser>(context);
                }
                return appUserRepository;
            }
        }

        public GenericRepository<AppRole> AppRoleRepository
        {
            get
            {
                if (appRoleRepository == null)
                {
                    appRoleRepository = new GenericRepository<AppRole>(context);
                }
                return appRoleRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
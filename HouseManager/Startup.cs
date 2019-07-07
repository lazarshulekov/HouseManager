using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HouseManager
{
    using BLL;

    using global::AutoMapper;

    using Microsoft.Extensions.Configuration;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    using Persistence.Models;
    using Persistence.Models.Identity;

    public class Startup
    {
        private readonly IConfigurationRoot configurationRoot;

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            configurationRoot = new ConfigurationBuilder().SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(
                options => { options.UseSqlServer(configurationRoot.GetConnectionString("DefaultConnection")); });
            services.AddOptions();
            services.AddIdentity<AppUser, AppRole>(
                o =>
                    {
                        // configure identity options
                        o.Password.RequireDigit = false;
                        o.Password.RequireLowercase = false;
                        o.Password.RequireUppercase = false;
                        o.Password.RequireNonAlphanumeric = false;
                        o.Password.RequiredLength = 6;
                    });
            services.AddTransient<IUserStore<AppUser>, AppUserStore>();
            services.AddTransient<IRoleStore<AppRole>, AppRoleStore>();
            services.AddMvc();
            services.AddTransient<IBuildingService, BuildingService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyTypeService, PorpertyTypeService>();
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddSingleton<IExpensesService, ExpensesService>();

            services.AddLogging();
            //services.AddMemoryCache(x => x.ExpirationScanFrequency = TimeSpan.FromSeconds(20));
            services.AddSession();
            services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddProvider(new MyFilteredLoggerProvider());
            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseIdentity();
            app.UseSession();
            app.UseMvc(
                routes =>
                    {
                        routes.MapRoute(
                            "default", 
                            "{controller=Home}/{action=Index}/{id?}");
                    });

            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    serviceScope.ServiceProvider.GetService<AppDbContext>(); //.Database.Migrate();
            //}
            //EnsureDatabase(app);
        }

        private void EnsureDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.EnsureCreated();
                serviceScope.ServiceProvider.GetService<AppDbContext>().Database.Migrate();
                //serviceScope.ServiceProvider.GetService<AppDbContext>().EnsureSeedData();
            }
        }
    }
}

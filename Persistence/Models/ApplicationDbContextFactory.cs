

//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.Extensions.Configuration;

//using Persistence.Models;

//public class ApplicationDbContextFactory : IDbContextFactory<AppDbContext>
//{
//    private IConfiguration configuration;

//    public ApplicationDbContextFactory()
//    {
//        var builder = new ConfigurationBuilder().SetBasePath(System.AppContext.BaseDirectory)
//            .AddJsonFile("appsettings.json");
//        Configuration = builder.Build();
//    }

//    public IConfiguration Configuration
//    {
//        get => configuration;
//        set => configuration = value;
//    }

//    public AppDbContext Create(DbContextFactoryOptions options)
//    {
//        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
//        optionsBuilder.UseSqlServer(
//            Configuration.GetConnectionString("DefaultConnection"),
//            m => { m.EnableRetryOnFailure(); });

//        return new AppDbContext(optionsBuilder.Options);
//    }
//}
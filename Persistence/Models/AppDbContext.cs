namespace DAL.Models
{
    using DAL.Models.Identity;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;

    using Models;

    public class AppDbContext : DbContext
    {
        private readonly ILoggerFactory loggerFactory;

        public AppDbContext(DbContextOptions<AppDbContext> options, ILoggerFactory loggerFactory)
            : base(options)
        {
            loggerFactory = loggerFactory;
        }

        #region DbSets

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppRole> AppRoles { get; set; }

        public DbSet<AppUsersRoles> UsersRoles { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingProperties> BuildingProperties { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<Meeting> Meetings { get; set; }

        public DbSet<MeetingsQuestionnaires> MeetingsQuestionnaires { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Questionnaire> Questionnaires { get; set; }

        public DbSet<QuestionnaireUserVotes> QuestionnaireUserVotes { get; set; }

        public DbSet<PropertiesExpenses> PropertiesExpenses { get; set; }

        public DbSet<BuildingHousemanagers> BuildingHousemanagers { get; set; }

        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseLoggerFactory(this.loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>().HasKey(p => p.Id);

            modelBuilder.Entity<Property>().Property(p => p.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Property>()
                .HasOne(p => p.AppUser)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AppUserId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.PropertyType)
                .WithMany(pt => pt.Properties)
                .HasForeignKey(p => p.PropertyTypeId);

            modelBuilder.Entity<AppUser>(
                au =>
                    {
                        au.Property(p => p.FirstName).IsRequired();
                        au.Property(p => p.LastName).IsRequired();
                        au.Property(p => p.Email).IsRequired();
                        au.Property(p => p.Password).IsRequired();
                    });
            modelBuilder.Entity<AppRole>(ar => ar.Property(p => p.Name).IsRequired());

            modelBuilder.Entity<Expense>(e => e.Property(p => p.Name).IsRequired());

            modelBuilder.Entity<QuestionnaireUserVotes>(i => i.Property(p => p.Agrees).IsRequired());
            modelBuilder.Entity<PropertyType>(i => i.Property(p => p.Type).IsRequired());
            modelBuilder.Entity<Building>(
                b =>
                    {
                        b.Property(p => p.City).IsRequired();
                        b.Property(p => p.Street).IsRequired();
                        b.Property(p => p.Number).IsRequired();
                    });

            modelBuilder.Entity<Questionnaire>(
                q =>
                    {
                        q.Property(p => p.UserId).IsRequired();
                        q.Property(p => p.Question).IsRequired();
                        q.Property(p => p.DateTimeCreated).IsRequired();
                    });


            modelBuilder.Entity<Meeting>(
                m =>
                    {
                        m.Property(p => p.Location).IsRequired();
                        m.Property(p => p.DateTime).IsRequired();
                    });

            modelBuilder.Entity<AppUsersRoles>().HasKey(ur => new { ur.AppUserId, ur.AppRoleId });

            modelBuilder.Entity<AppUsersRoles>()
                .HasOne(bc => bc.AppUser)
                .WithMany(b => b.AppUsersRoles)
                .HasForeignKey(bc => bc.AppUserId);

            modelBuilder.Entity<AppUsersRoles>()
                .HasOne(bc => bc.AppRole)
                .WithMany(c => c.AppUsersRoles)
                .HasForeignKey(bc => bc.AppRoleId);

            modelBuilder.Entity<BuildingProperties>().HasKey(bp => new { bp.BuildingId, bp.PropertyId });

            modelBuilder.Entity<BuildingProperties>()
                .HasOne(bp => bp.Building)
                .WithMany(b => b.BuildingProperties)
                .HasForeignKey(b => b.BuildingId);

            modelBuilder.Entity<BuildingProperties>()
                .HasOne(bp => bp.Property)
                .WithMany(bp => bp.BuildingProperties)
                .HasForeignKey(bp => bp.PropertyId);

            modelBuilder.Entity<BuildingHousemanagers>().HasKey(hm => new { hm.BuildingId, hm.HouseManagerId });

            modelBuilder.Entity<BuildingHousemanagers>()
                .HasOne(bh => bh.Building)
                .WithMany(b => b.BuildingHouseManagers)
                .HasForeignKey(b => b.BuildingId);

            modelBuilder.Entity<BuildingHousemanagers>()
                .HasOne(bp => bp.HouseManager)
                .WithMany(b => b.BuildingHouseManagers)
                .HasForeignKey(bp => bp.HouseManagerId);

            //modelBuilder.Entity<Issue>(ar => ar.Property(p => p.Id).ValueGeneratedNever());

            modelBuilder.Entity<MeetingsQuestionnaires>().HasKey(mi => new { mi.QuestionnaireId, mi.MeetingId });

            modelBuilder.Entity<MeetingsQuestionnaires>()
                .HasOne(mi => mi.Questionnaire)
                .WithMany(mi => mi.MeetingsQuestionnaires)
                .HasForeignKey(mi => mi.QuestionnaireId);

            modelBuilder.Entity<MeetingsQuestionnaires>()
                .HasOne(mi => mi.Meeting)
                .WithMany(mi => mi.MeetingsQuestionnaires)
                .HasForeignKey(mi => mi.MeetingId);

            modelBuilder.Entity<PropertiesExpenses>().HasKey(ue => new { ue.ExpenseId, ue.PropertyId });

            modelBuilder.Entity<PropertiesExpenses>()
                .HasOne(ue => ue.Property)
                .WithMany(ue => ue.PropertiesExpenses)
                .HasForeignKey(ue => ue.PropertyId);

            modelBuilder.Entity<PropertiesExpenses>()
                .HasOne(ue => ue.Expense)
                .WithMany(ue => ue.PropertiesExpenses)
                .HasForeignKey(ue => ue.ExpenseId);

            modelBuilder.Entity<QuestionnaireUserVotes>().HasKey(quv => new { quv.UserId, quv.QuestionnaireId });

            modelBuilder.Entity<QuestionnaireUserVotes>()
                .HasOne(quv => quv.AppUser)
                .WithMany(quv => quv.QuestionnaireUserVotes)
                .HasForeignKey(quv => quv.UserId);

            modelBuilder.Entity<QuestionnaireUserVotes>()
                .HasOne(quv => quv.Questionnaire)
                .WithMany(quv => quv.QuestionnaireUserVotes)
                .HasForeignKey(quv => quv.QuestionnaireId);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    using DAL.Models;

    [DbContext(typeof(AppDbContext))]
    [Migration("20190624183431_DueDateRemove")]
    partial class DueDateRemove
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Persistence.Models.Building", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int>("Number");

                    b.Property<string>("Street")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Buildings");
                });

            modelBuilder.Entity("Persistence.Models.BuildingHousemanagers", b =>
                {
                    b.Property<int>("BuildingId");

                    b.Property<int>("HouseManagerId");

                    b.HasKey("BuildingId", "HouseManagerId");

                    b.HasIndex("HouseManagerId");

                    b.ToTable("BuildingHousemanagers");
                });

            modelBuilder.Entity("Persistence.Models.BuildingProperties", b =>
                {
                    b.Property<int>("BuildingId");

                    b.Property<int>("PropertyId");

                    b.HasKey("BuildingId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("BuildingProperties");
                });

            modelBuilder.Entity("Persistence.Models.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<bool>("IsPaid");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Persistence.Models.Identity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("Persistence.Models.Identity.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Banned");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<int>("Rank");

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("Persistence.Models.Identity.AppUsersRoles", b =>
                {
                    b.Property<int>("AppUserId");

                    b.Property<int>("AppRoleId");

                    b.HasKey("AppUserId", "AppRoleId");

                    b.HasIndex("AppRoleId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("Persistence.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("Persistence.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("DateAndTime");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("Persistence.Models.MeetingsIssues", b =>
                {
                    b.Property<int>("IssueId");

                    b.Property<int>("MeetingId");

                    b.HasKey("IssueId", "MeetingId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingsIssues");
                });

            modelBuilder.Entity("Persistence.Models.PropertiesExpenses", b =>
                {
                    b.Property<int>("ExpenseId");

                    b.Property<int>("PropertyId");

                    b.Property<DateTime>("CreationDate");

                    b.HasKey("ExpenseId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertiesExpenses");
                });

            modelBuilder.Entity("Persistence.Models.Property", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AppUserId");

                    b.Property<decimal>("Area");

                    b.Property<string>("Comments");

                    b.Property<int>("PropertyTypeId");

                    b.HasKey("Id");

                    b.HasIndex("AppUserId");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("Properties");
                });

            modelBuilder.Entity("Persistence.Models.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("Persistence.Models.Questionnaire", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CreatedByAppUserId");

                    b.Property<DateTime>("DateTimeCreated");

                    b.Property<string>("Question")
                        .IsRequired();

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByAppUserId");

                    b.ToTable("Questionnaires");
                });

            modelBuilder.Entity("Persistence.Models.QuestionnaireUserComments", b =>
                {
                    b.Property<int>("QuestionnaireId");

                    b.Property<int>("AppUserId");

                    b.Property<string>("Comment")
                        .IsRequired();

                    b.Property<DateTime>("CommentDate");

                    b.HasKey("QuestionnaireId", "AppUserId");

                    b.HasIndex("AppUserId");

                    b.ToTable("QuestionnaireUserComments");
                });

            modelBuilder.Entity("Persistence.Models.QuestionnaireUserVotes", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("QuestionnaireId");

                    b.Property<bool>("Agrees");

                    b.HasKey("UserId", "QuestionnaireId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("QuestionnaireUserVotes");
                });

            modelBuilder.Entity("Persistence.Models.UsersFavouriteQuestionnaires", b =>
                {
                    b.Property<int>("AppUserId");

                    b.Property<int>("QuestionnaireId");

                    b.HasKey("AppUserId", "QuestionnaireId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("UsersFavouriteQuestionnaires");
                });

            modelBuilder.Entity("Persistence.Models.BuildingHousemanagers", b =>
                {
                    b.HasOne("Persistence.Models.Building", "Building")
                        .WithMany("BuildingHouseManagers")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Identity.AppUser", "HouseManager")
                        .WithMany("BuildingHouseManagers")
                        .HasForeignKey("HouseManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.BuildingProperties", b =>
                {
                    b.HasOne("Persistence.Models.Building", "Building")
                        .WithMany("BuildingProperties")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Property", "Property")
                        .WithMany("BuildingProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.Identity.AppUsersRoles", b =>
                {
                    b.HasOne("Persistence.Models.Identity.AppRole", "AppRole")
                        .WithMany("AppUsersRoles")
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Identity.AppUser", "AppUser")
                        .WithMany("AppUsersRoles")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.MeetingsIssues", b =>
                {
                    b.HasOne("Persistence.Models.Issue", "Issue")
                        .WithMany("MeetingsIssues")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Meeting", "Meeting")
                        .WithMany("MeetingsIssues")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.PropertiesExpenses", b =>
                {
                    b.HasOne("Persistence.Models.Expense", "Expense")
                        .WithMany("PropertiesExpenses")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Property", "Property")
                        .WithMany("PropertiesExpenses")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.Property", b =>
                {
                    b.HasOne("Persistence.Models.Identity.AppUser", "AppUser")
                        .WithMany("Properties")
                        .HasForeignKey("AppUserId");

                    b.HasOne("Persistence.Models.PropertyType", "PropertyType")
                        .WithMany("Properties")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.Questionnaire", b =>
                {
                    b.HasOne("Persistence.Models.Identity.AppUser", "CreatedByAppUser")
                        .WithMany()
                        .HasForeignKey("CreatedByAppUserId");
                });

            modelBuilder.Entity("Persistence.Models.QuestionnaireUserComments", b =>
                {
                    b.HasOne("Persistence.Models.Identity.AppUser", "User")
                        .WithMany("QuestionnaireUserComments")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Questionnaire", "Questionnaire")
                        .WithMany("QuestionnaireUserComments")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.QuestionnaireUserVotes", b =>
                {
                    b.HasOne("Persistence.Models.Questionnaire", "Questionnaire")
                        .WithMany("QuestionnaireUserVotes")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Identity.AppUser", "AppUser")
                        .WithMany("QuestionnaireUserVotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Persistence.Models.UsersFavouriteQuestionnaires", b =>
                {
                    b.HasOne("Persistence.Models.Identity.AppUser", "AppUser")
                        .WithMany("UsersFavouriteQuestionnaires")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Persistence.Models.Questionnaire", "Questionnaire")
                        .WithMany("UsersFavouriteQuestionnaires")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

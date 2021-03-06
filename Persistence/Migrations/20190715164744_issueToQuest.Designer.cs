﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using DAL.Models;

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190715164744_issueToQuest")]
    partial class issueToQuest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Models.Building", b =>
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

            modelBuilder.Entity("DAL.Models.BuildingHousemanagers", b =>
                {
                    b.Property<int>("BuildingId");

                    b.Property<int>("HouseManagerId");

                    b.HasKey("BuildingId", "HouseManagerId");

                    b.HasIndex("HouseManagerId");

                    b.ToTable("BuildingHousemanagers");
                });

            modelBuilder.Entity("DAL.Models.BuildingProperties", b =>
                {
                    b.Property<int>("BuildingId");

                    b.Property<int>("PropertyId");

                    b.HasKey("BuildingId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("BuildingProperties");
                });

            modelBuilder.Entity("DAL.Models.Expense", b =>
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

            modelBuilder.Entity("DAL.Models.Identity.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AppRoles");
                });

            modelBuilder.Entity("DAL.Models.Identity.AppUser", b =>
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

                    b.HasKey("Id");

                    b.ToTable("AppUsers");
                });

            modelBuilder.Entity("DAL.Models.Identity.AppUsersRoles", b =>
                {
                    b.Property<int>("AppUserId");

                    b.Property<int>("AppRoleId");

                    b.HasKey("AppUserId", "AppRoleId");

                    b.HasIndex("AppRoleId");

                    b.ToTable("UsersRoles");
                });

            modelBuilder.Entity("DAL.Models.Meeting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Meetings");
                });

            modelBuilder.Entity("DAL.Models.MeetingsQuestionnaires", b =>
                {
                    b.Property<int>("QuestionnaireId");

                    b.Property<int>("MeetingId");

                    b.Property<bool>("Accepted");

                    b.HasKey("QuestionnaireId", "MeetingId");

                    b.HasIndex("MeetingId");

                    b.ToTable("MeetingsQuestionnaires");
                });

            modelBuilder.Entity("DAL.Models.PropertiesExpenses", b =>
                {
                    b.Property<int>("ExpenseId");

                    b.Property<int>("PropertyId");

                    b.Property<DateTime>("CreationDate");

                    b.HasKey("ExpenseId", "PropertyId");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertiesExpenses");
                });

            modelBuilder.Entity("DAL.Models.Property", b =>
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

            modelBuilder.Entity("DAL.Models.PropertyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("DAL.Models.Questionnaire", b =>
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

            modelBuilder.Entity("DAL.Models.QuestionnaireUserVotes", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("QuestionnaireId");

                    b.Property<bool>("Agrees");

                    b.HasKey("UserId", "QuestionnaireId");

                    b.HasIndex("QuestionnaireId");

                    b.ToTable("QuestionnaireUserVotes");
                });

            modelBuilder.Entity("DAL.Models.BuildingHousemanagers", b =>
                {
                    b.HasOne("DAL.Models.Building", "Building")
                        .WithMany("BuildingHouseManagers")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Identity.AppUser", "HouseManager")
                        .WithMany("BuildingHouseManagers")
                        .HasForeignKey("HouseManagerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.BuildingProperties", b =>
                {
                    b.HasOne("DAL.Models.Building", "Building")
                        .WithMany("BuildingProperties")
                        .HasForeignKey("BuildingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Property", "Property")
                        .WithMany("BuildingProperties")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.Identity.AppUsersRoles", b =>
                {
                    b.HasOne("DAL.Models.Identity.AppRole", "AppRole")
                        .WithMany("AppUsersRoles")
                        .HasForeignKey("AppRoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Identity.AppUser", "AppUser")
                        .WithMany("AppUsersRoles")
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.MeetingsQuestionnaires", b =>
                {
                    b.HasOne("DAL.Models.Meeting", "Meeting")
                        .WithMany("MeetingsIssues")
                        .HasForeignKey("MeetingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Questionnaire", "Questionnaire")
                        .WithMany("MeetingsQuestionnaires")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.PropertiesExpenses", b =>
                {
                    b.HasOne("DAL.Models.Expense", "Expense")
                        .WithMany("PropertiesExpenses")
                        .HasForeignKey("ExpenseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Property", "Property")
                        .WithMany("PropertiesExpenses")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.Property", b =>
                {
                    b.HasOne("DAL.Models.Identity.AppUser", "AppUser")
                        .WithMany("Properties")
                        .HasForeignKey("AppUserId");

                    b.HasOne("DAL.Models.PropertyType", "PropertyType")
                        .WithMany("Properties")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DAL.Models.Questionnaire", b =>
                {
                    b.HasOne("DAL.Models.Identity.AppUser", "CreatedByAppUser")
                        .WithMany()
                        .HasForeignKey("CreatedByAppUserId");
                });

            modelBuilder.Entity("DAL.Models.QuestionnaireUserVotes", b =>
                {
                    b.HasOne("DAL.Models.Questionnaire", "Questionnaire")
                        .WithMany("QuestionnaireUserVotes")
                        .HasForeignKey("QuestionnaireId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("DAL.Models.Identity.AppUser", "AppUser")
                        .WithMany("QuestionnaireUserVotes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

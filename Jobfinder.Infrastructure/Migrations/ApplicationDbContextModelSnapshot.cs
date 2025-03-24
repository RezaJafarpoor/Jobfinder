﻿// <auto-generated />
using System;
using Jobfinder.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jobfinder.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Jobfinder.Domain.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<Guid>("Owner")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SizeOfCompany")
                        .HasColumnType("int");

                    b.Property<string>("WebsiteAddress")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.Cv", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("BirthDay")
                        .HasColumnType("date");

                    b.Property<Guid>("JobSeekerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("MaximumExpectedSalary")
                        .HasColumnType("int");

                    b.Property<int?>("MinimumExpectedSalary")
                        .HasColumnType("int");

                    b.Property<int>("ServiceStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("Cvs");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.EmployerProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmployerProfiles");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobApplication", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AppliedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JobOfferId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobSeekerProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("JobOfferId");

                    b.HasIndex("JobSeekerProfileId");

                    b.ToTable("JobApplications");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("JobCategories");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("EmployerProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("JobName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("EmployerProfileId");

                    b.ToTable("JobOffers");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobSeekerProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CvId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CvId")
                        .IsUnique();

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("JobSeekerProfiles");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.Company", b =>
                {
                    b.OwnsOne("Jobfinder.Domain.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("CompanyId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(1500)
                                .HasColumnType("nvarchar(1500)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("CompanyId");

                            b1.ToTable("Companies");

                            b1.WithOwner()
                                .HasForeignKey("CompanyId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.Cv", b =>
                {
                    b.OwnsOne("Jobfinder.Domain.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("CvId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Address")
                                .IsRequired()
                                .HasMaxLength(1500)
                                .HasColumnType("nvarchar(1500)");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("nvarchar(200)");

                            b1.Property<string>("Province")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("CvId");

                            b1.ToTable("Cvs");

                            b1.WithOwner()
                                .HasForeignKey("CvId");
                        });

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.EmployerProfile", b =>
                {
                    b.HasOne("Jobfinder.Domain.Entities.Company", "Company")
                        .WithOne()
                        .HasForeignKey("Jobfinder.Domain.Entities.EmployerProfile", "CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobfinder.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Jobfinder.Domain.Entities.EmployerProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobApplication", b =>
                {
                    b.HasOne("Jobfinder.Domain.Entities.JobOffer", "JobOffer")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobOfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobfinder.Domain.Entities.JobSeekerProfile", "JobSeekerProfile")
                        .WithMany("JobApplications")
                        .HasForeignKey("JobSeekerProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobOffer");

                    b.Navigation("JobSeekerProfile");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobOffer", b =>
                {
                    b.HasOne("Jobfinder.Domain.Entities.JobCategory", "Category")
                        .WithMany("JobOffers")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobfinder.Domain.Entities.EmployerProfile", null)
                        .WithMany("JobOffers")
                        .HasForeignKey("EmployerProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Jobfinder.Domain.ValueObjects.JobDetails", "JobDetails", b1 =>
                        {
                            b1.Property<Guid>("JobOfferId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("ContractType")
                                .HasColumnType("int");

                            b1.Property<bool>("IsRemote")
                                .HasColumnType("bit");

                            b1.Property<int?>("MaximumAge")
                                .HasColumnType("int");

                            b1.Property<int?>("MinimumAge")
                                .HasColumnType("int");

                            b1.HasKey("JobOfferId");

                            b1.ToTable("JobOffers");

                            b1.WithOwner()
                                .HasForeignKey("JobOfferId");

                            b1.OwnsOne("Jobfinder.Domain.ValueObjects.Location", "Location", b2 =>
                                {
                                    b2.Property<Guid>("JobDetailsJobOfferId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<string>("Address")
                                        .IsRequired()
                                        .HasMaxLength(1500)
                                        .HasColumnType("nvarchar(1500)");

                                    b2.Property<string>("City")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)");

                                    b2.Property<string>("Province")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("nvarchar(50)");

                                    b2.HasKey("JobDetailsJobOfferId");

                                    b2.ToTable("JobOffers");

                                    b2.WithOwner()
                                        .HasForeignKey("JobDetailsJobOfferId");
                                });

                            b1.OwnsOne("Jobfinder.Domain.ValueObjects.WorkingDatsAndHours", "WorkingDatsAndHours", b2 =>
                                {
                                    b2.Property<Guid>("JobDetailsJobOfferId")
                                        .HasColumnType("uniqueidentifier");

                                    b2.Property<int>("FinishingHour")
                                        .HasColumnType("int");

                                    b2.Property<int>("From")
                                        .HasColumnType("int");

                                    b2.Property<int>("StartingHour")
                                        .HasColumnType("int");

                                    b2.Property<int>("To")
                                        .HasColumnType("int");

                                    b2.HasKey("JobDetailsJobOfferId");

                                    b2.ToTable("JobOffers");

                                    b2.WithOwner()
                                        .HasForeignKey("JobDetailsJobOfferId");
                                });

                            b1.Navigation("Location")
                                .IsRequired();

                            b1.Navigation("WorkingDatsAndHours")
                                .IsRequired();
                        });

                    b.OwnsOne("Jobfinder.Domain.ValueObjects.Salary", "Salary", b1 =>
                        {
                            b1.Property<Guid>("JobOfferId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("From")
                                .HasColumnType("int");

                            b1.Property<int>("To")
                                .HasColumnType("int");

                            b1.HasKey("JobOfferId");

                            b1.ToTable("JobOffers");

                            b1.WithOwner()
                                .HasForeignKey("JobOfferId");
                        });

                    b.Navigation("Category");

                    b.Navigation("JobDetails")
                        .IsRequired();

                    b.Navigation("Salary")
                        .IsRequired();
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobSeekerProfile", b =>
                {
                    b.HasOne("Jobfinder.Domain.Entities.Cv", "JobSeekerCv")
                        .WithOne()
                        .HasForeignKey("Jobfinder.Domain.Entities.JobSeekerProfile", "CvId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Jobfinder.Domain.Entities.User", "User")
                        .WithOne()
                        .HasForeignKey("Jobfinder.Domain.Entities.JobSeekerProfile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("JobSeekerCv");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Jobfinder.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.EmployerProfile", b =>
                {
                    b.Navigation("JobOffers");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobCategory", b =>
                {
                    b.Navigation("JobOffers");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobOffer", b =>
                {
                    b.Navigation("JobApplications");
                });

            modelBuilder.Entity("Jobfinder.Domain.Entities.JobSeekerProfile", b =>
                {
                    b.Navigation("JobApplications");
                });
#pragma warning restore 612, 618
        }
    }
}

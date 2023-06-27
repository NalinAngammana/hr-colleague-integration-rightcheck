﻿// <auto-generated />
using System;
using ColleagueInt.RTW.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ColleagueInt.RTW.Database.Migrations
{
    [DbContext(typeof(RTWContext))]
    [Migration("20211202160703_Update_DocumentTypeTable_DocumentSection")]
    partial class Update_DocumentTypeTable_DocumentSection
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.AppSetting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DataType")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("AppSettings");
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Colleague", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientId")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ErrorLog")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JsonData")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonNumber")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("StageId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("TrackingReference")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("StageId");

                    b.ToTable("Colleagues");
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.DocumentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DocumentSection")
                        .HasColumnType("int");

                    b.Property<string>("HCMDocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RTWDocumentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DocumentTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DocumentSection = 2,
                            HCMDocumentName = "Birth or Adoption Certificate",
                            RTWDocumentName = "Birth or Adoption Certificate"
                        },
                        new
                        {
                            Id = 2,
                            DocumentSection = 5,
                            HCMDocumentName = "Frontier Worker Permit",
                            RTWDocumentName = "Frontier Worker Permit"
                        },
                        new
                        {
                            Id = 3,
                            DocumentSection = 4,
                            HCMDocumentName = "Share Code Evidence",
                            RTWDocumentName = "Share Code Evidence"
                        },
                        new
                        {
                            Id = 4,
                            DocumentSection = 1,
                            HCMDocumentName = "Passport",
                            RTWDocumentName = "Passport"
                        },
                        new
                        {
                            Id = 5,
                            DocumentSection = 5,
                            HCMDocumentName = "National Identity card - back",
                            RTWDocumentName = "National Identity card - back"
                        },
                        new
                        {
                            Id = 6,
                            DocumentSection = 5,
                            HCMDocumentName = "National Identity card - front",
                            RTWDocumentName = "National Identity card - front"
                        },
                        new
                        {
                            Id = 7,
                            DocumentSection = 2,
                            HCMDocumentName = "National Insurance Document",
                            RTWDocumentName = "National Insurance Document"
                        });
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Error", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Logged")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StackTrace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Errors");
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("IncidentDetailId")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("ServiceNowDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IncidentDetailId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.IncidentDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("AssignmentGroup")
                        .HasColumnType("varchar(500)");

                    b.Property<string>("BusinessService")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConfigurationItem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorCode")
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Impact")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UndefinedCaller")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UndefinedLocation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IncidentDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "Azure EventHub Error",
                            ErrorCode = "AZRCCP_RTW_HR_APP_101001",
                            Impact = 3,
                            Summary = "Azure resource error. Please follow the PDG AZRCCP_RTW_HR_APP_101001.",
                            Type = 2,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        },
                        new
                        {
                            Id = 2,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "Azure Database Error",
                            ErrorCode = "AZRCCP_RTW_HR_APP_102001",
                            Impact = 3,
                            Summary = "Azure Database Error. Please follow the PDG AZRCCP_RTW_HR_APP_102001.",
                            Type = 2,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        },
                        new
                        {
                            Id = 3,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "Azure KeyVault Error",
                            ErrorCode = "AZRCCP_RTW_HR_APP_103001",
                            Impact = 3,
                            Summary = "Azure KeyVault Error. Please follow the PDG AZRCCP_RTW_HR_APP_103001.",
                            Type = 2,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        },
                        new
                        {
                            Id = 4,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "RTW Outbount API Error",
                            ErrorCode = "AZRCCP_RTW_HR_APP_104001",
                            Impact = 3,
                            Summary = "RTW New starter posting data API Error. Please follow the PDG AZRCCP_RTW_HR_APP_104001.",
                            Type = 1,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        },
                        new
                        {
                            Id = 5,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "RTW Inbount API Error",
                            ErrorCode = "AZRCCP_RTW_HR_APP_105001",
                            Impact = 3,
                            Summary = "RTW Read data API Error. Please follow the PDG AZRCCP_RTW_HR_APP_105001.",
                            Type = 1,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        },
                        new
                        {
                            Id = 6,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "RTW Data Error",
                            ErrorCode = "AZRCCP_HR_APP_106001",
                            Impact = 3,
                            Summary = "Update HCM Data Error. Please follow the PDG AZRCCP_HR_APP_106001.",
                            Type = 1,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        },
                        new
                        {
                            Id = 7,
                            Active = true,
                            AssignmentGroup = "HR Engineering",
                            BusinessService = "Application Platform",
                            ConfigurationItem = "Azure Cloud Platform",
                            Description = "Generic Exception",
                            ErrorCode = "AZRCCP_RTW_HR_APP_110001",
                            Impact = 3,
                            Summary = "Generic exception. Please follow the PDG AZRCCP_RTW_HR_APP_110001.",
                            Type = 2,
                            UndefinedCaller = "HR Engineering",
                            UndefinedLocation = "Application Platform"
                        });
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.LookupData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExternalValue")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("LookupType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("LookupData");
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Stage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Stages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StageName = "Initial Stage"
                        },
                        new
                        {
                            Id = 2,
                            StageName = "Check Requested"
                        },
                        new
                        {
                            Id = 3,
                            StageName = "Check Completed"
                        },
                        new
                        {
                            Id = 4,
                            StageName = "HCM Updated"
                        },
                        new
                        {
                            Id = 5,
                            StageName = "Check Request Failed"
                        },
                        new
                        {
                            Id = 6,
                            StageName = "Read Data Failed"
                        },
                        new
                        {
                            Id = 7,
                            StageName = "HCM Update Failed"
                        });
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExternalValue")
                        .IsRequired()
                        .HasColumnType("varchar(300)");

                    b.Property<string>("StatusName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ExternalValue = "Check Not Completed",
                            StatusName = "Check Not Completed"
                        },
                        new
                        {
                            Id = 2,
                            ExternalValue = "Successful Check",
                            StatusName = "Successful Check"
                        },
                        new
                        {
                            Id = 3,
                            ExternalValue = "Failed Check",
                            StatusName = "Failed Check"
                        },
                        new
                        {
                            Id = 4,
                            ExternalValue = "Partial Check",
                            StatusName = "Partial Check"
                        },
                        new
                        {
                            Id = 5,
                            ExternalValue = "User Removed Check",
                            StatusName = "User Removed Check"
                        });
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Colleague", b =>
                {
                    b.HasOne("ColleagueInt.RTW.Database.Entities.Stage", "IncidentDetails")
                        .WithMany()
                        .HasForeignKey("StageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IncidentDetails");
                });

            modelBuilder.Entity("ColleagueInt.RTW.Database.Entities.Incident", b =>
                {
                    b.HasOne("ColleagueInt.RTW.Database.Entities.IncidentDetail", "IncidentDetails")
                        .WithMany()
                        .HasForeignKey("IncidentDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IncidentDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
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
    [Migration("20211220154000_Update_Stages_Data_v1")]
    partial class Update_Stages_Data_v1
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
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Accession Work Card"
                        },
                        new
                        {
                            Id = 2,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Accession Work Card cover"
                        },
                        new
                        {
                            Id = 3,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Application Registration Card - back"
                        },
                        new
                        {
                            Id = 4,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Application Registration Card - front"
                        },
                        new
                        {
                            Id = 5,
                            DocumentSection = 5,
                            HCMDocumentName = "Biometric Residence Permit current (not Tier 4)",
                            RTWDocumentName = "Biometric Residence card - back"
                        },
                        new
                        {
                            Id = 6,
                            DocumentSection = 5,
                            HCMDocumentName = "Biometric Residence Permit current (not Tier 4)",
                            RTWDocumentName = "Biometric Residence card - front"
                        },
                        new
                        {
                            Id = 7,
                            DocumentSection = 5,
                            HCMDocumentName = "Biometric Residence Permit current (not Tier 4)",
                            RTWDocumentName = "Biometric Residence card - Indefinite Leave - front"
                        },
                        new
                        {
                            Id = 8,
                            DocumentSection = 5,
                            HCMDocumentName = "Biometric Residence Permit current (not Tier 4)",
                            RTWDocumentName = "Biometric Residence card - Permanent Residence (EEA) - front"
                        },
                        new
                        {
                            Id = 9,
                            DocumentSection = 5,
                            HCMDocumentName = "Biometric Residence Permit current (Tier 4) plus study & term time evidence",
                            RTWDocumentName = "Biometric Residence Card - Tier 4 (student) - front"
                        },
                        new
                        {
                            Id = 10,
                            DocumentSection = 2,
                            HCMDocumentName = "Birth certificate (British citizen)",
                            RTWDocumentName = "Birth or Adoption Certificate (British)"
                        },
                        new
                        {
                            Id = 11,
                            DocumentSection = 2,
                            HCMDocumentName = "Birth certificate",
                            RTWDocumentName = "Birth, Adoption or Naturalisation Certificate"
                        },
                        new
                        {
                            Id = 12,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Certificate of Application Family Member - back"
                        },
                        new
                        {
                            Id = 13,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Certificate of Application Family Member - front"
                        },
                        new
                        {
                            Id = 14,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "ECS Positive Verification Notice"
                        },
                        new
                        {
                            Id = 15,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "ECS Positive Verification Notice - Asylum"
                        },
                        new
                        {
                            Id = 16,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "ECS Positive Verification Notice - Family"
                        },
                        new
                        {
                            Id = 17,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "EEA & Swiss Residents Permit or Registration Certificate"
                        },
                        new
                        {
                            Id = 18,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "EEA & Swiss Residents Permit or Registration Certificate cover"
                        },
                        new
                        {
                            Id = 19,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Frontier Worker Permit"
                        },
                        new
                        {
                            Id = 20,
                            DocumentSection = 5,
                            HCMDocumentName = "Immigration Status Document plus NI No",
                            RTWDocumentName = "Immigration Status Document - Indefinite Leave to Enter or Remain"
                        },
                        new
                        {
                            Id = 21,
                            DocumentSection = 5,
                            HCMDocumentName = "Immigration Status Document plus NI No",
                            RTWDocumentName = "Immigration Status Document - Leave to Enter or Remain"
                        },
                        new
                        {
                            Id = 22,
                            DocumentSection = 5,
                            HCMDocumentName = "Immigration Status Document plus NI No",
                            RTWDocumentName = "Immigration Status Document - Permanent Residence Card EEA Family Member"
                        },
                        new
                        {
                            Id = 23,
                            DocumentSection = 5,
                            HCMDocumentName = "Immigration Status Document plus NI No",
                            RTWDocumentName = "Immigration Status Document - Residence Card EEA Family Member"
                        },
                        new
                        {
                            Id = 24,
                            DocumentSection = 5,
                            HCMDocumentName = "Immigration Status Document plus NI No",
                            RTWDocumentName = "Immigration Status Document - Work Permit"
                        },
                        new
                        {
                            Id = 25,
                            DocumentSection = 2,
                            HCMDocumentName = "Marriage Certificate",
                            RTWDocumentName = "Marriage certificate, divorce decree absolute, a deed poll or statutory declaration"
                        },
                        new
                        {
                            Id = 26,
                            DocumentSection = 5,
                            HCMDocumentName = "ID Card",
                            RTWDocumentName = "National Identity card - back"
                        },
                        new
                        {
                            Id = 27,
                            DocumentSection = 5,
                            HCMDocumentName = "ID Card",
                            RTWDocumentName = "National Identity card - front"
                        },
                        new
                        {
                            Id = 28,
                            DocumentSection = 5,
                            HCMDocumentName = "ID Card",
                            RTWDocumentName = "National Insurance Document"
                        },
                        new
                        {
                            Id = 29,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Naturalisation Certificate"
                        },
                        new
                        {
                            Id = 30,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Other"
                        },
                        new
                        {
                            Id = 31,
                            DocumentSection = 1,
                            HCMDocumentName = "Passport of British or EEA citizen",
                            RTWDocumentName = "Passport"
                        },
                        new
                        {
                            Id = 32,
                            DocumentSection = 1,
                            HCMDocumentName = "Passport of British or EEA citizen",
                            RTWDocumentName = "Passport Additional Page"
                        },
                        new
                        {
                            Id = 33,
                            DocumentSection = 1,
                            HCMDocumentName = "Passport of British or EEA citizen",
                            RTWDocumentName = "Passport cover"
                        },
                        new
                        {
                            Id = 34,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Indefinite Leave to Enter or Remain"
                        },
                        new
                        {
                            Id = 35,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Leave to Enter or Remain"
                        },
                        new
                        {
                            Id = 36,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Permanent Residence Card EEA Family Member"
                        },
                        new
                        {
                            Id = 37,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Residence Card EEA Family Member"
                        },
                        new
                        {
                            Id = 38,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Right of Abode"
                        },
                        new
                        {
                            Id = 39,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Tier 4 (student)"
                        },
                        new
                        {
                            Id = 40,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Tier 4 (student)"
                        },
                        new
                        {
                            Id = 41,
                            DocumentSection = 5,
                            HCMDocumentName = "Passport Document",
                            RTWDocumentName = "Passport Stamp - Work Permit"
                        },
                        new
                        {
                            Id = 42,
                            DocumentSection = 5,
                            HCMDocumentName = "EU Pre-Settled Status",
                            RTWDocumentName = "Pre-settled Status Document"
                        },
                        new
                        {
                            Id = 43,
                            DocumentSection = 5,
                            HCMDocumentName = "EU Settled Status",
                            RTWDocumentName = "Settled Status Document"
                        },
                        new
                        {
                            Id = 44,
                            DocumentSection = 4,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Share Code Evidence"
                        },
                        new
                        {
                            Id = 45,
                            DocumentSection = 4,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Share Code Evidence"
                        },
                        new
                        {
                            Id = 46,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Student Term Time Evidence"
                        },
                        new
                        {
                            Id = 47,
                            DocumentSection = 5,
                            HCMDocumentName = "Work",
                            RTWDocumentName = "Work Permit letter - back"
                        },
                        new
                        {
                            Id = 48,
                            DocumentSection = 5,
                            HCMDocumentName = "Work",
                            RTWDocumentName = "Work Permit letter - front"
                        },
                        new
                        {
                            Id = 49,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Yellow Registration Certificate"
                        },
                        new
                        {
                            Id = 50,
                            DocumentSection = 5,
                            HCMDocumentName = "Other Legal Document",
                            RTWDocumentName = "Yellow Registration Certificate cover"
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

                    b.Property<bool>("IsValidCheckExist")
                        .HasColumnType("bit");

                    b.Property<string>("StageName")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Stages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsValidCheckExist = true,
                            StageName = "Initial Stage"
                        },
                        new
                        {
                            Id = 2,
                            IsValidCheckExist = true,
                            StageName = "Check Requested"
                        },
                        new
                        {
                            Id = 3,
                            IsValidCheckExist = true,
                            StageName = "Check Completed"
                        },
                        new
                        {
                            Id = 4,
                            IsValidCheckExist = false,
                            StageName = "HCM Updated"
                        },
                        new
                        {
                            Id = 5,
                            IsValidCheckExist = false,
                            StageName = "Check Request Failed"
                        },
                        new
                        {
                            Id = 6,
                            IsValidCheckExist = true,
                            StageName = "Read Data Failed"
                        },
                        new
                        {
                            Id = 7,
                            IsValidCheckExist = true,
                            StageName = "HCM Update Failed"
                        },
                        new
                        {
                            Id = 8,
                            IsValidCheckExist = true,
                            StageName = "HCM Documents Uploaded"
                        },
                        new
                        {
                            Id = 9,
                            IsValidCheckExist = true,
                            StageName = "HCM Documents Upload Failed"
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

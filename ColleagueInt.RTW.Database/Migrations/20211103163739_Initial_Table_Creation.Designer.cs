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
    [Migration("20211103163739_Initial_Table_Creation")]
    partial class Initial_Table_Creation
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
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("LastUpdateOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("PersonNumber")
                        .IsRequired()
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

                    b.ToTable("Colleagues");
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
                            StageName = "Inital Stage"
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
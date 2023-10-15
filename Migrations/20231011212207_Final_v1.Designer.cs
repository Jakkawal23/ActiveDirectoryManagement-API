﻿// <auto-generated />
using System;
using ActiveDirectoryManagement_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ActiveDirectoryManagement_API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231011212207_Final_v1")]
    partial class Final_v1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.DB.DbDepartment", b =>
                {
                    b.Property<string>("DepartmentCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentId"));

                    b.Property<string>("DepartmentNameEN")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DepartmentNameTH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("DepartmentCode");

                    b.ToTable("DbDepartments");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", b =>
                {
                    b.Property<string>("EmployeeCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("DepartmentCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PositionCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("TeamCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("EmployeeCode");

                    b.HasIndex("DepartmentCode");

                    b.HasIndex("PositionCode");

                    b.HasIndex("TeamCode");

                    b.ToTable("DbEmployees");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.DB.DbPosition", b =>
                {
                    b.Property<string>("PositionCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PositionId"));

                    b.Property<string>("PositionNameEN")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("PositionNameTH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("PositionCode");

                    b.ToTable("DbPositions");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.DB.DbStatus", b =>
                {
                    b.Property<string>("StatusCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StatusId"));

                    b.Property<string>("StatusNameEN")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StatusNameTH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("StatusCode");

                    b.ToTable("DbStatuses");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.DB.DbTeam", b =>
                {
                    b.Property<string>("TeamCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("TeamId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamId"));

                    b.Property<string>("TeamNameEN")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("TeamNameTH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("TeamCode");

                    b.ToTable("DbTeams");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentEmail", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"));

                    b.Property<DateTime?>("ApproveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ApproveEmpCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DocumentId");

                    b.HasIndex("EmpCode");

                    b.HasIndex("StatusCode");

                    b.ToTable("DocumentEmails");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentManagement", b =>
                {
                    b.Property<int>("ManagementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagementId"));

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Password")
                        .HasColumnType("bit");

                    b.HasKey("ManagementId");

                    b.HasIndex("EmpCode");

                    b.ToTable("DocumentManagements");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentPassword", b =>
                {
                    b.Property<int>("PasswordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PasswordId"));

                    b.Property<DateTime?>("ApproveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ApproveEmpCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("PasswordId");

                    b.HasIndex("EmpCode");

                    b.HasIndex("StatusCode");

                    b.ToTable("DocumentPasswords");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentPhone", b =>
                {
                    b.Property<int>("DocumentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentId"));

                    b.Property<DateTime?>("ApproveDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ApproveEmpCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("StatusCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("DocumentId");

                    b.HasIndex("EmpCode");

                    b.HasIndex("StatusCode");

                    b.ToTable("DocumentPhones");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.SU.SuProfile", b =>
                {
                    b.Property<string>("ProfileCode")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfileId"));

                    b.Property<string>("ProfileNameEN")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProfileNameTH")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("ProfileCode");

                    b.ToTable("SuProfiles");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.SU.SuUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("EmpCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("MobilePhoneNo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ProfileCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("UserId");

                    b.HasIndex("EmpCode");

                    b.HasIndex("ProfileCode");

                    b.ToTable("SuUsers");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", b =>
                {
                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbDepartment", "DbDepartment")
                        .WithMany()
                        .HasForeignKey("DepartmentCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbPosition", "DbPosition")
                        .WithMany()
                        .HasForeignKey("PositionCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbTeam", "DbTeam")
                        .WithMany()
                        .HasForeignKey("TeamCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbDepartment");

                    b.Navigation("DbPosition");

                    b.Navigation("DbTeam");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentEmail", b =>
                {
                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", "DbEmployee")
                        .WithMany()
                        .HasForeignKey("EmpCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbStatus", "DbStatus")
                        .WithMany()
                        .HasForeignKey("StatusCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbEmployee");

                    b.Navigation("DbStatus");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentManagement", b =>
                {
                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", "DbEmployee")
                        .WithMany()
                        .HasForeignKey("EmpCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbEmployee");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentPassword", b =>
                {
                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", "DbEmployee")
                        .WithMany()
                        .HasForeignKey("EmpCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbStatus", "DbStatus")
                        .WithMany()
                        .HasForeignKey("StatusCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbEmployee");

                    b.Navigation("DbStatus");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.Document.DocumentPhone", b =>
                {
                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", "DbEmployee")
                        .WithMany()
                        .HasForeignKey("EmpCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbStatus", "DbStatus")
                        .WithMany()
                        .HasForeignKey("StatusCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbEmployee");

                    b.Navigation("DbStatus");
                });

            modelBuilder.Entity("ActiveDirectoryManagement_API.Models.Domain.SU.SuUser", b =>
                {
                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.DB.DbEmployee", "DbEmployee")
                        .WithMany()
                        .HasForeignKey("EmpCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ActiveDirectoryManagement_API.Models.Domain.SU.SuProfile", "SuProfile")
                        .WithMany()
                        .HasForeignKey("ProfileCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DbEmployee");

                    b.Navigation("SuProfile");
                });
#pragma warning restore 612, 618
        }
    }
}
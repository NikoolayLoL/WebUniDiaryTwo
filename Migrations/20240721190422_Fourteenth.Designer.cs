﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebUniDiaryTwo.Services;

#nullable disable

namespace WebUniDiaryTwo.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20240721190422_Fourteenth")]
    partial class Fourteenth
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebUniDiaryTwo.Services.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsRetired")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Specialties");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Formula", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("MultiplierAttention")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierExam")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierExercises")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierExtra")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierTask")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierWork")
                        .HasColumnType("float");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Formulas");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Grade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("GradeValue")
                        .HasPrecision(2)
                        .HasColumnType("decimal(2,2)");

                    b.Property<int>("SemesterUserId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SemesterUserId");

                    b.HasIndex("SubjectId");

                    b.HasIndex("UserId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Semester", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SemesterLenght")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Semesters");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.SemesterSubject", b =>
                {
                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<int>("SemesterLenghtId")
                        .HasColumnType("int");

                    b.HasKey("SemesterId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SemesterSubjects");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.SemesterUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SemesterId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SemesterId");

                    b.HasIndex("UserId");

                    b.ToTable("SemesterUsers");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrimaryTeacherId")
                        .HasColumnType("int");

                    b.Property<int>("SubstituteTeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryTeacherId");

                    b.HasIndex("SubstituteTeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("AddedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EGN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Properties")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Formula", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.Subject", "Subject")
                        .WithMany("Formulas")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Grade", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.SemesterUser", "SemesterUser")
                        .WithMany("Grades")
                        .HasForeignKey("SemesterUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.Subject", "Subject")
                        .WithMany("Grades")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.User", null)
                        .WithMany("Grades")
                        .HasForeignKey("UserId");

                    b.Navigation("SemesterUser");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.SemesterSubject", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.Semester", "Semester")
                        .WithMany("SemesterSubjects")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.Subject", "Subject")
                        .WithMany("SemesterSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.SemesterUser", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.Semester", "Semester")
                        .WithMany("SemesterUsers")
                        .HasForeignKey("SemesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Semester");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Subject", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.User", "PrimaryTeacher")
                        .WithMany("PrimarySubjects")
                        .HasForeignKey("PrimaryTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.User", "SubstituteTeacher")
                        .WithMany("SubstituteSubjects")
                        .HasForeignKey("SubstituteTeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PrimaryTeacher");

                    b.Navigation("SubstituteTeacher");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.UserRole", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Semester", b =>
                {
                    b.Navigation("SemesterSubjects");

                    b.Navigation("SemesterUsers");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.SemesterUser", b =>
                {
                    b.Navigation("Grades");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Subject", b =>
                {
                    b.Navigation("Formulas");

                    b.Navigation("Grades");

                    b.Navigation("SemesterSubjects");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.User", b =>
                {
                    b.Navigation("Grades");

                    b.Navigation("PrimarySubjects");

                    b.Navigation("SubstituteSubjects");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}

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
    [Migration("20240715195217_four")]
    partial class four
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
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("SubjectId");

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

                    b.HasKey("SemesterId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("SemesterSubjects");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PrimaryTeacherId")
                        .HasColumnType("int");

                    b.Property<int>("SpecialtyId")
                        .HasColumnType("int");

                    b.Property<int>("SubstituteTeacherId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrimaryTeacherId");

                    b.HasIndex("SpecialtyId");

                    b.HasIndex("SubstituteTeacherId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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
                        .HasColumnType("nvarchar(max)");

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
                    b.HasOne("WebUniDiaryTwo.Services.User", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.Subject", "Subject")
                        .WithMany("Grades")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

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

            modelBuilder.Entity("WebUniDiaryTwo.Services.Subject", b =>
                {
                    b.HasOne("WebUniDiaryTwo.Services.User", "PrimaryTeacher")
                        .WithMany("PrimarySubjects")
                        .HasForeignKey("PrimaryTeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.Discipline", "Specialty")
                        .WithMany("Subjects")
                        .HasForeignKey("SpecialtyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebUniDiaryTwo.Services.User", "SubstituteTeacher")
                        .WithMany("SubstituteSubjects")
                        .HasForeignKey("SubstituteTeacherId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("PrimaryTeacher");

                    b.Navigation("Specialty");

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

            modelBuilder.Entity("WebUniDiaryTwo.Services.Discipline", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("WebUniDiaryTwo.Services.Semester", b =>
                {
                    b.Navigation("SemesterSubjects");
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

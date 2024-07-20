using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace WebUniDiaryTwo.Services
{
    public class UniversityContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Discipline> Specialties { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<SemesterSubject> SemesterSubjects { get; set; }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SemesterSubject>()
                .HasKey(ss => new { ss.SemesterId, ss.SubjectId });

            modelBuilder.Entity<SemesterSubject>()
                .HasOne(ss => ss.Semester)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SemesterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SemesterSubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.SemesterSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.PrimaryTeacher)
                .WithMany(u => u.PrimarySubjects)
                .HasForeignKey(s => s.PrimaryTeacherId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasOne(s => s.SubstituteTeacher)
                .WithMany(u => u.SubstituteSubjects)
                .HasForeignKey(s => s.SubstituteTeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(u => u.Grades)
                .HasForeignKey(g => g.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Formula>()
                .HasOne(f => f.Subject)
                .WithMany(s => s.Formulas)
                .HasForeignKey(f => f.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();
        }
    }

    // User Model
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Properties { get; set; } = string.Empty;
        public string EGN { get; set; } = string.Empty;
        public bool Active { get; set; } = true;
        public DateTime AddedOn { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
        public ICollection<Grade> Grades { get; set; } = new HashSet<Grade>();
        public ICollection<Subject> PrimarySubjects { get; set; } = new HashSet<Subject>();
        public ICollection<Subject> SubstituteSubjects { get; set; } = new HashSet<Subject>();
    }


    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }

    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

    public class Discipline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsRetired { get; set; }
    }

    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // 8 to 10 most of the time
        public int SemesterLenght { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<SemesterSubject> SemesterSubjects { get; set; }
    }

    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [AllowNull]
        public string Description { get; set; } = string.Empty;
        [AllowNull]
        public int PrimaryTeacherId { get; set; }
        public User PrimaryTeacher { get; set; }
        [AllowNull]
        public int SubstituteTeacherId { get; set; }
        public User SubstituteTeacher { get; set; }
        public bool Active { get; set; } = true;

        public ICollection<Formula> Formulas { get; set; }
        public ICollection<Grade> Grades { get; set; }
        public ICollection<SemesterSubject> SemesterSubjects { get; set; }
    }

    public class SemesterSubject
    {
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
        // Semester IITB -> 2 (and IITB has a max of 8 for example)
        public int SemesterLenghtId { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }

    public class Grade
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public User Student { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public decimal GradeValue { get; set; }
        public DateTime DateRecorded { get; set; }
    }

    public class Formula
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public double MultiplierExam { get; set; } = 0;
        public double MultiplierWork { get; set; } = 0;
        public double MultiplierTask { get; set; } = 0;
        public double MultiplierAttention { get; set; } = 0;
        public double MultiplierExercises { get; set; } = 0;
        public double MultiplierExtra { get; set; } = 0;
    }
}

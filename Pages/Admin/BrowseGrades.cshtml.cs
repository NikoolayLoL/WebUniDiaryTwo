using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Admin
{
    public class BrowseGradesModel : PageModel
    {

        public List<Grade> Grades { get; set; } = new();
        public SemesterSubject SSubject { get; set; } = new SemesterSubject();

        private readonly UniversityContext context;

        public BrowseGradesModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet(int semesterId, int subjectId)
        {
            try
            {
                SSubject = context.SemesterSubjects
                        .Where(ss => ss.SemesterId == semesterId)
                        .Where(ss => ss.SubjectId == subjectId)
                        .Include(ss => ss.Semester)
                        .ThenInclude (ss => ss.SemesterUsers)
                        .ThenInclude (x => x.User)
                        .Include(s => s.Subject)
                        .First();

                Grades = context.Grades
                    .Where(gr => gr.SubjectId == subjectId)
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO make an error display handler class
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "/BrowseSemesters?Failure=issueWithSubject");
                return;
            }
        }

        public IActionResult OnGetAddGrades(int semesterUserId, int subjectId, int gradeValue, string gradeType)
        {
            try
            {
                var grade = new Grade
                {
                    SemesterUserId = semesterUserId,
                    SubjectId = subjectId,
                    GradeValue = gradeValue,
                    Type = gradeType,
                    DateRecorded = DateTime.Now
                };

                context.Grades.Add(grade);
                

                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.InnerException });
            }
        }

        public IActionResult OnGetAddStudent(int studentId, int semesterId)
        {
            try
            {
                var studentRoleId = context.Roles.First(x => x.Name == "Student").Id;
                var user = context.Users
                    .Include(u => u.UserRoles)
                    .FirstOrDefault(u => u.Id == studentId && u.UserRoles.Any(ur => ur.RoleId == studentRoleId));

                if (user == null)
                {
                    return new JsonResult(new { success = false, message = "User is not a student" });
                }

                var semesterUser = new SemesterUser
                {
                    UserId = studentId,
                    SemesterId = semesterId
                };

                context.SemesterUsers.Add(semesterUser);
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public JsonResult OnGetGetStudents(string term)
        {
            var studentRoleId = context.Roles.First(x => x.Name == "Student").Id;
            var userIds = context.UserRoles
                .Where(x => x.RoleId == studentRoleId)
                .Select(x => x.UserId)
                .Distinct()
                .ToList();

            var suggestions = context.Users
                .Where(u => u.Email.Contains(term))
                .Where(x => userIds.Contains(x.Id))
                .Select(u => new { u.Email, u.Id })
                .ToList();

            return new JsonResult(suggestions);
        }
    }
}

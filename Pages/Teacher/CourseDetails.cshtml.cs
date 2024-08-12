using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Logic;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Teacher
{
    public class CourseDetailsModel : PageModel
    {
        public SemesterSubject SSubject { get; set; } = new();
        public List<Grade> Grades { get; set; } = new();

        private readonly UniversityContext context;

        public CourseDetailsModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet(int semesterId, int subjectId)
        {
            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            var cookie = new CookieRepository(cookieValue);
            int userId = int.Parse(cookie.GetCookieId().Split('/').Skip(1).FirstOrDefault() ?? "0");

            try
            {
                SSubject = context.SemesterSubjects
                    .Where(x => x.SemesterId == semesterId && x.SubjectId == subjectId)
                    .Include(x => x.Subject)
                    .Include(x => x.Semester)
                    .ThenInclude(x => x.SemesterUsers)
                    .ThenInclude(x => x.User)
                    .First();

                Grades = context.Grades
                    .Where(gr => gr.SubjectId == subjectId)
                    .ToList();

                //Grades = context.Grades.

                if (SSubject == null)
                {
                    Response.Redirect(CustomRedirect.RoleRedirect("teacher") + "BrowseSubjects/?Failure=SubjectNotFound");
                }
            }
            catch (Exception ex)
            {
                
            }
        }

        public IActionResult OnGetSubmitGrade(int semesterUserId, int subjectId, decimal gradeValue, string gradeType)
        {
            if (gradeValue < (decimal) 2 || gradeValue > (decimal) 6)
            {
                return new JsonResult(new { success = false, message = "Invalid Grade!" });
            }

            switch (gradeType)
            {
                case "Exam":
                case "Work":
                case "Task":
                case "Attention":
                case "Exercises":
                case "Extra":
                    break;
                default:
                    return new JsonResult(new { success = false, message = "Invalid Type!" });
            }

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
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }
    }
}

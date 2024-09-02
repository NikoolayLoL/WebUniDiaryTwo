using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebUniDiary.Logic;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Student
{
    public class SemesterOverviewModel : PageModel
    {
        public SemesterUser SUser { get; set; } = new();
        public List<SemesterSubject> SSubject { get; set; } = new();
        public List<Formula> Formulas { get; set; } = new();

        private readonly UniversityContext _context;

        public SemesterOverviewModel(UniversityContext context)
        {
            _context = context;
        }

        public void OnGet(int semesterId = 0)
        {
            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            var cookie = new CookieRepository(cookieValue);

            int userId = int.Parse(cookie.GetCookieId().Split('/').Skip(1).FirstOrDefault() ?? "0");

            try
            {
                SUser = _context.SemesterUsers
                    .Where(su => su.SemesterId == semesterId)
                    .Where(su => su.UserId == userId)
                    .Include(ss => ss.Semester)
                    .ThenInclude(ss => ss.SemesterUsers)
                    .ThenInclude(x => x.User)
                    .First();
                SSubject = _context.SemesterSubjects
                    .Where(ss => ss.SemesterId == semesterId)
                    .Include(ss => ss.Subject)
                    .ThenInclude (ss => ss.Grades)
                    .ToList();

                var subjectIds = SSubject.Select(subject => subject.Subject.Id).ToList();
                Formulas = _context.Formulas
                    .Where(f => subjectIds.Contains(f.SubjectId))
                    .ToList();
            }
            catch (Exception ex)
            {
                // TODO make an error display handler class
                Response.Redirect(CustomRedirect.RoleRedirect("student") + "/?Failure=issueWithSemester");
                return;
            }
        }
    }
}

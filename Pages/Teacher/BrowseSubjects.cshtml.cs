using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiary.Logic;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Teacher
{
    public class BrowseSubjectsModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<SemesterSubject> SSubjects { get; set; }
        public User Teacher { get; set; } = new();

        private readonly UniversityContext context;

        public BrowseSubjectsModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet(int pageSize = 10, int currentPage = 1)
        {
            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            var cookie = new CookieRepository(cookieValue);
            int userId = int.Parse(cookie.GetCookieId().Split('/').Skip(1).FirstOrDefault() ?? "0");

            PageSize = pageSize;
            CurrentPage = currentPage;

            int skip = (currentPage - 1) * pageSize;

            try
            {
                SSubjects = context.SemesterSubjects
                    .Include(x => x.Subject)
                    .Where(x => x.Subject.PrimaryTeacherId == userId || x.Subject.SubstituteTeacherId == userId)
                    .Include(x => x.Semester)
                    .ToList();

                TotalRecords = SSubjects.Count;
                TotalPages = (int) Math.Ceiling(TotalRecords / (double) pageSize);

                SSubjects = SSubjects.Skip(skip).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO make an error display handler class
                Response.Redirect(CustomRedirect.RoleRedirect("teacher") + "?Failure=issueWithSubjects");
                return;
            }

            Teacher = context.Users
                .Find(userId)!;
        }

        public IActionResult OnGetGetFormula(int subjectId)
        {
            if (subjectId == 0)
            {
                return new JsonResult(new { success = false, message = "Invalid Subject!" });
            }

            try
            {
                Formula formula = context.Formulas.FirstOrDefault(f => f.SubjectId == subjectId);

                if (formula != null)
                {
                    return new JsonResult(new
                    {
                        success = true,
                        data = new
                        {
                            exam = formula.MultiplierExam,
                            work = formula.MultiplierWork,
                            task = formula.MultiplierTask,
                            attention = formula.MultiplierAttention,
                            exercises = formula.MultiplierExercises,
                            extra = formula.MultiplierExtra
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false });
            }

            return new JsonResult(new { success = false, message = "No formula found for this subject!" });
        }

        public IActionResult OnGetSaveFormula(int subjectId, int exam, int work, int task, int attention, int exercises, int extra)
        {
            if (subjectId == 0)
            {
                return new JsonResult(new { success = false, message = "Invalid Subject!" });
            }

            int total = exam + work + task + attention + exercises + extra;

            if (total != 100)
            {
                return new JsonResult(new { success = false, message = "Formula Value must be 100%" });
            }

            try
            {
                var formula = context.Formulas.FirstOrDefault(f => f.SubjectId == subjectId);
                if (formula == null)
                {
                    formula = new Formula
                    {
                        SubjectId = subjectId,
                        MultiplierExam = exam,
                        MultiplierWork = work,
                        MultiplierTask = task,
                        MultiplierAttention = attention,
                        MultiplierExercises = exercises,
                        MultiplierExtra = extra
                    };
                    context.Formulas.Add(formula);
                }
                else
                {
                    formula.SubjectId = subjectId;
                    formula.MultiplierExam = exam;
                    formula.MultiplierWork = work;
                    formula.MultiplierTask = task;
                    formula.MultiplierAttention = attention;
                    formula.MultiplierExercises = exercises;
                    formula.MultiplierExtra = extra;
                }


                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.InnerException});
            }
        }
    }
}

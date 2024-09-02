using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Migrations;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Admin
{
    public class BrowseSemestersModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<Semester> Semesters { get; set; } = new();

        private readonly UniversityContext context;

        public BrowseSemestersModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet(int pageSize = 10, int currentPage = 1)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;

            int skip = (currentPage - 1) * pageSize;

            try
            {
                Semesters = context.Semesters
                    .Include(x => x.SemesterSubjects)
                    .ThenInclude(x => x.Subject)
                    .ToList();

                TotalRecords = Semesters.Count;
                TotalPages = (int) Math.Ceiling(TotalRecords / (double) pageSize);

                Semesters = Semesters.Skip(skip).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO make an error display handler class
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?Failure=issueWithSemesters");
                return;
            }
        }

        public IActionResult OnGetAddSemester(string name, int lenght = 8)
        {
            try
            {
                if (lenght % 2 == 1)
                {
                    return BadRequest("Semester Lenght must be an odd number!");
                }

                var time = DateTime.Now.AddYears(lenght/2);
                var semester = new Semester
                {
                    Name = name,
                    SemesterLenght = lenght,
                    StartDate = DateTime.Now,
                    EndDate = time,
                };
                context.Semesters.Add(semester);
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        public IActionResult OnGetGetSemester(int id)
        {
            try
            {
                var semester = context.Semesters.FirstOrDefault(x => x.Id == id);
                if (semester == null)
                {
                    return new JsonResult(new { success = false, message = "Semester not found" });
                }

                return new JsonResult(new { success = true, id = semester.Id, name = semester.Name, lenght = semester.SemesterLenght });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetEditSemester(int id, string name, int lenght)
        {
            try
            {
                if (lenght % 2 == 1)
                {
                    return new BadRequestResult();
                }

                var semester = context.Semesters.FirstOrDefault(x => x.Id == id);
                if (semester == null)
                {
                    return new BadRequestResult();
                }

                semester.Name = name;

                if (semester.SemesterLenght != lenght)
                {

                    var time = semester.StartDate.AddYears(lenght / 2);
                    semester.SemesterLenght = lenght;
                    semester.EndDate = time;
                }

                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        public IActionResult OnGetAddSubject(int semesterId, int subjectId, int semesterLenghtId)
        {
            // TODO - add active flag and if the user has deactivated it, and then wants to re-add it just activate it.
            bool exists = context.SemesterSubjects
                .Any(ss => ss.SemesterId == semesterId
                        && ss.SubjectId == subjectId);

            if (!exists)
            {
                SemesterSubject semesterSubject = new()
                {
                    SemesterId = semesterId,
                    SubjectId = subjectId,
                    SemesterLenghtId = semesterLenghtId
                };

                context.SemesterSubjects.Add(semesterSubject);
                context.SaveChanges();
            }
            else
            {
                return new BadRequestResult();
            }

            return new JsonResult(new { success = true });
        }

        public JsonResult OnGetGetSubject(string term)
        {
            var suggestions = context.Subjects
                .Where(u => u.Name.Contains(term))
                .Select(u => new { u.Name, u.Id })
                .ToList();

            return new JsonResult(suggestions);
        }
    }
}

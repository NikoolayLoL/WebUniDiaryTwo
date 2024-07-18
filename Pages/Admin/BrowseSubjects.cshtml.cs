using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Admin
{
    public class BrowseSubjectsModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<Subject> Subjects { get; set; } = new ();
        public List<User> Teachers { get; set; } = new();

        private readonly UniversityContext context;

        public BrowseSubjectsModel(UniversityContext context)
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
                Subjects = context.Subjects.ToList();

                TotalRecords = Subjects.Count;
                TotalPages = (int) Math.Ceiling(TotalRecords / (double) pageSize);

                Subjects = Subjects.Skip(skip).Take(pageSize).ToList();
            }
            catch (Exception ex)
            {
                // TODO make an error display handler class
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?Failure=issueWithSubjects");
                return;
            }

            int teacherRoleId = context.Roles.First(x => x.Name == "Teacher").Id;

            var userIds = context.UserRoles
                .Where(x => x.RoleId == teacherRoleId)
                .Select(x => x.UserId)
                .Distinct()
                .ToList();

            Teachers = context.Users
                .Where(x => userIds.Contains(x.Id))
                .ToList();
        }

        public JsonResult OnGetAddSubject(string name, string description, int teacherId, int substituteTeacherId)
        {
            try
            {
                var subject = new Subject
                {
                    Name = name,
                    Description = description,
                    PrimaryTeacherId = teacherId,
                    SubstituteTeacherId = substituteTeacherId
                };
                context.Subjects.Add(subject);
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetEditSubject(int id, string name, string description)
        {
            try
            {
                var subject = context.Subjects.FirstOrDefault(x => x.Id == id);
                if (subject == null)
                {
                    return new JsonResult(new { success = false, message = "Subject not found" });
                }

                subject.Name = name;
                subject.Description = description;
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetGetSubject(int id)
        {
            try
            {
                var subject = context.Subjects.FirstOrDefault(x => x.Id == id);
                if (subject == null)
                {
                    return new JsonResult(new { success = false, message = "Subject not found" });
                }

                return new JsonResult(new { success = true, id = subject.Id, name = subject.Name, description = subject.Description });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetToggleStatus(int subjectId, bool isActive)
        {
            try
            {
                var subject = context.Subjects.FirstOrDefault(x => x.Id == subjectId);
                if (subject == null)
                {
                    return new JsonResult(new { success = false, message = "Subject not found" });
                }

                subject.Active = isActive;
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public JsonResult OnGetGetEmail(string term)
        {
            int teacherRoleId = context.Roles.First(x => x.Name == "Teacher").Id;

            var userIds = context.UserRoles
                .Where(x => x.RoleId == teacherRoleId)
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

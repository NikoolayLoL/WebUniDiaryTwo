using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Admin
{
    public class BrowseStudentsModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<User> Students { get; set; } = new List<User>();

        private readonly UniversityContext context;

        public BrowseStudentsModel(UniversityContext context)
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
                int studentRoleId = context.Roles.First(x => x.Name == "Student").Id;

                var userIds = context.UserRoles
                                     .Where(x => x.RoleId == studentRoleId)
                                     .Select(x => x.UserId)
                                     .Distinct()
                                     .ToList();

                TotalRecords = userIds.Count;
                TotalPages = (int) Math.Ceiling(TotalRecords / (double) pageSize);

                Students = context.Users
                                  .Where(x => userIds.Contains(x.Id))
                                  .Skip(skip)
                                  .Take(pageSize)
                                  .ToList();
            }
            catch (Exception ex)
            {
                Response.Redirect(CustomRedirect.RoleRedirect("admin") + "?Failure=RolesNotSetUp");
                return;
            }
        }

        public IActionResult OnGetAddStudent(string email, string password, string firstName, string lastName)
        {
            try
            {
                if (context.Users.Any(u => u.Email == email))
                {
                    return new JsonResult(new { StatusCode = StatusCodes.Status409Conflict, success = false, message = "Email already exists" });
                }
                
                var user = new User
                {
                    Email = email,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    AddedOn = DateTime.UtcNow
                };
                context.Users.Add(user);
                context.SaveChanges();

                var studentRoleId = context.Roles.First(x => x.Name == "Student").Id;
                context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = studentRoleId });
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetEditStudent(int id, string email, string firstName, string lastName, string egn)
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return new JsonResult(new { success = false, message = "User not found" });
                }

                user.Email = email;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.EGN = egn;
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetGetStudent(int id)
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    return new JsonResult(new { success = false, message = "User not found" });
                }

                return new JsonResult(new { success = true, id = user.Id, email = user.Email, firstName = user.FirstName, lastName = user.LastName, egn = user.EGN });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetToggleStatus(int userId, bool isActive)
        {
            try
            {
                var user = context.Users.FirstOrDefault(x => x.Id == userId);
                if (user == null)
                {
                    return new JsonResult(new { success = false, message = "User not found" });
                }

                user.Active = isActive;
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

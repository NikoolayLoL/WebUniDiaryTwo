using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Admin
{
    public class BrowseTeachersModel : PageModel
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public List<User> Teachers { get; set; } = new List<User>();

        private readonly UniversityContext context;

        public BrowseTeachersModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet(int pageSize = 10, int currentPage = 1)
        {
            PageSize = pageSize;
            CurrentPage = currentPage;

            int skip = (currentPage - 1) * pageSize;


                int teacherRoleId = context.Roles.First(x => x.Name == "Teacher").Id;

                var userIds = context.UserRoles
                                     .Where(x => x.RoleId == teacherRoleId)
                                     .Select(x => x.UserId)
                                     .Distinct()
                                     .ToList();

                TotalRecords = userIds.Count;
                TotalPages = (int) Math.Ceiling(TotalRecords / (double) pageSize);

                Teachers = context.Users
                                  .Where(x => userIds.Contains(x.Id))
                                  .Skip(skip)
                                  .Take(pageSize)
                                  .ToList();

        }

        public IActionResult OnGetAddTeacher(string email, string password, string firstName, string lastName)
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

                var teacherRoleId = context.Roles.First(x => x.Name == "Teacher").Id;
                context.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = teacherRoleId });
                context.SaveChanges();

                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message });
            }
        }

        public IActionResult OnGetEditTeacher(int id, string email, string firstName, string lastName, string egn)
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

        public IActionResult OnGetGetTeacher(int id)
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

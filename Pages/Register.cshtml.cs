using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiary.Logic;
using WebUniDiary.Models;
using WebUniDiary.Models.DTOs;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiary.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();
        private readonly UniversityContext context;
        public string failureMessage = string.Empty;

        public RegisterModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            if (this.UserDto.Password == null || this.UserDto.Email == null)
            {
                failureMessage = "Incorrect credentials. Please try again.";
                return;
            }

            // TODO - Remove, Debug only
            List<Role> roles = context.Roles.ToList();

            if (roles == null || !roles.Any(x => x.Name == UserDto.Role))
            {
                context.Roles.Add(
                    new Role()
                    {
                        Name = UserDto.Role,
                    }
                );
                context.SaveChanges();
            }

            User user = new User()
            {
                Email = UserDto.Email,
                Password = UserDto.Password,
                FirstName = UserDto.FirstName,
                LastName = UserDto.LastName,
                AddedOn = DateTime.Now,
            };

            try
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                failureMessage += ex.InnerException;
                return;
            }

            Role role = context.Roles.First(x => x.Name == UserDto.Role);
            UserRole userRole = new UserRole()
            {
                UserId = user.Id,
                RoleId = role.Id,
            };

            try
            {
                context.UserRoles.Add(userRole);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
                return;
            }

            CookieRepository cookie = new CookieRepository(user.Id);
            HttpContext.Session.SetString(cookie.GetCookieId(), role.Name);
            HttpContext.Response.Cookies.Append("LoggedInUser", cookie.GetCookieId(), new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1)
            });

            Response.Redirect(CustomRedirect.RoleRedirect(role.Name));
        }
    }
}

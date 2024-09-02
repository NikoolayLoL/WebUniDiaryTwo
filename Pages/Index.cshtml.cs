using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using WebUniDiary.Logic;
using WebUniDiary.Models.DTOs;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UniversityContext context;
        private readonly UserService userContext;

        [BindProperty]
        public UserDto userDto { get; set; } = new UserDto();
        public string failureMessage = string.Empty;

        public IndexModel(UniversityContext context, ILogger<IndexModel> logger, UserService userContext)
        {
            this.context = context;
            _logger = logger;
            this.userContext = userContext;
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            var result = context.Users.FirstOrDefault(u => u.Email == userDto.Email);

            if (null == result)
            {
                failureMessage = "Incorrect Credentials!";
                return;
            }

            if (!userContext.VerifyPassword(result.Password, userDto.Password.Trim()))
            {
                failureMessage = "Incorrect Password!";
                return;
            }

            User user = result;
            CookieRepository cookie = new CookieRepository(user.Id);

            UserRole userRole = context.UserRoles.First(u => u.UserId == user.Id);
            Role role = context.Roles.Find(userRole.RoleId)!;

            // If log in successful
            HttpContext.Session.SetString(cookie.GetCookieId(), role.Name);
            HttpContext.Response.Cookies.Append("LoggedInUser", cookie.GetCookieId(), new CookieOptions
            {
                Expires = DateTime.Now.AddHours(1)
            });

            Response.Redirect(CustomRedirect.RoleRedirect(role.Name));
        }
    }
}

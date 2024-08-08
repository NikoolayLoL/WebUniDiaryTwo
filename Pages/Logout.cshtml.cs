using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUniDiaryTwo.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages
{
    public class LogoutModel : PageModel
    {

        private readonly UniversityContext context;

        public LogoutModel(UniversityContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            
            if (HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue))
            {
                string storedUserCookie = (HttpContext.Session.GetString(cookieValue) ?? "");
                HttpContext.Session.Remove(storedUserCookie);
                Response.Cookies.Delete("LoggedInUser");
            }

            Response.Redirect("/");
        }
    }
}

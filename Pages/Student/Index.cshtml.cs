using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebUniDiary.Logic;
using WebUniDiaryTwo.Services;

namespace WebUniDiaryTwo.Pages.Student
{
    public class IndexModel : PageModel
    {
        public string CookieId { get; set; }
        public bool UserStudies { get; set; } = true;
        public int UserId { get; set; }
        public User User { get; set; } = new();
        public List<SemesterUser> SemesterUsers { get; set; } = new();

        private readonly UniversityContext _context;

        public IndexModel(UniversityContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            HttpContext.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue);
            var cookie = new CookieRepository(cookieValue);

            CookieId = cookie.GetCookieId();
            UserId = int.Parse(cookie.GetCookieId().Split('/').Skip(1).FirstOrDefault() ?? "0");

            try
            {
                SemesterUsers = _context.SemesterUsers
                    .Where(x => x.UserId == UserId)
                    .Include(x => x.Semester)
                    .ToList();

                User = _context.Users.Find(UserId) ?? new User();

                if (!SemesterUsers.Any())
                {
                    UserStudies = false;
                }
            }
            catch
            {
                UserStudies = false;
            }
        }
    }
}

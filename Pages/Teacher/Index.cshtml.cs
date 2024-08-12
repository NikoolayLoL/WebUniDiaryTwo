using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUniDiaryTwo.Pages.Teacher
{
    public class IndexModel : PageModel
    {
        public string successMessage = "false";
        public string failureMessage = "false";

        public void OnGet(string Success = "", string Failure = "")
        {
            this.successMessage = Success ?? "";
            this.failureMessage = Failure ?? "";
        }
    }
}

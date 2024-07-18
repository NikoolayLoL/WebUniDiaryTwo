using WebUniDiaryTwo.Logic;

namespace WebUniDiaryTwo.Services
{
    public class LoginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string INDEX = "/";

        public LoginMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var route = context.GetEndpoint()?.ToString();
            string controller = INDEX;
            string action = "Index";

            bool isLoggedIn = false;
            string userRole = "";

            if (route == null)
            {
                route = INDEX;
            }
            else
            {
                var paths = route.Split('/');
                if (paths.Length > 1)
                {
                    controller = paths[1].ToLower();
                }
                if (paths.Length > 2)
                {
                    action = paths[2];
                }
            }

            // Check if Login cookie exists and retrieve its value
            if (context.Request.Cookies.TryGetValue("LoggedInUser", out var cookieValue))
            {
                // Validate if session contains the cookie value
                string storedUserCookie = context.Session.GetString(cookieValue) ?? "";

                if (!string.IsNullOrEmpty(storedUserCookie))
                {
                    isLoggedIn = true;
                    userRole = storedUserCookie.ToLower();
                }
            }

            // Index page or Register
            if (route == INDEX || route.Equals("/Index", System.StringComparison.OrdinalIgnoreCase) || route.Equals("/Register", System.StringComparison.OrdinalIgnoreCase))
            {
                if (isLoggedIn)
                {
                    context.Response.Redirect(CustomRedirect.RoleRedirect(userRole));
                    return;
                }

                await _next(context);
                return;
            }

            // Guest tries to access a restricted Controller
            if (!isLoggedIn)
            {
                if (CustomRedirect.IsAuthorizationNeeded(controller))
                {
                    context.Response.Redirect(INDEX);
                    return;
                }
            }
            else
            {
                if (!CustomRedirect.IsUserAllowed(userRole, controller))
                {
                    context.Response.Redirect(CustomRedirect.RoleRedirect(userRole));
                    return;
                }
            }

            await _next(context);
        }
    }
}

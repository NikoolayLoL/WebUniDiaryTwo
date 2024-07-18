namespace WebUniDiaryTwo.Logic
{
    public static class CustomRedirect
    {
        public static string RoleRedirect(string role)
        {
            switch (role.ToLower())
            {
                case "admin":
                    return "/Admin/Index";
                case "student":
                    return "/Student/Index";
                case "teacher":
                    return "/Teacher/Index";
                default:
                    return "/Error";
            }
        }

        public static bool IsAuthorizationNeeded(string route)
        {
            switch (route.ToLower())
            {
                case "admin":
                case "student":
                case "teacher":
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsUserAllowed(string role, string route)
        {
            if (IsAuthorizationNeeded(route.ToLower()))
            {
                if (role != route)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

namespace WebUniDiary.Logic
{
    public class CookieRepository
    {
        private int userID { get; set; } = 0;
        private string uniqueCookieID { get; set; }

        // Brand new successful Login, create new CookieRepository with data for the session.
        public CookieRepository(int userID)
        {
            this.userID = userID;
            this.uniqueCookieID = Guid.NewGuid().ToString() + $"/{this.userID}";
        }

        // Session contains cookieID and userID, simply initialize, user already Logged in.
        public CookieRepository(string uniqueCookieId)
        {
            this.uniqueCookieID = uniqueCookieId;

            this.SplitUserId();
        }

        public string GetCookieId() => this.uniqueCookieID;
        public int GetUserId() => this.userID;
        private void SplitUserId()
        {
            int userId = this.uniqueCookieID.Split('/').Skip(1).Select(int.Parse).FirstOrDefault();
        }
    }
}

namespace UsersManagement.Options
{
    /// <summary>
    /// AuthenticationCookieBaseOption
    /// </summary>
    public class AuthenticationCookieBaseOption
    {
        public AuthenticationCookieBaseOption()
        {
            
        }
        public string LoginPath { get; set; }=string.Empty;
        public string LogoutPath { get; set; }=string.Empty;
        public string CookieName { get; set; }=string.Empty;
        public string AccessDeniedPath { get; set; }=string.Empty;
        public bool SlidingExpiration { get; set; }
        public bool CookieHttpOnly { get; set; }
        public TimeSpan ExpireTimeSpan { get; set; }

    }
}

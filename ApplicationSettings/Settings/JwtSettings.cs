namespace ApplicationSettings.Settings
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        /// <summary>
        /// In hours
        /// </summary>
        public int TimeToLiveInHours { get; set; }

        public string SecretKey { get; set; }
    }
}

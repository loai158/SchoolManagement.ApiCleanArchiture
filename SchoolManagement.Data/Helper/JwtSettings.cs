namespace SchoolManagement.Data.Helper
{
    public class JwtSettings
    {
        public string secretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double durationInMinutes { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifeTime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
        public int AccessTokenExpireDate { get; set; }
        public int RefreshTokenExpireDate { get; set; }
    }
}

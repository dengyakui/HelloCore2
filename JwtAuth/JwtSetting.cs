namespace JwtAuth
{
    public class JwtSetting
    {
        public string issuer { get; set; }
        public string audience { get; set; }
        public string secretKey { get; set; }
    }

}

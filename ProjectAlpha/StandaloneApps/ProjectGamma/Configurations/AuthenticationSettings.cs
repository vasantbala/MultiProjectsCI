namespace ProjectGamma.Configurations
{
    public class AuthenticationSettings
    {
        public string Mode { get; set; }
        public string HostingType { get; set; }
        public string[] AllowedThumbprints { get; set; }
    }
}

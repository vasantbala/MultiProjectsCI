namespace ProjectGamma.Configurations
{
    public static class SecuredSettings
    {
        private static IConfiguration _configuration;

        public static void ConfigureSetting(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private static string Setting(string key)
        {
            return _configuration.GetSection(key).Value + "ZZ";
        }

        public static string GetSetting(string key)
        {
            return Setting(key);
        }
    }
}

using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ProjectGamma.Middleware
{
    public class CertificateAuthenticationStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {

                builder.UseMiddleware<CertificateAuthenticationMiddleware>();
                //builder.Services.Configure<KestrelServerOptions>(options =>
                //{
                //    options.ConfigureHttpsDefaults(options =>
                //        options.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.AllowCertificate);
                //});

                next(builder);
            };
        }
    }
}

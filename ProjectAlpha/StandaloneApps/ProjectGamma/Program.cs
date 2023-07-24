using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProjectGamma;
using ProjectGamma.Configurations;
using ProjectGamma.Middleware;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFoo(builder.Configuration);

builder.Services.AddTransient<IStartupFilter, CertificateAuthenticationStartupFilter>();
var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("AuthenticationSettings").Bind(authenticationSettings);

var key = builder.Configuration.GetSection("SecuredSettings:ApiKey").Value;
if (authenticationSettings?.Mode == "Certificate")
{
    if (authenticationSettings.HostingType.Contains("Kestrel"))
    {
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.ConfigureHttpsDefaults(options => 
            options.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.AllowCertificate);
                
        });
    }

    builder.Services
        .AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
        .AddCertificate(options =>
        {
            options.AllowedCertificateTypes = CertificateTypes.All;
            options.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
            options.Events = new CertificateAuthenticationEvents()
            {
                OnCertificateValidated = context =>
                {
                    if (authenticationSettings.AllowedThumbprints.Contains(context.ClientCertificate.Thumbprint, StringComparer.OrdinalIgnoreCase))
                    {
                        var claims = new[]
                        {
                            new Claim(
                                ClaimTypes.NameIdentifier,
                                context.ClientCertificate.Subject,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer),
                            new Claim(
                                ClaimTypes.Name,
                                context.ClientCertificate.Subject,
                                ClaimValueTypes.String, context.Options.ClaimsIssuer)
                        };

                        context.Principal = new ClaimsPrincipal(
                            new ClaimsIdentity(claims, context.Scheme.Name));
                        context.Success();

                        
                    }
                    return Task.CompletedTask;
                },
                OnAuthenticationFailed = context => 
                {
                    return Task.CompletedTask;
                }

            };
        }
        );
}



var app = builder.Build();

SecuredSettings.ConfigureSetting(app.Services.GetRequiredService<IConfiguration>());

var apiKey = SecuredSettings.GetSetting("SecuredSettings:ApiKey");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

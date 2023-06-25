using Microsoft.AspNetCore.Server.Kestrel.Core;
using ProjectGamma.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("AuthenticationSettings").Bind(authenticationSettings);

if (authenticationSettings?.Mode == "Certificate")
{
    if (authenticationSettings.HostingType.Contains("Kestrel"))
    {
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.ConfigureHttpsDefaults(options =>
                options.ClientCertificateMode = Microsoft.AspNetCore.Server.Kestrel.Https.ClientCertificateMode.RequireCertificate);
        });
    }
}



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

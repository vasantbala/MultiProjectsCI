using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectGamma.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Check()
        {
            var clientCert = await Request.HttpContext.Connection.GetClientCertificateAsync();
            return "OKAY";
        }
    }
}

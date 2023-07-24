using System.Net;

namespace ProjectGamma.Middleware
{
    public class CertificateAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CertificateAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Test with https://localhost:5001/Privacy/?option=Hello
        public async Task Invoke(HttpContext httpContext)
        {

            var clientCert = httpContext.Connection.GetClientCertificateAsync();

            await _next(httpContext);
        }
    }


}

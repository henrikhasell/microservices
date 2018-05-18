using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// Note to self:
// http://blog.hovland.xyz/2017-04-09-simple-and-elegant-microservices-authentication-using-JWT/

namespace Authentication.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private readonly string tokenIssuer;
        private readonly byte[] tokenKey;

        public TokenController(IConfiguration configuration)
        {
            this.tokenIssuer = configuration["Jwt:Issuer"];
            this.tokenKey = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);
        }
    }
}
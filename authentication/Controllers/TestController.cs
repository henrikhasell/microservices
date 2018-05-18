using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Authentication.Controllers
{
    public class TestController : Controller
    {
        private readonly IConfiguration configuration;

        public TestController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        
        public string Index()
        {
            return "Hello, world!";
        }
        
        [Authorize]
        public string[] Names()
        {
            return new []
            {
                "Henrik",
                "Alex",
                "James",
                "Sam"
            };
        }

        [AllowAnonymous, HttpPost]
        public IActionResult GenerateToken([FromBody]LoginModel model)
        {
            string token = null;

            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = Authorize(model);

                if(applicationUser != null)
                {
                    token = BuildToken(applicationUser);
                }
            }
            
            if(!string.IsNullOrEmpty(token))
            {
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }

        [NonAction]
        protected ApplicationUser Authorize(LoginModel model)
        {
            ApplicationUser applicationUser = null;

            if(model.Username == "henrik" && model.Password == "password")
            {
                applicationUser = new ApplicationUser()
                {
                    Name = "Henrik Adam Hasell",
                    Email = "henrikhasell@gmail.com"
                };
            }

            return applicationUser;
        }

        [NonAction]
        protected string BuildToken(ApplicationUser applicationUser)
        {
            JwtSecurityToken securityToken = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                new Claim[]{
                    new Claim(nameof(applicationUser.Name), applicationUser.Name),
                    new Claim(nameof(applicationUser.Email), applicationUser.Email),
                }, null,
                DateTime.Now.AddMinutes(30),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])), SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

// https://amanagrawal.blog/2017/09/18/jwt-token-authentication-with-cookies-in-asp-net-core/
// https://stormpath.com/blog/token-authentication-asp-net-core

namespace Authentication
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup (IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie();
        }
        
        public void Configure(IApplicationBuilder applicationBuilder, IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                applicationBuilder.UseDeveloperExceptionPage();
            }
            
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseMvcWithDefaultRoute();
        }
    }
}

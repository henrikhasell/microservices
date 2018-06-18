using System;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
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
                options.DefaultSignOutScheme =
                    CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.Cookie.Expiration = TimeSpan.FromMinutes(5);


                SecurityKey signingKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(configuration["Token:SigningKey"]));
                    
                TokenValidationParameters validationParams = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,

                    ValidateAudience = true,
                    ValidAudience = configuration["Token:Audience"],

                    ValidateIssuer = true,
                    ValidIssuer = configuration["Token:Issuer"],

                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,

                    RequireExpirationTime = true,
                    ValidateLifetime = true
                };
                
                options.TicketDataFormat = new JwtAuthTicketFormat(validationParams,            
                    services
                        .BuildServiceProvider()
                        .GetService<IDataSerializer>(),
                    services
                        .BuildServiceProvider()
                        .GetDataProtector(new[] { $"{Environment.ApplicationName}-Auth1" })
                );

                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = options.LoginPath;
                options.ReturnUrlParameter = "returnUrl";
            });
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

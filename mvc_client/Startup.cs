using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace mvc_client
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // 配置单点授权
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = "cookie";
                option.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("cookie")
            .AddOpenIdConnect(option =>
            {
                option.SignInScheme = "oidc";
                option.SignOutScheme = "oidc";
                option.Authority = "http://localhost:5000";
                option.ClientId = "mvc";
                option.ClientSecret = "mvcsecret";
                option.SaveTokens = true;
                option.ResponseType = "code";

                option.Scope.Clear();
                option.Scope.Add("information");
                option.Scope.Add("openid");
                option.Scope.Add("profile");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}

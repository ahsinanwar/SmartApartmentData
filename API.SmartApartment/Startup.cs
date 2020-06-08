using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.SmartApartment.DataObjects;
using API.SmartApartment.Helper;
using API.SmartApartment.Services.Implementation;
using API.SmartApartment.Services.Interface;
using API.SmartApartment.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace API.SmartApartment
{
    /// <summary>
    /// This is a Startup class 
    /// </summary>
    /// <remarks>It is the entry point of the application. It configures the request pipeline which handles all requests made 
    /// to the .net core application. If we use any third party package in our application, we need to configure in this class. 
    /// e.g In this application we handled our authentication and authorization by Auth0, so we need to configue it here.</remarks>
    public class Startup
    {
        /// <summary>
        /// This is the constructor of this class
        /// </summary>
        /// <param name="configuration">this is key and value of configuration properties for this application e.g we can 
        /// access properties which are defined in appsetting.json file by this interface</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        /// <summary>
        /// This function configures all services which your application need
        /// </summary>
        /// <param name="services">servies descriptors</param>
        /// <remarks>We have configured below services:
        /// -->Call AddAuthentication method with JWT Bearer tokens as the default authentication and challenge schemes.
        /// -->Use AddJwtBearer method to Configure Auth0 domain as the authority, and Auth0 API identifier 
        /// as the audience which is created in Auth0 dashboard
        /// -->Call AddAuthorization method to add permissions in requirement of policies you created to access rest api requests
        /// --> Configure developer designed services for dependencies handling
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = Configuration["Auth0:Audience"];
                // for Authorization
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:student", policy => policy.Requirements.Add(new VMScopeRequirement("read:student", domain)));
                options.AddPolicy("read:students", policy => policy.Requirements.Add(new VMScopeRequirement("read:students", domain)));
                options.AddPolicy("create:student", policy => policy.Requirements.Add(new VMScopeRequirement("create:student", domain)));
            });
            // register the scope authorization handler
            //////////////********TO-DO*************
            ///Make a seperate class to register developer designed dependencies to configure
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
            services.AddScoped<ICRUDService<DtoStudent>, StudentService>();

            services.AddControllers();
        }
        /// <summary>
        /// This function execute at runtime of the application
        /// </summary>
        /// <param name="app">This is application builder interface which configue application pipeline</param>
        /// <param name="env">This is interface provides web hosting environment</param>
        /// <remarks>This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
        /// We configure items for development or production environment</remarks>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Web.SmartApartment
{
    /// <summary>
    /// This is a Startup class 
    /// </summary>
    /// <remarks>t is the entry point of the application. It configures the request pipeline which handles all requests made 
    /// to the .net core application. In this class we have enabled authentication in ASP.NET Core web application by using 
    /// the OpenID Connect (OIDC) middleware.</remarks>

    public class Startup
    {
        /// <summary>
        /// This is the constructor of this class
        /// </summary>
        /// <param name="configuration">this is key and value of configuration properties for this application e.g we can access properties 
        /// which are defined in appsetting.json file by this interface</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method configures all services which this application need
        /// </summary>
        /// <param name="services">servies descriptors</param>
        /// <remarks>We have configured below services:
        /// -->Configure the OIDC authentication handler
        /// -->Add a call to AddOpenIdConnect  to configure the authentication scheme and pass "Auth0" as the authenticationScheme parameter.
        /// -->Configure other parameters, such as ClientId, ClientSecret or ResponseType which is saved in appsettings.json
        /// -->Handle Auth0 logout redirection
        /// -->To call API from MVC application, we need to obtain an Access Token issued for the API we want to call. To obtain the token, 
        /// pass an additional audience parameter containing the API identifier to the Auth0 authorization endpoint.
        /// 
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Configure the OIDC authentication handler
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie().AddOpenIdConnect("Auth0", options =>
            {
                // Set the authority to your Auth0 domain
                options.Authority = $"https://{Configuration["Auth0:Domain"]}";

                // Configure the Auth0 Client ID and Client Secret
                options.ClientId = Configuration["Auth0:ClientId"];
                options.ClientSecret = Configuration["Auth0:ClientSecret"];

                // Set response type to code
                options.ResponseType = OpenIdConnectResponseType.Code;

                // Configure the scope
                options.Scope.Add("openid");

                // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                options.CallbackPath = new PathString("/Home/Index");

                // Configure the Claims Issuer to be Auth0
                options.ClaimsIssuer = "Auth0";
                options.SaveTokens = true;
                options.Events = new OpenIdConnectEvents
                {
                    // handle the logout redirection
                    OnRedirectToIdentityProviderForSignOut = (context) =>
                            {
                                var logoutUri = $"https://{Configuration["Auth0:Domain"]}/v2/logout?client_id={Configuration["Auth0:ClientId"]}";

                                var postLogoutUri = context.Properties.RedirectUri;
                                if (!string.IsNullOrEmpty(postLogoutUri))
                                {
                                    if (postLogoutUri.StartsWith("/"))
                                    {
                                        // transform to absolute
                                        var request = context.Request;
                                        postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                                    }
                                    logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                                }

                                context.Response.Redirect(logoutUri);
                                context.HandleResponse();

                                return Task.CompletedTask;
                            },
                    OnRedirectToIdentityProvider = context =>
                    {
                        context.ProtocolMessage.SetParameter("audience", Configuration["Auth0:API_IDENTIFIER"]);

                        return Task.FromResult(0);
                    }
                };
            });

            // Add framework services.
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Area",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

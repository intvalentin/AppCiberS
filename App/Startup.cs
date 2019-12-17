using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using App.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace App
{
    public class Startup
    {
        public readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(43200);
                options.Cookie.HttpOnly = false;
                // Make the session cookie essential
                options.Cookie.IsEssential = true;
                });
                services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });
            services.AddControllersWithViews(); ;
            services.AddMvc().AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;
            }); 
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddDbContext<appciberContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("AppciberDB")));


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = async (context) =>
                    {
                        context.HttpContext.Response.Redirect("https://localhost:5001/");
                    },
                   
                };
                //options.Cookie.HttpOnly = true;
                //options.LoginPath = "/Employees";
                //options.LogoutPath = "/Jobs";
        });

            //config =>
            //config.SlidingExpiration = true
            //options.SlidingExpiration = true;
            //config.LoginPath = "/Employees"
            //options.LogoutPath = ""

            //.AddJwtBearer(config =>
            //{

            //    config.RequireHttpsMetadata = false;
            //    config.SaveToken = true;

            //    //config.Events = new JwtBearerEvents()
            //    //{
            //    //    OnMessageReceived = context =>
            //    //    {
            //    //        if (context.Request.Query.ContainsKey("acces_token"))
            //    //        {
            //    //            context.Token = context.Request.Query["access_token"];
            //    //        }
            //    //        return Task.CompletedTask;
            //    //    }

            //    //};
            //    config.TokenValidationParameters = new TokenValidationParameters()
            //    {

            //        ValidateIssuer = false,
            //        ValidateAudience = false,
            //        //ValidAudience = "https://localhost:44389",
            //        //ValidIssuer = "https://localhost:44389",
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YVBy0OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SBM"))
            //    };
            //});



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (Configuration.GetValue<bool>("EnableDeveloperException"))
            {
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }

            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseRouting();
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
            endpoints.MapControllerRoute(
             name: "Home",
             pattern: "{controller=}/{action=Index}");

                   
        });
        }
    }
}

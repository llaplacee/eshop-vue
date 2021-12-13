using System;
using System.IO;
using System.Text;
using Core.Contracts;
using Core.Extensions;
using Core.Implementations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

namespace EshopApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public IHostingEnvironment HostingEnvironment { get; set; }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSingleton<IConfiguration>(new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json")
                .Build());

            services.AddApplicationDbContext(HostingEnvironment, Configuration);

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISliderService, SliderService>();
            services.AddTransient<IOrderService, OrderService>();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "http://localhost:13172",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyEshopWebApi")),
                    };
                });

            services.AddCors(options =>
            {
                options.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .Build();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            /*app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "Images")),
                RequestPath = new PathString("/Images")
            });*/

            app.UseCors("EnableCors");
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

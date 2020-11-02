using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Middlewares;
using API.Services;
using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
        // TODO NOW: Change default connection in appsettings.json
        // TODO NOW: Change UserSecretsId in API.csproj
        // TODO PRODUCTION: https://localhost:5001 Put in applicationUrl in PRODUCTION
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services from Inftastructure and Application project
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            // add CORS, cross origin, allowing client-app to communicate with API
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    // TODO NOW: Change origin
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:3000")
                        .AllowCredentials();
                });
            });

            services.AddControllers(options =>
            {
                // TODO NOW: Uncomment if you dont need authorize for all controllers
                // Adding Authorize for all Controllers
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddNewtonsoftJson();

            // Register the Swagger generator
            services.AddSwaggerGen(c =>
            {
                // TODO NOW: Change title
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectName API", Version = "v1", });
                c.CustomSchemaIds(x => x.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpint
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("v1/swagger.json", "My API V1");
                });
            }

            app.UseCustomExceptionHandler();

            // TODO PRODUCTION: Uncomment in PRODUCTION
            // app.UseHttpsRedirection();

            // TODO PRODUCTION: Security Headers - uncomment this in production
            // app.UseXContentTypeOptions();
            // app.UseReferrerPolicy(opt => opt.NoReferrer());
            // app.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            // app.UseXfo(opt => opt.Deny());
            // app.UseRedirectValidation();
            // app.UseCsp(opt => opt
            //     .BlockAllMixedContent()
            //     .StyleSources(s => s.Self().CustomSources("https://fonts.googleapis.com", "sha256-F4GpCPyRepgP5znjMD8sc7PEjzet5Eef4r09dEGPpTs="))
            //     .StyleSources(s => s.UnsafeInline())
            //     .FontSources(s => s.Self().CustomSources("https://fonts.gstatic.com"))
            //     .FormActions(s => s.Self())
            //     .FrameAncestors(s => s.Self())
            //     .ImageSources(s => s.Self().CustomSources("https://res.cloudinary.com", "blob:", "data:"))
            //     .ScriptSources(s => s.Self().CustomSources("sha256-ma5XxS1EBgt17N22Qq31rOxxRWRfzUTQS1KOtfYwuNo="))
            //   );



            // TODO PRODUCTION: Static Files -  for wwwroot, uncomment this in production
            // app.UseDefaultFiles();
            // app.UseStaticFiles()

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // TODO PRODUCTION:  static files, index.html from react app, create Fallback controller
                //endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Versioning;
using MicroFz.Common.Config;
//using Microsoft.Web.Http.Routing;
using Microsoft.AspNetCore.Mvc.Abstractions;
using MicroFz.Business;
using MicroFz.Data;
using CoreBase.Common.Middleware;

namespace MicroFz.Api
{
    public static class ActionDescriptorExtensions
    {
        public static ApiVersionModel GetApiVersion(this ActionDescriptor actionDescriptor)
        {
            return actionDescriptor?.Properties
              .Where((kvp) => ((Type)kvp.Key).Equals(typeof(ApiVersionModel)))
              .Select(kvp => kvp.Value as ApiVersionModel).FirstOrDefault();
        }
    }
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IMicroFzUnitOfWork, MicroFzUnitOfWork>();
            services.AddScoped<IPersonBusiness, PersonBusiness>();

            // services.AddControllers();
            services.AddMvcCore().AddApiExplorer();
            services.AddHttpContextAccessor();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "MicroFz Api Version 1", Version = "v1.0" });
                c.SwaggerDoc("v2.0", new OpenApiInfo { Title = "MicroFz Api Version 2", Version = "v2.0" });
                c.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var actionApiVersionModel = apiDesc.ActionDescriptor?.GetApiVersion();
                    // would mean this action is unversioned and should be included everywhere
                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }
                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v => $"v{v.ToString()}" == docName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v => $"v{v.ToString()}" == docName);
                });
               // c.OperationFilter<RequestResponseLog>();
                //c.OperationFilter<AddRequiredHeaderParameter>(); 
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
            services.Configure<Settings>(Configuration.GetSection("Settings"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseMiddleware<RequestResponseLog>();


            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<DataBaseContext>();
            //    context.Database.EnsureCreated();
            //}
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "MicroFz Api Version 1");
                    c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "MicroFz Api Version 2");
                });
            }
            else /// Hust for Tesing
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "MicroFz Api Version 1");
                    c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "MicroFz Api Version 2");
                });
            }
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}

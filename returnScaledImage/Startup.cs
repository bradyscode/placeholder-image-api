using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using returnScaledImage.Interfaces;
using returnScaledImage.Interfaces.ImageSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using returnScaledImage.Models;
using returnScaledImage.Interfaces.Icon;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Events;
using returnScaledImage.Middleware;

namespace returnScaledImages
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File(new JsonFormatter(),
                          "logs.json",
                          restrictedToMinimumLevel: LogEventLevel.Warning)
            .WriteTo.File("LogFile.logs",
                          rollingInterval: RollingInterval.Day)
            .MinimumLevel.Debug()
            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IImageSource, NatureImageSource>();
            services.AddScoped<IImageSource, KittenImageSource>();
            services.AddOptions<ImageSizeOptions>().Bind(Configuration.GetSection("ImageSizeOptions"));
            services.AddScoped<IImageRetreiver, ImageRetreiver>();
            services.AddScoped<IIconRetreiver, IconRetreiver>();
            services.AddLazyCache();
            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "resizeImage", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); //This line

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("./v1/swagger.json", "resizeImage v1"));
            }

            app.UseHttpsRedirection();
            
            app.UseRouting();

            app.UseAuthorization();
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

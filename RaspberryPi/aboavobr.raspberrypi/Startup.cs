using System.Runtime.InteropServices;
using aboavobr.raspberrypi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace aboavobr.raspberrypi
{
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
         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
         services.AddSwaggerGen(c =>
         {
            c.SwaggerDoc(
               "v1",
               new Info
               {
                  Title = "ABOAVOBR Rest API",
                  Version = "v1"                  
               });
         });

         services.AddSingleton<ISerialPortFactory, SerialPortFactory>();
         services.AddSingleton<ISerialCommunicationService, SerialCommunicationService>();
         services.AddSingleton<ISerialPortService, SerialPortService>();

         if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
         {
            // Use Dev Streaming Service
            services.AddSingleton<IStreamingService, FakeStreamingService>();
         }
         else
         {
            // We are probably on the raspberry now
            services.AddSingleton<IStreamingService, RaspberryPiStreamingService>();
         }
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseHsts();
         }

         app.UseSwagger();
         app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ABOAVOBR Rest API v1"));

         app.UseMvc();
      }
   }
}

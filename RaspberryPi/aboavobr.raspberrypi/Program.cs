using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace aboavobr.raspberrypi
{
   public class Program
   {
      public static void Main(string[] args)
      {
         CreateWebHostBuilder(args).Build().Run();
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args)
      {
         return WebHost.CreateDefaultBuilder(args)
            .UseUrls("https://*:5000")
            .UseStartup<Startup>();
      }
   }
}

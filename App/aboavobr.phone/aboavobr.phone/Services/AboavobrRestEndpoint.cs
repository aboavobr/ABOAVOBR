using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace aboavobr.phone.Services
{
   public class AboavobrRestEndpoint : IAboavobrRestEndpoint
   {
      private const string AppApiEndpoint = "/api/app";

      private string baseUrl;

      private readonly HttpClient client;

      public AboavobrRestEndpoint()
      {
         client = new HttpClient();
         client.Timeout = TimeSpan.FromSeconds(10);
         client.MaxResponseContentBufferSize = 256000;
      }

      public async Task<bool> Connect(string url)
      {
         var heartbeatUrl = $"{url}{AppApiEndpoint}/heartbeat";

         baseUrl = string.Empty;
         var uri = new Uri(heartbeatUrl);

         var response = await client.GetAsync(uri);

         if (response.IsSuccessStatusCode)
         {
            baseUrl = url;
            return true;
         }

         return false;
      }
   }
}

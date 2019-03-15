using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace aboavobr.phone.Services
{
   public class AboavobrRestEndpoint : IAboavobrRestEndpoint
   {
      private const string AppApiEndpoint = "/api/app";
      private const string DebugApiEndpoint = "/api/debug";

      private string baseUrl;

      private readonly HttpClient client;

      public AboavobrRestEndpoint()
      {
         client = new HttpClient
         {
            Timeout = TimeSpan.FromSeconds(10),
            MaxResponseContentBufferSize = 256000
         };
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

      public async Task SendCommandAsync(string valueToSend)
      {
         var commandUrl = $"{baseUrl}{DebugApiEndpoint}/serial";

         var uri = new Uri(commandUrl);
         var response = await client.PostAsync(uri, new StringContent(valueToSend, Encoding.UTF8, "application/json"));
      }

      public async Task<int> GetBatteryLifeAsync()
      {
         var commandUrl = $"{baseUrl}{AppApiEndpoint}/battery";

         var uri = new Uri(commandUrl);
         var response = await client.GetAsync(uri);

         if (response.IsSuccessStatusCode)
         {
            var content = await response.Content.ReadAsStringAsync();
            return int.Parse(content);
         }

         return -1;
      }
   }
}

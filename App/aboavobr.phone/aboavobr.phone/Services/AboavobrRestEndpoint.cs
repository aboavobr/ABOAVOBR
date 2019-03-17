using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace aboavobr.phone.Services
{
   public class AboavobrRestEndpoint : IAboavobrRestEndpoint
   {
      private const string AppApiEndpoint = "/api/app";
      private const string CameraApiEndpoint = "/api/camera";
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
         baseUrl = url;
         var uri = CreateAppUri("heartbeat");
         var response = await client.GetAsync(uri);

         if (response.IsSuccessStatusCode)
         {
            baseUrl = url;
            return true;
         }

         baseUrl = string.Empty;
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
         var uri = CreateAppUri("battery");
         var response = await client.GetAsync(uri);

         var content = await GetResponseContentAsync(response);
         if (!string.IsNullOrEmpty(content))
         {
            return int.Parse(content);
         }

         return -1;
      }

      public async Task<byte[]> GetImageAsync()
      {
         var uri = CreateCameraUri("image");
         var response = await client.GetAsync(uri);

         return await response.Content.ReadAsByteArrayAsync();
      }

      private async Task<string> GetResponseContentAsync(HttpResponseMessage response)
      {
         if (response.IsSuccessStatusCode)
         {
            var content = await response.Content.ReadAsStringAsync();
            return content;
         }

         return string.Empty;
      }

      private Uri CreateAppUri(string endpoint)
      {
         return CreateConnectionUri($"{AppApiEndpoint}/{endpoint}");
      }

      private Uri CreateCameraUri(string endpoint)
      {
         return CreateConnectionUri($"{CameraApiEndpoint}/{endpoint}");
      }

      private Uri CreateConnectionUri(string endpoint)
      {
         var url = $"{baseUrl}{endpoint}";
         return new Uri(url);
      }
   }
}

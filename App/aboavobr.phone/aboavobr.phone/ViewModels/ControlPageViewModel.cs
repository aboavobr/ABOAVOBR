using System;
using System.Threading;
using aboavobr.phone.Services;

namespace aboavobr.phone.ViewModels
{
   public class ControlPageViewModel : ViewModelBase, IControlPageViewModel
   {
      private const string UknownBatteryLife = "Unknown";
      private readonly IAboavobrRestEndpoint aboavobrRestEndpoint;
      private string batteryLifeInPercent = UknownBatteryLife;

      public ControlPageViewModel(IAboavobrRestEndpoint aboavobrRestEndpoint)
      {
         this.aboavobrRestEndpoint = aboavobrRestEndpoint;

         new Timer(SendBatteryLifeRequest, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(30));
      }

      public string BatteryLifeInPercent
      {
         get
         {
            return batteryLifeInPercent;
         }

         set
         {
            SetProperty(ref batteryLifeInPercent, value);
         }
      }

      private async void SendBatteryLifeRequest(object state)
      {
         var batteryLife = await aboavobrRestEndpoint.GetBatteryLifeAsync();

         if (batteryLife > 0)
         {
            BatteryLifeInPercent = $"{batteryLife}%";
         }
         else
         {
            BatteryLifeInPercent = UknownBatteryLife;
         }
      }
   }
}

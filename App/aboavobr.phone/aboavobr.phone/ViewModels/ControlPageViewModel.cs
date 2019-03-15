using System;
using System.Threading;
using aboavobr.phone.Services;

namespace aboavobr.phone.ViewModels
{
   public class ControlPageViewModel : ViewModelBase, IControlPageViewModel
   {
      private const string UknownBatteryLife = "Unknown";
      private const int BatteryHealthyStateThreshold = 20;

      private readonly IAboavobrRestEndpoint aboavobrRestEndpoint;
      private readonly IUiService uiService;
      private string batteryLifeInPercent = UknownBatteryLife;
      private bool wasAlreadyNotified;

      public ControlPageViewModel(IAboavobrRestEndpoint aboavobrRestEndpoint, IUiService uiService)
      {
         this.aboavobrRestEndpoint = aboavobrRestEndpoint;
         this.uiService = uiService;
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

            if (!wasAlreadyNotified && batteryLife < BatteryHealthyStateThreshold)
            {
               wasAlreadyNotified = true;
               uiService.DisplayAlert("Battery Low", "Battery is low, consider chargin it up soon", "Ok");
            }
         }
         else
         {
            BatteryLifeInPercent = UknownBatteryLife;
         }
      }
   }
}

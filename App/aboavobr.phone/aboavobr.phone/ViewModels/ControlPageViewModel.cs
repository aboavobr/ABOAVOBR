using System;
using System.IO;
using System.Threading;
using aboavobr.phone.Services;
using Xamarin.Forms;

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
      private ImageSource imageSource;

      private bool cameraSupportChecked;

      public ControlPageViewModel(IAboavobrRestEndpoint aboavobrRestEndpoint, IUiService uiService)
      {
         this.aboavobrRestEndpoint = aboavobrRestEndpoint;
         this.uiService = uiService;

         new Timer(SendBatteryLifeRequest, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(30));
         new Timer(UpdatePreviewImage, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(5));
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

      public ImageSource ImageSource
      {
         get
         {
            return imageSource;
         }

         set
         {
            SetProperty(ref imageSource, value);
         }
      }

      public bool CameraIsSupported { get; private set; }

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

      private async void UpdatePreviewImage(object state)
      {
         if (!cameraSupportChecked)
         {
            cameraSupportChecked = true;
            CameraIsSupported = await aboavobrRestEndpoint.IsCameraSupported();
         }

         if (CameraIsSupported)
         {
            var image = await aboavobrRestEndpoint.GetImageAsync();
            ImageSource = ImageSource.FromStream(() => new MemoryStream(image));
         }
      }
   }
}

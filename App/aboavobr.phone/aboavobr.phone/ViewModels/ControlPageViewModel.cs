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
      private Timer batteryLifeRequestTimer;
      private Timer previewImageTimer;
      private string batteryLifeInPercent = UknownBatteryLife;
      private bool wasAlreadyNotified;
      private ImageSource imageSource;

      private bool cameraSupportChecked;
      private bool cameraIsSupported;

      public ControlPageViewModel(IAboavobrRestEndpoint aboavobrRestEndpoint, IUiService uiService)
      {
         this.aboavobrRestEndpoint = aboavobrRestEndpoint;
         this.uiService = uiService;
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

      public bool CameraIsSupported
      {
         get
         {
            return cameraIsSupported;
         }
         private set
         {
            SetProperty(ref cameraIsSupported, value);
            OnPropertyChanged(nameof(DisplayCameraNotSupportedMessage));
         }
      }

      public bool DisplayCameraNotSupportedMessage => !CameraIsSupported;

      public void OnAppearing()
      {
         batteryLifeRequestTimer = new Timer(SendBatteryLifeRequest, null, TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(30));
         previewImageTimer = new Timer(UpdatePreviewImage, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));
      }

      public void OnDisappearing()
      {
         batteryLifeRequestTimer.Dispose();
         previewImageTimer.Dispose();
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

using System.Windows.Input;
using Xamarin.Forms;

namespace aboavobr.phone.ViewModels
{
   public interface IControlPageViewModel : IDisposableViewModel
   {
      string BatteryLifeInPercent { get; }

      bool CameraIsSupported { get; }

      bool DisplayCameraNotSupportedMessage { get; }

      ImageSource ImageSource { get; }
   }
}
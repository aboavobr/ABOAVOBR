using System.Windows.Input;
using Xamarin.Forms;

namespace aboavobr.phone.ViewModels
{
   public interface IControlPageViewModel
   {
      string BatteryLifeInPercent { get; }

      ImageSource ImageSource { get; }
   }
}
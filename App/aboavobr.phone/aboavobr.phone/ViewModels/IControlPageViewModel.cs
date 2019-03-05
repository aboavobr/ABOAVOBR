using System.Windows.Input;

namespace aboavobr.phone.ViewModels
{
   public interface IControlPageViewModel
   {
      ICommand ToggleCommand { get; }
   }
}
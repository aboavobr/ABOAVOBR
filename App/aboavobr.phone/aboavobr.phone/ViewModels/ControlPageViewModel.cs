using System.Threading.Tasks;
using System.Windows.Input;
using aboavobr.phone.Services;

namespace aboavobr.phone.ViewModels
{
   public class ControlPageViewModel : ViewModelBase, IControlPageViewModel
   {
      private readonly IAboavobrRestEndpoint aboavobrRestEndpoint;
      private bool isToggled = false;

      public ControlPageViewModel(IAboavobrRestEndpoint aboavobrRestEndpoint)
      {
         ToggleCommand = new DelegateCommand(async() => await ExecuteToggle());
         this.aboavobrRestEndpoint = aboavobrRestEndpoint;
      }

      public ICommand ToggleCommand { get; }

      private async Task ExecuteToggle()
      {
         var valueToSend = isToggled ? "0" : "1";
         isToggled = !isToggled;

         await aboavobrRestEndpoint.SendCommandAsync(valueToSend);
      }
   }
}

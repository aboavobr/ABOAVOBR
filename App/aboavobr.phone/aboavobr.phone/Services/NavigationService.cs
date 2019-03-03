using System.Threading.Tasks;
using aboavobr.phone.Views;

namespace aboavobr.phone.Services
{
   public class NavigationService : INavigationService
   {
      public async Task NavigateToControlPage()
      {
         await App.Current.MainPage.Navigation.PushAsync(new ControlPage());
      }
   }
}

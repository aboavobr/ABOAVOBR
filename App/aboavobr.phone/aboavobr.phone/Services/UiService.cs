using System.Threading.Tasks;
using aboavobr.phone.ViewModels;
using aboavobr.phone.Views;
using Autofac;
using Xamarin.Forms;

namespace aboavobr.phone.Services
{
   public class UiService : IUiService
   {
      private readonly IApp app;

      public UiService(IApp app)
      {
         this.app = app;
      }

      public async Task NavigateToControlPage()
      {
         var controlPage = new ControlPage
         {
            BindingContext = app.Container.Resolve<IControlPageViewModel>()
         };

         await SwitchPageAsync(controlPage);
      }

      public void DisplayAlert(string title, string message, string cancel)
      {
         Device.BeginInvokeOnMainThread(async () => await app.MainPage.DisplayAlert(title, message, cancel));
      }

      private async Task SwitchPageAsync(Page newPage)
      {
         await app.Navigation.PushAsync(newPage);
      }
   }
}

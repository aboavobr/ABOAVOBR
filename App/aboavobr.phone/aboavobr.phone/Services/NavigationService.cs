using System.Threading.Tasks;
using aboavobr.phone.ViewModels;
using aboavobr.phone.Views;
using Autofac;
using Xamarin.Forms;

namespace aboavobr.phone.Services
{
   public class NavigationService : INavigationService
   {
      private readonly IApp app;

      public NavigationService(IApp app)
      {
         this.app = app;
      }

      public async Task NavigateToControlPage()
      {
         var controlPage = new ControlPage
         {
            BindingContext = app.Container.Resolve<IControlPageViewModel>()
         };

         await app.Navigation.PushAsync(controlPage);
      }
   }
}

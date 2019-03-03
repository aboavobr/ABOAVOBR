using aboavobr.phone.Services;
using aboavobr.phone.ViewModels;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace aboavobr.phone
{
   public partial class App : Application
   {
      public App()
      {
         InitializeComponent();

         var container = InitializeServices();
         MainPage = new NavigationPage(new MainPage())
         {
            BindingContext = container.Resolve<IMainPageViewModel>()
         };
      }

      protected override void OnStart()
      {
         // Handle when your app starts
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }

      private IContainer InitializeServices()
      {
         var builder = new ContainerBuilder();
         builder.RegisterType<AboavobrRestEndpoint>().As<IAboavobrRestEndpoint>().SingleInstance();
         builder.RegisterType<NavigationService>().As<INavigationService>();

         builder.RegisterType<MainPageViewModel>().As<IMainPageViewModel>();

         return builder.Build();
      }
   }
}

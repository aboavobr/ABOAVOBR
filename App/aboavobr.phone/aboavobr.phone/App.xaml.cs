using aboavobr.phone.Services;
using aboavobr.phone.ViewModels;
using aboavobr.phone.Views;
using Autofac;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace aboavobr.phone
{
   public partial class App : Application, IApp
   {
      public App()
      {
         InitializeComponent();

         Container = InitializeServices();
         MainPage = new NavigationPage(new ConnectPage())
         {
            BindingContext = Container.Resolve<IMainPageViewModel>()
         };
      }

      public INavigation Navigation => MainPage.Navigation;

      public IContainer Container
      {
         get;
         private set;
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

         builder.RegisterInstance<IApp>(this).SingleInstance();

         builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();

         builder.RegisterType<MainPageViewModel>().As<IMainPageViewModel>();
         builder.RegisterType<ControlPageViewModel>().As<IControlPageViewModel>();

         return builder.Build();
      }
   }
}

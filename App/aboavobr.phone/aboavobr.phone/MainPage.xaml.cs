using aboavobr.phone.Services;
using aboavobr.phone.ViewModels;
using Xamarin.Forms;

namespace aboavobr.phone
{
   public partial class MainPage : ContentPage
   {
      public MainPage()
      {
         InitializeComponent();

         BindingContext = new MainPageViewModel(new AboavobrRestEndpoint());
      }
   }
}

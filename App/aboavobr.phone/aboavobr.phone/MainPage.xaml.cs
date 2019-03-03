using aboavobr.phone.ViewModels;
using Xamarin.Forms;

namespace aboavobr.phone
{
   public partial class MainPage : ContentPage
   {
      public MainPage(IMainPageViewModel mainPageViewModel)
      {
         InitializeComponent();
         BindingContext = mainPageViewModel;
      }
   }
}

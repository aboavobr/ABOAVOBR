
using aboavobr.phone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace aboavobr.phone.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ControlPage : ContentPage
   {
      public ControlPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();

         if (BindingContext is IDisposableViewModel dispoableViewModel)
         {
            dispoableViewModel.OnAppearing();
         }
      }

      protected override void OnDisappearing()
      {
         base.OnDisappearing();

         if (BindingContext is IDisposableViewModel dispoableViewModel)
         {
            dispoableViewModel.OnDisappearing();
         }
      }
   }
}
using Autofac;
using Xamarin.Forms;

namespace aboavobr.phone
{
   public interface IApp
   {
      INavigation Navigation { get; }

      IContainer Container { get; }

      Page MainPage { get; }
   }
}

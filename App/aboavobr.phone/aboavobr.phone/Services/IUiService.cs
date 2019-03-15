using System.Threading.Tasks;

namespace aboavobr.phone.Services
{
   public interface IUiService
   {
      Task NavigateToControlPage();

      void DisplayAlert(string title, string message, string cancel);
   }
}
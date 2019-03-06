namespace aboavobr.phone.ViewModels
{
   public interface IMainPageViewModel
   {
      DelegateCommand ConnectCommand { get; }

      DelegateCommand OpenSettingsCommand { get; }

      string ConnectionStatus { get; set; }

      string Error { get; set; }

      bool IsConnecting { get; set; }

      string Url { get; set; }
   }
}
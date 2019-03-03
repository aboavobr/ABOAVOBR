﻿using System;
using System.Threading.Tasks;
using aboavobr.phone.Services;

namespace aboavobr.phone.ViewModels
{
   public class MainPageViewModel : ViewModelBase
   {
      private readonly IAboavobrRestEndpoint aboavobrRestEndpoint;

      private string url;
      private string connectionStatus;
      private bool isConnecting;
      private string error;

      public MainPageViewModel(IAboavobrRestEndpoint aboavobrRestEndpoint)
      {
         connectionStatus = "Not Connected";
         ConnectCommand = new DelegateCommand(async () => await Connect(), () => !string.IsNullOrEmpty(Url));
         Url = "http://10.0.2.2:5000";
         this.aboavobrRestEndpoint = aboavobrRestEndpoint;
      }

      public string Url
      {
         get
         {
            return url;
         }

         set
         {
            SetProperty(ref url, value);
            ConnectCommand.RaiseCanExecuteChanged();
         }
      }

      public DelegateCommand ConnectCommand { get; }

      public bool IsConnecting
      {
         get
         {
            return isConnecting;
         }
         set
         {
            SetProperty(ref isConnecting, value);
         }
      }

      public string ConnectionStatus
      {
         get
         {
            return connectionStatus;
         }

         set
         {
            SetProperty(ref connectionStatus, value);
         }
      }

      public string Error
      {
         get
         {
            return error;
         }
         set
         {
            SetProperty(ref error, value);
         }
      }

      private async Task Connect()
      {
         var isConnected = false;

         try
         {
            ConnectionStatus = "Connecting...";
            IsConnecting = true;

            isConnected = await aboavobrRestEndpoint.Connect(Url);
         }
         catch (Exception ex)
         {
            isConnected = false;
            Error = ex.Message;
         }
         finally
         {
            IsConnecting = false;
         }

         if (isConnected)
         {
            ConnectionStatus = "Connected";
         }
         else
         {
            ConnectionStatus = "Connection Attempt failed";
         }
      }
   }
}

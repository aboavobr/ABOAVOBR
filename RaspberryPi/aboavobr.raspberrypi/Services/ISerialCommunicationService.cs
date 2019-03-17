
using System.Collections.Generic;

namespace aboavobr.raspberrypi.Services
{
    public interface ISerialCommunicationService
    {
        string PortName { get; }

        bool IsConnected { get; }

        void SendMessage(string message);

        int GetBatteryLife();
    }
}

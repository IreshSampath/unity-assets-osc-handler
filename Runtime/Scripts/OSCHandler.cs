using extOSC;
using System.Net.Sockets;
using UnityEngine;
using System.Net.NetworkInformation;

namespace GAG.OSCHandler
{
    public class OSCHandler : MonoBehaviour
    {
        #region Fields Transmitter

        [Header("OSC Settings")]
        public OSCTransmitter Transmitter;

        string _msg = "/Message";

        #endregion


        #region Fields Receiver

        [Header("OSC Settings")]
        public OSCReceiver Receiver;

        #endregion


        #region Unity Methods

        void OnEnable()
        {
            AppEvents.OnOscDashboardOpened += LoadDefaultData;

            AppEvents.OnRemotIPEntered += SetRemoteIP;
            AppEvents.OnMsgSent += SendMessageFromThisDevice;
        }

        void Start()
        {
            LoadDefaultData();
            Receiver.Bind(_msg, ReceivedMessageFromOtherDevice);
        }

        #endregion


        #region Other Methods

        public void LoadDefaultData()
        {
            AppEvents.RaiseOnLocalIPLoaded(GetWiFiIPAddress());
        }

        string GetWiFiIPAddress()
        {
            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Check if the interface is Wi-Fi
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
                    netInterface.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ipInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == AddressFamily.InterNetwork) // IPv4 only
                        {
                            return ipInfo.Address.ToString();
                        }
                    }
                }
            }

            //return "Wi-Fi IPv4 Not Found";
            return GetLocalIPAddress();
        }

        string GetLocalIPAddress()
        {
            string ipAddress = "Not Found";

            foreach (NetworkInterface netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.OperationalStatus == OperationalStatus.Up)  // Check if network is active
                {
                    foreach (UnicastIPAddressInformation ipInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == AddressFamily.InterNetwork) // IPv4 only
                        {
                            return ipInfo.Address.ToString();
                        }
                    }
                }
            }

            return ipAddress;
        }

        void SetRemoteIP(string ip)
        {
            Transmitter.RemoteHost = ip;
        }

        void SendMessageFromThisDevice(string msg)
        {
            var message = new OSCMessage(_msg);
            message.AddValue(OSCValue.String(msg));

            Transmitter.Send(message);
        }

        void ReceivedMessageFromOtherDevice(OSCMessage message)
        {
            if (message.ToString(out var value))
            {
                AppEvents.RaiseOnMsgRecieved(value);
            }
        }

        #endregion
    }
}

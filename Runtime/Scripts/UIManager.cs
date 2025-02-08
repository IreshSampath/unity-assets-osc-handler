using System;
using TMPro;
using UnityEngine;

namespace GAG.OSCHandler
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] GameObject _oscDashboardPanel;
        [SerializeField] TMP_Text _localIPTxt;

        [SerializeField] TMP_InputField _remoteIPInput;
        [SerializeField] TMP_InputField _portInput;
        [SerializeField] TMP_InputField _msgInput;

        [SerializeField] TMP_Text _consolTxt;

        void OnEnable()
        {
            AppEvents.OnOscDashboardOpened += ShowDashboard;
            AppEvents.OnMsgRecieved += ReceivedMessageFromOtherDevice;
            AppEvents.OnLocalIPLoaded += ShowLocalIP;
        }

        private void Start()
        {
            LoadDefaultData();
        }

        public void ShowInstructions()
        {
            string instructions = "" +
                "# Ensure all devices are connected to the same network.\n\n" +
                "# When changing the local network, click \"Refresh\" to update the local IP address.\n\n" +
                "# After changing the remote IP, don't forget to click \"Save\".\n\n" +
                "# If you're not receiving messages on the Windows app, disable the firewall for public networks.";

            PrintConsole(instructions);
        }

        void ShowDashboard()
        {
            LoadDefaultData();
            _oscDashboardPanel.SetActive(true);
        }

        void LoadDefaultData()
        {
            _remoteIPInput.text = PlayerPrefs.GetString("RemoteIpAdress", "0.0.0.0");
            _portInput.text = PlayerPrefs.GetString("Port", "7000");
            RegisterOtherDeviceIP();
            RegisterPort();
        }

        public void PrintConsole(string msg)
        {
            DateTime now = DateTime.Now;
            string currentTime = now.ToString("hh:mm:ss");

            string newMessage = $"{currentTime} : {msg}\n\n{_consolTxt.text}"; // Prepend new message
            _consolTxt.text = newMessage;

            //string prevMsg = _consolTxt.text;
            //_consolTxt.text = prevMsg + "\n" + currentTime + " : " + msg;
        }

        void ShowLocalIP(string ip)
        {
            _localIPTxt.text = ip;
        }

        void ReceivedMessageFromOtherDevice(string msg)
        {
            PrintConsole(msg);
        }

        public void RegisterOtherDeviceIP()
        {
            PlayerPrefs.SetString("RemoteIpAdress", _remoteIPInput.text);

            AppEvents.RaiseOnRemotIPEntered(_remoteIPInput.text);
            PrintConsole($"Remote IP Saved ({_remoteIPInput.text})");
        }

        public void RegisterPort()
        {
            PlayerPrefs.SetString("Port", _portInput.text);

            AppEvents.RaiseOnPortLoaded(_portInput.text);
            PrintConsole($"Port Saved ({_portInput.text})");
        }

        public void SetMsg()
        {
            AppEvents.RaiseOnMsgSentd(_msgInput.text);
            PrintConsole($"Message Sent ({ _msgInput.text})");
        }
    }
}

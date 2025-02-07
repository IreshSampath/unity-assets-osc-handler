using System;

namespace GAG.OSCHandler 
{
    public class AppEvents
    {
        public static event Action OnOscDashboardOpened;

        public static event Action<string> OnLocalIPLoaded;
        public static event Action<string> OnPortLoaded;

        public static event Action<string> OnRemotIPEntered;
        public static event Action<string> OnMsgSent;
        public static event Action<string> OnMsgRecieved;

        public static void RaiseOnOscDashboardOpened()
        {
            OnOscDashboardOpened?.Invoke();
        }

        public static void RaiseOnLocalIPLoaded(string ip)
        {
            OnLocalIPLoaded?.Invoke(ip);
        }

        public static void RaiseOnRemotIPEntered(string ip)
        {
            OnRemotIPEntered?.Invoke(ip);
        }

        public static void RaiseOnPortLoaded(string port)
        {
            OnPortLoaded?.Invoke(port);
        }

        public static void RaiseOnMsgSentd(string msg)
        {
            OnMsgSent?.Invoke(msg);
        }

        public static void RaiseOnMsgRecieved(string msg)
        {
            OnMsgRecieved?.Invoke(msg);
        }
    }
}

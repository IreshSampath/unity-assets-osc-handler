using GAG.OSCHandler;
using System.Net;
using UnityEngine;

public class AppManger : MonoBehaviour
{
    int _ipTapCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenOscDashboardMenu()
    {
        _ipTapCount++;

        if (_ipTapCount == 3)
        {
            AppEvents.RaiseOnOscDashboardOpened();

            _ipTapCount = 0;
        }
    }
}

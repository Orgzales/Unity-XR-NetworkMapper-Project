using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using System.Net.NetworkInformation;

public class HiddenSSID_Scan : MonoBehaviour
{
    private List<string> hiddenSSIDs = new List<string>();

    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        ScanForHiddenSSIDs();
    }

    void ScanForHiddenSSIDs()
    {
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");

        // Start Wi-Fi scan
        wifiManager.Call("startScan");

        // Get the scan results
        AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");
        int scanResultsCount = scanResults.Call<int>("size");

        for (int i = 0; i < scanResultsCount; i++)
        {
            AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
            string ssid = scanResult.Get<string>("SSID");

            // Check if SSID is hidden
            if (ssid.Length == 0)
            {
                // Add hidden SSID to the list
                hiddenSSIDs.Add("Hidden SSID Detected");
            }
        }

        // Display detected hidden SSIDs
        foreach (string hiddenSSID in hiddenSSIDs)
        {
            Debug.Log("Hidden SSID Detected: " + hiddenSSID);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.XR;

public class Checking_Internet : MonoBehaviour
{
    // Netsh WLAN show interfaces - command to get wifi name and signal

    public Text Wifi_is_Available;
    // public Text ssidText;
    public Text wifiNameText;

    private string wifiSSID = "Not Connected";
    // private string wifiSSID = "N/A";
    // private string networkName = "";
    void Start()
    {

        InvokeRepeating("test", 0, 30);






        // if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        // {
        //     Permission.RequestUserPermission(Permission.FineLocation);
        // }
        // InvokeRepeating("GetWiFiSSID", 0, 30);
        // StartCoroutine(AR_GetWiFiSSID());

        // StartCoroutine(GetWiFiSSIDCoroutine());
        // networkName = wifiSSID;
        // CheckSignalStrength();

    }
    // Update is called once per frame
    void Update()
    {

        // Wifi_is_Available.text = "Testing";

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Wifi_is_Available.text = "No Networks in Area";
            Wifi_is_Available.color = Color.red;
            // Debug.Log("No Networks in Area");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Wifi_is_Available.text = "Wifi Networks are Availiable";
            Wifi_is_Available.color = Color.green;
            // Debug.Log("Wifi Networks are Availiable");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Wifi_is_Available.text = "Network/Wifi Found through Mobile Accounts, Please log in";
            Wifi_is_Available.color = Color.yellow;
            // Debug.Log("Network/Wifi Found through Mobile Accounts, Please log in"); //FOR RESNET
        }
    }

    private void CheckSignalStrength()
    {
        // NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

        // foreach (var networkInterface in networkInterfaces)
        // {
        //     // string currentNetworkName = networkInterface.Name;

        //     // Debug.Log("Connected to Wi-Fi Network: " + networkName);
        //     if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
        //         networkInterface.Name.StartsWith("Wi-Fi"))
        //     {
        //         foreach (var unicastIPAddressInformation in networkInterface.GetIPProperties().UnicastAddresses)
        //         {
        //             if (unicastIPAddressInformation.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork &&
        //                 networkInterface.OperationalStatus == OperationalStatus.Up)
        //             {
        //                 string currentNetworkName = networkInterface.Name;
        //                 if (currentNetworkName.Equals("Wi-Fi") && networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
        //                 {
        //                     Debug.Log("Connected to Wi-Fi Network: " + networkName);
        //                     Debug.Log("Signal Strength: " + networkInterface.GetSignalStrength());
        //                 }
        //             }
        //         }
        //     }
        // }
    }

    // private IEnumerator GetWiFiSSIDCoroutine()
    // {
    //     // yield return new WaitForSeconds(1.0f);

    //     // NetworkInterface[] networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

    //     // foreach (var networkInterface in networkInterfaces)
    //     // {
    //     //     if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 &&
    //     //         networkInterface.OperationalStatus == OperationalStatus.Up)
    //     //     {
    //     //         wifiSSID = networkInterface.Description;
    //     //         break; 
    //     //     }
    //     // }
    //     // Debug.Log("Wi-Fi SSID: " + wifiSSID);

    //  yield return new WaitForSeconds(1.0f); // Wait for a moment (optional)

    //         #if UNITY_ANDROID && !UNITY_EDITOR
    //         AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
    //             .GetStatic<AndroidJavaObject>("currentActivity");
    //         AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
    //         AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
    //         int rssi = wifiInfo.Call<int>("getRssi");
    //         signalStrength = WifiManager.CalculateSignalLevel(rssi, 100);
    //         #else
    //         signalStrength = -1; 
    //         #endif
    //         Debug.Log("Signal Strength: " + signalStrength);

    // }

    private void test()
    {
        if (XRSettings.enabled && XRSettings.loadedDeviceName == "Oculus")
        {
            // Check if XR is currently active (indicating the Oculus Quest 2 is connected).
            if (XRSettings.isDeviceActive)
            {
                wifiNameText.text = "Oculus Quest 2 is plugged in!";
            }
            else
            {
                wifiNameText.text = "Oculus Quest 2 is not plugged in!!!!";
            }
        }
        else
        {
            wifiNameText.text = "Not an Oculus Quest device or XR is not enabled.";
        }

    }

    private void GetWiFiSSID()
    {
        Process process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/C netsh wlan show interfaces";
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.CreateNoWindow = true;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        process.WaitForExit();

        // Parse the output to find the SSID
        string[] lines = output.Split('\n');
        foreach (string line in lines)
        {
            if (line.Contains("BSSID"))
            {
                int startIndex = line.IndexOf(": ") + 2;
                wifiSSID = line.Substring(startIndex).Trim();
                break;
            }
        }

        // Print or use the retrieved SSID
        UnityEngine.Debug.Log("Windows Wi-Fi SSID: " + wifiSSID);

        // You can now use the 'wifiSSID' variable to access the SSID in your application.
    }


    // private IEnumerator AR_GetWiFiSSID()
    // {
    //     yield return new WaitForSeconds(1.0f); // Wait for a moment (optional)

    //     AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer")
    //         .GetStatic<AndroidJavaObject>("currentActivity");
    //     AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
    //     AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
    //     wifiSSID = wifiInfo.Call<string>("getSSID").Replace("\"", ""); // Remove surrounding quotes

    //     // Print or use the retrieved SSID
    //     UnityEngine.Debug.Log("Quest 2 Wi-Fi SSID!!!!!!!!: " + wifiSSID);

    //     // Display the SSID in a UI Text element (optional)
    //     if (ssidText != null)
    //     {
    //         ssidText.text = "SSID: " + wifiSSID;
    //     }

    //     // You can now use the 'wifiSSID' variable to access the SSID in your VR application on Oculus Quest 2.
    // }

}


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


    public Text Wifi_is_Available; //To display there is a connection avialble
    public Text SSID_text; //SSID = Name of wifi
    public string wifiSSID;

    public Text BSSID_text; //BSSID = address of accesspoint
    public string wifiBSSID;

    public Text Singal_STR_text; //Power of strength of wifi
    public int wifiSignalStrength;

    public Text wifiAuthentication_text; //Type of authencation
    public string wifiAuthentication;

    // public string testing = "This is wifi script";

    void Start()
    {

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        InvokeRepeating("AR_repeat_wifi", 0, 6);


    }
    // Update is called once per frame
    void Update()
    {



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

    // private void GetWiFiSSID()
    // {
    //     Process process = new Process();
    //     process.StartInfo.FileName = "cmd.exe";
    //     process.StartInfo.Arguments = "/C netsh wlan show interfaces";
    //     process.StartInfo.UseShellExecute = false;
    //     process.StartInfo.RedirectStandardOutput = true;
    //     process.StartInfo.CreateNoWindow = true;
    //     process.Start();

    //     string output = process.StandardOutput.ReadToEnd();
    //     process.WaitForExit();

    //     // Parse the output to find the SSID
    //     string[] lines = output.Split('\n');
    //     foreach (string line in lines)
    //     {
    //         if (line.Contains("SSID"))
    //         {
    //             int startIndex = line.IndexOf(": ") + 2;
    //             wifiSSID = line.Substring(startIndex).Trim();
    //             break;
    //         }
    //     }

    //     // Print or use the retrieved SSID
    //     // wifiNameText.text = ("Windows Wi-Fi SSID: " + wifiSSID);

    //     // You can now use the 'wifiSSID' variable to access the SSID in your application.
    // }


    private void AR_repeat_wifi()
    {
        StartCoroutine(AR_GetWiFiSSID());
    }
    private IEnumerator AR_GetWiFiSSID()
    {
        yield return new WaitForSeconds(1.0f);
        // UnityEngine.Debug.Log("!!!!");

        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
        AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
        wifiSSID = wifiInfo.Call<string>("getSSID").Replace("\"", "");
        wifiBSSID = wifiInfo.Call<string>("getBSSID");
        wifiSignalStrength = wifiInfo.Call<int>("getRssi");


        string capabilities = wifiInfo.Call<string>("getCapabilities");
        if (capabilities.Contains("WEP"))
        {
            wifiAuthentication = "WEP";
        }
        else if (capabilities.Contains("PSK"))
        {
            wifiAuthentication = "WPA2";
        }
        else if (capabilities.Contains("SAE"))
        {
            wifiAuthentication = "WPA3";
        }
        else if (capabilities.Contains("WPA"))
        {
            wifiAuthentication = "WPA";
        }
        else if (capabilities.Contains("EAP"))
        {
            wifiAuthentication = "EAP";
        }
        else
        {
            wifiAuthentication = "OPEN";
        }




        // if (supplicantState != null)
        // {
        //     int supplicantStateValue = supplicantState.Call<int>("ordinal");

        //     switch (supplicantStateValue)
        //     {
        //         case 3: // SupplicantState.COMPLETED
        //             wifiAuthentication = "WPA2"; // Assuming WPA2 for completed connections
        //             break;
        //         case 6: // SupplicantState.FOUR_WAY_HANDSHAKE
        //             wifiAuthentication = "WPA2"; // You can consider it WPA2 during handshakes
        //             break;
        //         case 7: // SupplicantState.GROUP_HANDSHAKE
        //             wifiAuthentication = "WPA2"; // You can consider it WPA2 during handshakes
        //             break;
        //         case 8: // SupplicantState.COMPLETED
        //             wifiAuthentication = "WPA3"; // Assuming WPA3 for completed connections
        //             break;
        //         default:
        //             wifiAuthentication = "OPEN"; // Default to OPEN
        //             // You can add more cases for other SupplicantState values if needed
        //             break;
        //     }
        // }


        // string capabilities = wifiInfo.Call<string>("getCapabilities");

        // // Check capabilities to determine authentication type
        // if (capabilities.Contains("WEP"))
        // {
        //     wifiAuthentication = "WEP";
        // }
        // else if (capabilities.Contains("WPA2") || capabilities.Contains("PSK"))
        // {
        //     wifiAuthentication = "WPA2";
        // }
        // else if (capabilities.Contains("WPA3") || capabilities.Contains("SAE"))
        // {
        //     wifiAuthentication = "WPA3";
        // }
        // else if (capabilities.Contains("WPA"))
        // {
        //     wifiAuthentication = "WPA";
        // }
        // else if (capabilities.Contains("EAP"))
        // {
        //     wifiAuthentication = "EAP";
        // }
        // else
        // {
        //     wifiAuthentication = "OPEN";
        // }

        // // Retrieve Wi-Fi configuration for Quest 2
        // AndroidJavaObject wifiConfigList = wifiManager.Call<AndroidJavaObject>("getConfiguredNetworks");
        // int networkId = wifiInfo.Call<int>("getNetworkId");

        // // Find the network Authtication Type base on keymanagement
        // for (int i = 0; i < wifiConfigList.Call<int>("size"); i++)
        // {
        //     AndroidJavaObject config = wifiConfigList.Call<AndroidJavaObject>("get", i);
        //     int configNetworkId = config.Call<int>("networkId");
        //     if (configNetworkId == networkId)
        //     {
        //         int keyManagement = config.Call<int>("allowedKeyManagement");
        //         switch (keyManagement)
        //         {
        //             case 0:
        //                 wifiAuthentication = "OPEN";
        //                 wifiAuthentication_text.color = Color.red;
        //                 break;
        //             case 1:
        //                 wifiAuthentication = "WEP";
        //                 wifiAuthentication_text.color = Color.magenta;
        //                 break;
        //             case 2:
        //             case 3:
        //                 wifiAuthentication = "WPA";
        //                 wifiAuthentication_text.color = Color.yellow;
        //                 break;
        //             case 4:
        //             case 6:
        //                 wifiAuthentication = "WPA2";
        //                 wifiAuthentication_text.color = Color.green;
        //                 break;
        //             case 8:
        //             case 9:
        //                 wifiAuthentication = "WPA3";
        //                 wifiAuthentication_text.color = Color.blue;
        //                 break;
        //             default:
        //                 wifiAuthentication = "UNKNOWN";
        //                 wifiAuthentication_text.color = Color.gray;
        //                 break;
        //         }
        //         break;
        //     }
        // }


        // Display the SSID in a UI Text element 
        if (string.IsNullOrEmpty(wifiSSID) || wifiSSID.Equals("Not Connected"))
        {
            SSID_text.text = "No Networks in Area";
            BSSID_text.text = "No Networks in Area";
            Singal_STR_text.text = "No Networks in Area";
            wifiAuthentication_text.text = "No Networks in Area";
            SSID_text.color = Color.red;
            BSSID_text.color = Color.red;
            Singal_STR_text.color = Color.red;
            wifiAuthentication_text.color = Color.red;
        }
        else
        {
            SSID_text.text = "SSID: " + wifiSSID;
            BSSID_text.text = "BSSID: " + wifiBSSID;
            Singal_STR_text.text = "STRENGTH: " + wifiSignalStrength.ToString() + "dBm";
            wifiAuthentication_text.text = "Authentication: " + wifiAuthentication;
            SSID_text.color = Color.green;
            BSSID_text.color = Color.green;
            if (wifiSignalStrength >= -67) //-67 = Amazing in dbm
            {
                Singal_STR_text.color = Color.green;
            }
            else if (wifiSignalStrength >= -79 && wifiSignalStrength < -70) // -70 = okay
            {
                Singal_STR_text.color = Color.yellow;
            }
            else if (wifiSignalStrength < -80) // -80 or lower is bad
            {
                Singal_STR_text.color = Color.red;
            }
        }


    }

}


using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.XR;
using System;
using TMPro;


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

    public Text bestSecuirty_text; //best authentication the network can provide
    public string bestSecuirty;

    public Text receiverate_text; //recieve rate of wifi
    public int receiverate;

    public Text transmitrate_text; //transmit rate of wifi
    public int transmitrate;


    void Start()
    {

        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }

        InvokeRepeating("AR_repeat_wifi", 0, 3);
        // InvokeRepeating("GetWiFiSSID", 0, 6);


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
    //Debugginh on windows instead of andriod
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
    //         if (line.Contains("Authentication"))
    //         {
    //             int startIndex = line.IndexOf(": ") + 2;
    //             wifiSSID = line.Substring(startIndex).Trim();
    //             break;
    //         }
    //     }
    //     UnityEngine.Debug.Log(wifiSSID);

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

        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
        AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");

        AndroidJavaObject connectivityManager = activity.Call<AndroidJavaObject>("getSystemService", "connectivity");
        AndroidJavaObject networkInfo = connectivityManager.Call<AndroidJavaObject>("getNetworkInfo", 1);
        AndroidJavaObject detailedState = networkInfo.Call<AndroidJavaObject>("getDetailedState");

        //code for finding best secuirty
        AndroidJavaObject activeNetwork = connectivityManager.Call<AndroidJavaObject>("getActiveNetwork");
        AndroidJavaObject networkCapabilities = connectivityManager.Call<AndroidJavaObject>("getNetworkCapabilities", activeNetwork);

        wifiSSID = wifiInfo.Call<string>("getSSID").Replace("\"", "");
        wifiBSSID = wifiInfo.Call<string>("getBSSID");
        wifiSignalStrength = wifiInfo.Call<int>("getRssi");
        receiverate = wifiInfo.Call<int>("getLinkSpeed");
        transmitrate = wifiInfo.Call<int>("getTxLinkSpeed");


        //Yes all these lines to just to check Authencation
        string stateString = detailedState.Call<string>("toString");
        string capabilities = wifiInfo.Call<string>("toString");
        int securityTypeIndex = capabilities.IndexOf("Security type: ");
        int endOfSecurityTypeIndex = capabilities.IndexOf(",", securityTypeIndex);
        string securityTypeValue = capabilities.Substring(securityTypeIndex + 15, endOfSecurityTypeIndex - securityTypeIndex - 15);

        switch (securityTypeValue)
        {
            case "-1":
                wifiAuthentication = "No Security/Signal";
                wifiAuthentication_text.color = Color.red;
                break;
            case "0":
                wifiAuthentication = "OPEN"; //No password 
                wifiAuthentication_text.color = Color.red;
                break;
            case "1":
                wifiAuthentication = "WEP"; //old be cautious 
                wifiAuthentication_text.color = Color.yellow;
                break;
            case "2":
                wifiAuthentication = "WPA/WPA2"; //mondern security
                wifiAuthentication_text.color = Color.green;
                break;
            case "3":
                wifiAuthentication = "WPA3"; //high secuirty
                wifiAuthentication_text.color = Color.blue;
                break;
            default:
                wifiAuthentication = "Unknown Type: " + securityTypeValue; //Incase Im missing any that shows up.
                wifiAuthentication_text.color = Color.magenta;
                break;
        }


        //Code to check for the best possible authentication of server
        bool hasInternet = networkCapabilities.Call<bool>("hasCapability", 12);
        bool hasWep = networkCapabilities.Call<bool>("hasCapability", 15);
        bool hasWpa2 = networkCapabilities.Call<bool>("hasCapability", 13);
        bool hasWpa3 = networkCapabilities.Call<bool>("hasCapability", 26);
        bool hasCcmp = hasWpa2 && networkCapabilities.Call<bool>("hasCapability", 6);
        // int networkId = wifiInfo.Call<int>("getNetworkId");
        // AndroidJavaObject wifiConfig = wifiManager.Call<AndroidJavaObject>("getConfiguredNetwork", networkId);


        if (!hasWpa2 && !hasWep)
        {
            bestSecuirty = "Open Authentication";
            bestSecuirty_text.color = Color.red;
        }
        else
        {
            // bestSecuirty = $"Internet: {hasInternet}, WPA3: {hasWpa3}, WPA2: {hasWpa2}, WEP: {hasWep}"; //Debug
            if (hasWep == true)
            {
                bestSecuirty = "WEP";
                bestSecuirty_text.color = Color.yellow;
            }
            if (hasWpa2 == true)
            {
                bestSecuirty = "WPA2";
                bestSecuirty_text.color = Color.green;
            }
            if (hasWpa3 == true)
            {
                bestSecuirty = "WPA3";
                bestSecuirty_text.color = Color.blue;
            }
        }


        if (string.IsNullOrEmpty(wifiSSID) || wifiSSID.Equals("<unknown ssid>"))
        {
            SSID_text.text = "SSID: No Connection";
            BSSID_text.text = "BSSID: No Connection";
            Singal_STR_text.text = "STRENGTH: No Connection";
            wifiAuthentication_text.text = "CURRENT SECUIRTY: No Connection";
            bestSecuirty_text.text = "BEST SECUIRTY: No Connection";
            receiverate_text.text = "RECEIVE RATE: No Connection";
            transmitrate_text.text = "TRANSMIT RATE: No Connection";
            SSID_text.color = Color.red;
            BSSID_text.color = Color.red;
            Singal_STR_text.color = Color.red;
            wifiAuthentication_text.color = Color.red;
            bestSecuirty_text.color = Color.red;
            receiverate_text.color = Color.red;
            transmitrate_text.color = Color.red;
        }
        else
        {
            SSID_text.text = "SSID: " + wifiSSID;
            BSSID_text.text = "BSSID: " + wifiBSSID;
            Singal_STR_text.text = "STRENGTH: " + wifiSignalStrength.ToString() + "dBm";
            wifiAuthentication_text.text = "CURRENT SECUIRTY: " + wifiAuthentication;
            bestSecuirty_text.text = "BEST SECUIRTY: " + bestSecuirty;
            receiverate_text.text = "RECEIVE RATE: " + receiverate.ToString() + " Mbps";
            transmitrate_text.text = "TRANSMIT RATE: " + transmitrate.ToString() + " Mbps";
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
            //put a if statment for each recieverate and transmit rate to change the color based on how good the rate is



        }


    }

}


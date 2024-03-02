using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using System.Net.NetworkInformation;
using UnityEngine.UI;

public class HiddenSSID_Scan : MonoBehaviour
{

    public Text PrintAllSSID;
    // private List<string> hiddenSSIDs = new List<string>();
    private List<string> allSSIDs = new List<string>();


    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            Permission.RequestUserPermission(Permission.FineLocation);
        }
        InvokeRepeating("BeginScanningShadow", 0, 3);
    }

    private void BeginScanningShadow()
    {
        StartCoroutine(ScanForHiddenSSIDs());
    }



    private IEnumerator ScanForHiddenSSIDs()
    {
        yield return new WaitForSeconds(1.0f);

        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");

        // Start Wi-Fi scan
        AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");
        int scanResultsCount = scanResults.Call<int>("size");

        for (int i = 0; i < scanResultsCount; i++)
        {
            AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
            string ssid = scanResult.Get<string>("SSID");

            // // Check if SSID is hidden
            // if (string.IsNullOrEmpty(ssid))
            // {
            //     // Add hidden SSID to the list
            //     hiddenSSIDs.Add("Hidden SSID Detected");
            // }


            // Add SSID to the list
            allSSIDs.Add(ssid);

        }

        RemoveDuplicates(allSSIDs);
        string finalText = "";
        // Display detected hidden SSIDs
        // foreach (string hiddenSSID in hiddenSSIDs)
        // {
        //     finalText += hiddenSSID + "\n";
        // }

        foreach (string ssid in allSSIDs)
        {
            finalText += ssid + "\n";
        }
        // Update UI text
        PrintAllSSID.text = finalText;
    }


    public void RemoveDuplicates(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for (int j = i + 1; j < list.Count; j++)
            {
                if (list[i] == list[j])
                {
                    list.RemoveAt(j);
                    j--;
                }
            }
        }
    }
}

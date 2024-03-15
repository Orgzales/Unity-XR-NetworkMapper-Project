using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using TMPro;

public class HiddenSSID_Scan : MonoBehaviour
{

    public Other_Spawner_Manager Other_Spawner_ManagerScript;

    public Transform CameraObject; //switch with OVR later
    public GameObject popupPrefab; //POp up screen prefab
    public GameObject parentObject; //MRTK scene
    public Text WhiteListText;
    public Text BlackListText;



    // private List<string> hiddenSSIDs = new List<string>();
    public List<string> allSSIDs = new List<string>();

    public List<string> WhiteSSIDs = new List<string>();
    public List<string> BlackSSIDs = new List<string>();
    // public bool NewShadowSSID;

    public string DescriptionTextPrefabName = ""; //name of object to change text

    // public int count_debug = 0;



    private string CurrentSSID;
    public bool popupClosed = false; //To stop more than one screen pop up instantanty  
    public bool NoActiveScanning = true; //To prevent Scan within a scan


    public Dictionary<string, int> SSIDSignal = new Dictionary<string, int>();

    // void Start()
    // {
    //     if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
    //     {
    //         Permission.RequestUserPermission(Permission.FineLocation);
    //     }
    //     // InvokeRepeating("BeginScanningShadow", 0, 3);
    //     BeginScanningShadow();

    // }

    public void BeginScanningShadow()
    {
        NoActiveScanning = false; //SCanning begins
        allSSIDs.Clear(); //for rescans
        WhiteListText.text = "Scanning...";
        BlackListText.text = "Scanning...";
        StartCoroutine(ScanForHiddenSSIDs());
    }

    public void BeginDEMOScanningShadow()
    {
        NoActiveScanning = false; //SCanning begins
        allSSIDs.Clear(); //for rescans
        WhiteListText.text = "Scanning...";
        BlackListText.text = "Scanning...";
        StartCoroutine(ScanForDemoSSIDS()); //windows testing
    }


    private IEnumerator ScanForHiddenSSIDs()
    {
        yield return new WaitForSeconds(1.0f);

        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
        wifiManager.Call<bool>("startScan"); //works for restarting scan
        AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");

        int scanResultsCount = scanResults.Call<int>("size");

        for (int i = 0; i < scanResultsCount; i++)
        {
            AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
            string ssid = scanResult.Get<string>("SSID");
            int signalStrength = scanResult.Get<int>("level"); //works

            string SSIDInfo = ssid + " (" + signalStrength.ToString() + " dBm)";
            // Debug.Log("SSID: " + ssidWithStrength);

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                SSIDSignal.Add(ssid, signalStrength);
                ShowPopup(SSIDInfo);
                // NewShadowSSID = true;
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }

        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab
        NoActiveScanning = true; //Scanning ends here when prefab is created

    }

    private IEnumerator ScanForDemoSSIDS() //windows testing
    {
        yield return new WaitForSeconds(1.0f);

        string[] scanResultsCount = new string[] { "Resnet", "eduroam", "SUPER SAFE WIFI",
        "ATT", "Xfinity", "Hidden SSID Detected", "Very Obvious Dangerious Network"};


        for (int i = 0; i < scanResultsCount.Length; i++)
        {

            // // Check if SSID is hidden
            // if (string.IsNullOrEmpty(ssid))
            // {
            //     // Add hidden SSID to the list
            //     hiddenSSIDs.Add("Hidden SSID Detected"); //Add this code later if there is a need to display hidden SSIDs
            // }

            string ssid = scanResultsCount[i];
            int signalStrength = Random.Range(-50, -90);
            string SSIDInfo = ssid + " (" + signalStrength.ToString() + " dBm)";
            // Debug.Log("SSID: " + ssid);

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                SSIDSignal[ssid] = signalStrength;
                Debug.Log("SSID: " + ssid + " Signal: " + signalStrength.ToString() + " dBm");
                Debug.Log(SSIDSignal[ssid]);
                ShowPopup(SSIDInfo);
                // NewShadowSSID = true;
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }
        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab
        NoActiveScanning = true; //Scanning ends here when prefab is created
        SetWhiteText();
        SetBlackText();

    }

    public void ShowPopup(string NetworkSSID)
    {
        Vector3 Straight = CameraObject.forward; // Get the forward direction of VR
        Straight.y = 0; // To prevent camera tilt affecting roatation
        Straight.Normalize(); // Normalize vector scale

        Quaternion Rotation = Quaternion.LookRotation(Straight); // Calculate POPUP rotation
        Vector3 POS = CameraObject.position + Straight * 0.5f; // To popup in front
        GameObject newObjectScreen = Instantiate(popupPrefab, POS, Rotation); // Create Popup
        newObjectScreen.transform.SetParent(parentObject.transform); //Set Popup within MRTK scene
        newObjectScreen.SetActive(true);


        string DescriptionText = "SSID Detected, Do you trust this Network? : " + NetworkSSID;

        SetTextRecursively(newObjectScreen.transform, DescriptionText); // Set the text of the popup

    }

    void SetTextRecursively(Transform parent, string text) //setting the text in the popupwindow
    {
        // Get Parent -> Parent -> Textobject
        Transform textObjectTransform = parent.Find(DescriptionTextPrefabName);

        if (textObjectTransform != null)
        {
            // Access the TextMeshPro Text and changes it 
            TextMeshProUGUI textComponent = textObjectTransform.GetComponent<TextMeshProUGUI>();
            textComponent.text = text;
        }
        else
        {
            // Keep looking for the text for later code for BSSID
            foreach (Transform child in parent)
            {
                SetTextRecursively(child, text);
            }
        }

    }

    public void AddWhiteList()
    {

        if (!WhiteSSIDs.Contains(CurrentSSID))
        {
            WhiteSSIDs.Add(CurrentSSID);
        }
        if (BlackSSIDs.Contains(CurrentSSID))
        {
            BlackSSIDs.Remove(CurrentSSID);
        }

    }

    public void SetWhiteText()
    {
        string finalText = "";
        foreach (string ssid in WhiteSSIDs)
        {
            int dBm = SSIDSignal[ssid];
            switch (CheckdBm(dBm))
            {
                case 1:
                    finalText += "<color=green>" + dBm.ToString() + "dBm|" + "</color>" + ssid + "\n";
                    break;
                case 2:
                    finalText += "<color=#FFFF00>" + dBm.ToString() + "dBm|" + "</color>" + ssid + "\n";
                    break;
                case 3:
                    finalText += "<color=red>" + dBm.ToString() + "dBm|" + "</color>" + ssid + "\n";
                    break;
                default:
                    finalText += dBm.ToString() + "dBm|" + ssid + "\n";
                    break;
            }
        }
        WhiteListText.text = finalText;
    }

    public void AddBlackList()
    {
        if (!BlackSSIDs.Contains(CurrentSSID))
        {
            BlackSSIDs.Add(CurrentSSID);
        }
        if (WhiteSSIDs.Contains(CurrentSSID))
        {
            WhiteSSIDs.Remove(CurrentSSID);
        }

    }

    public void SetBlackText()
    {
        string finalText = "";
        foreach (string ssid in BlackSSIDs)
        {
            int dBm = SSIDSignal[ssid];
            switch (CheckdBm(dBm))
            {
                case 1:
                    finalText += "<color=green>" + dBm.ToString() + "dBm|" + "</color>" + ssid + "\n";
                    break;
                case 2:
                    finalText += "<color=#FFFF00>" + dBm.ToString() + "dBm|" + "</color>" + ssid + "\n";
                    break;
                case 3:
                    finalText += "<color=red>" + dBm.ToString() + "dBm|" + "</color>" + ssid + "\n";
                    break;
                default:
                    finalText += dBm.ToString() + "dBm|" + ssid + "\n";
                    break;
            }
        }
        BlackListText.text = finalText;
    }

    public int CheckdBm(int wifiSignalStrength)
    {
        if (wifiSignalStrength >= -67) //-67 = Amazing in dbm
        {
            return 1;
        }
        else if (wifiSignalStrength >= -79 && wifiSignalStrength < -70) // -70 = okay
        {
            return 2;
        }
        else if (wifiSignalStrength < -80) // -80 or lower is bad
        {
            return 3;
        }
        else
        {
            return 3;
        }
    }

}

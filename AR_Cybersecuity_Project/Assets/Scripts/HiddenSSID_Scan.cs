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

    private bool ScanningDemo = false;

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
        stopScanning();
        NoActiveScanning = false; //SCanning begins
        allSSIDs.Clear(); //for rescans
        WhiteListText.text = "Scanning...";
        BlackListText.text = "Scanning...";

        ScanningDemo = false;
        StartCoroutine(ScanForHiddenSSIDs());

    }

    public void BeginDEMOScanningShadow()
    {
        stopScanning();
        NoActiveScanning = false; //SCanning begins
        allSSIDs.Clear(); //for rescans
        WhiteListText.text = "Scanning...";
        BlackListText.text = "Scanning...";

        ScanningDemo = true;
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
        int hiddenFound = 0;

        for (int i = 0; i < scanResultsCount; i++)
        {
            AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
            string ssid = scanResult.Get<string>("SSID");
            int signalStrength = scanResult.Get<int>("level"); //works

            if (string.IsNullOrEmpty(ssid)) //For Rouge or unnamed ssid networks 
            {
                ssid = "Hidden/NoName SSID Found #" + hiddenFound.ToString();
                hiddenFound++;
            }

            string SSIDInfo = ssid + " (" + signalStrength.ToString() + " dBm)";

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                SSIDSignal[ssid] = signalStrength;
                ShowPopup(SSIDInfo);
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }

        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab
        NoActiveScanning = true; //Scanning ends here when prefab is created
        SetWhiteText();
        SetBlackText();
        InvokeRepeating("UpdateSignals", 2.0f, 6.0f);
    }

    private IEnumerator ScanForDemoSSIDS() //windows testing
    {
        yield return new WaitForSeconds(1.0f);

        string[] scanResultsCount = new string[] { "Resnet", "eduroam", "SUPER SAFE WIFI",
        "ATT", "Xfinity", "Hidden SSID Detected", "Very Obvious Dangerious Network"};


        for (int i = 0; i < scanResultsCount.Length; i++)
        {
            string ssid = scanResultsCount[i];
            int signalStrength = Random.Range(-50, -90);
            string SSIDInfo = ssid + " (" + signalStrength.ToString() + " dBm)";

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                SSIDSignal[ssid] = signalStrength;
                ShowPopup(SSIDInfo);
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }
        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab
        NoActiveScanning = true; //Scanning ends here when prefab is created
        SetWhiteText();
        SetBlackText();
        InvokeRepeating("UpdateSignals", 2.0f, 6.0f);
    }

    public void UpdateSignals()
    {
        if (ScanningDemo == false)
        {
            AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
            wifiManager.Call<bool>("startScan"); //works for restarting scan
            AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");

            int scanResultsCount = scanResults.Call<int>("size");
            int Num_newSSID = 0;

            List<string> allNewScans = new List<string>();

            for (int i = 0; i < scanResultsCount; i++)
            {
                AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
                string ssid = scanResult.Get<string>("SSID");
                int signalStrength = scanResult.Get<int>("level"); //works

                if (allSSIDs.Contains(ssid)) //updating signal strength
                {
                    SSIDSignal[ssid] = signalStrength;
                }
                else //new ssid found
                {
                    Num_newSSID++;
                }

                allNewScans.Add(ssid);
            }

            for (int i = 0; i < allSSIDs.Count; i++) //if ssid is missing = no signal 
            {
                if (!allNewScans.Contains(allSSIDs[i]))
                {
                    SSIDSignal[allSSIDs[i]] = -999;
                }
            }
            if (Num_newSSID > 0) //Notifying user that new network was found 
            {
                WhiteSSIDs.Add("+ NEW SSID DETECTED +");
                BlackSSIDs.Add("+ NEW SSID DETECTED +");
                SSIDSignal["+ NEW SSID DETECTED +"] = Num_newSSID;
            }

            SetWhiteText();
            SetBlackText();
        }
        else //DEMO MODE
        {

            string[] scanResultsCount = new string[] { "Resnet", "eduroam", "SUPER SAFE WIFI",
        "ATT", "Xfinity", "Hidden SSID Detected", "Very Obvious Dangerious Network"};

            int Num_newSSID = 0;
            int Rand_SSID = Random.Range(0, 2);
            if (Rand_SSID == 0)
            {
                //mimic new SSID is detected
                string newSSID = "Verizon";
                scanResultsCount[Random.Range(0, 6)] = newSSID;
            }
            for (int i = 0; i < scanResultsCount.Length; i++) //updating signal strength
            {

                string ssid = scanResultsCount[i];
                int signalStrength = Random.Range(-50, -90);

                if (allSSIDs.Contains(ssid))
                {
                    SSIDSignal[ssid] = signalStrength;
                }
                else
                {
                    Num_newSSID++;
                }

            }

            for (int i = 0; i < allSSIDs.Count; i++) //if ssid is missing = no signal
            {
                if (System.Array.IndexOf(scanResultsCount, allSSIDs[i]) == -1)
                {
                    SSIDSignal[allSSIDs[i]] = -999;
                }
            }

            if (Num_newSSID > 0) //Notifying user that new network was found
            {
                WhiteSSIDs.Add("+ NEW SSID DETECTED +");
                BlackSSIDs.Add("+ NEW SSID DETECTED +");
                SSIDSignal["+ NEW SSID DETECTED +"] = Num_newSSID;
            }

            SetWhiteText();
            SetBlackText();
        }

    }

    void stopScanning()
    {
        CancelInvoke("UpdateSignals");
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
                    finalText += "<color=green>" + dBm.ToString() + "dBm|" + ssid + "</color>" + "\n";
                    break;
                case 2:
                    finalText += "<color=#FFFF00>" + dBm.ToString() + "dBm|" + ssid + "</color>" + "\n";
                    break;
                case 3:
                    finalText += "<color=red>" + dBm.ToString() + "dBm|" + ssid + "</color>" + "\n";
                    break;
                case 4:
                    finalText += "<color=grey>" + "No Signal|" + ssid + "</color>" + "\n";
                    break;
                case 5:
                    finalText += "<color=blue>" + ssid + "</color>" + "\n";
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
                    finalText += "<color=green>" + dBm.ToString() + "dBm|" + ssid + "</color>" + "\n";
                    break;
                case 2:
                    finalText += "<color=#FFFF00>" + dBm.ToString() + "dBm|" + ssid + "</color>" + "\n";
                    break;
                case 3:
                    finalText += "<color=red>" + dBm.ToString() + "dBm|" + ssid + "</color>" + "\n";
                    break;
                case 4:
                    finalText += "<color=grey>" + "No Signal|" + ssid + "</color>" + "\n";
                    break;
                case 5:
                    finalText += "<color=blue>" + ssid + "</color>" + "\n";
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


        if (wifiSignalStrength == -999) // -999 = no signal
        {
            return 4;
        }
        else if (wifiSignalStrength >= 1) // New signal found
        {
            return 5;
        }
        else if (wifiSignalStrength >= -67) //-67 = Amazing in dbm
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

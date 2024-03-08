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
    public bool popupClosed = false; //debugging 

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
        // StopCoroutine(ScanForHiddenSSIDs());
        // StartCoroutine(ScanForHiddenSSIDs());
        StopCoroutine(ScanOnWindowsSSIDs()); //windows testing
        StartCoroutine(ScanOnWindowsSSIDs()); //windows testing
    }



    private IEnumerator ScanForHiddenSSIDs()
    {
        yield return new WaitForSeconds(1.0f);

        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
        AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");

        allSSIDs.Clear(); //for rescans
        int scanResultsCount = scanResults.Call<int>("size");

        for (int i = 0; i < scanResultsCount; i++)
        {
            AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
            string ssid = scanResult.Get<string>("SSID");

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                ShowPopup(ssid);
                // NewShadowSSID = true;
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }

        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab


    }

    private IEnumerator ScanOnWindowsSSIDs() //windows testing
    {
        yield return new WaitForSeconds(1.0f);

        allSSIDs.Clear();
        string[] scanResultsCount = new string[] { "Resnet", "eduroam",
        "ATT", "Xfinity", "Hidden SSID Detected" };

        for (int i = 0; i < scanResultsCount.Length; i++)
        {

            // // Check if SSID is hidden
            // if (string.IsNullOrEmpty(ssid))
            // {
            //     // Add hidden SSID to the list
            //     hiddenSSIDs.Add("Hidden SSID Detected"); //Add this code later if there is a need to display hidden SSIDs
            // }
            string ssid = scanResultsCount[i];
            Debug.Log("SSID: " + ssid);

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                ShowPopup(ssid);
                // NewShadowSSID = true;
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }
        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab


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
        string finalText = "";
        foreach (string ssid in WhiteSSIDs)
        {
            finalText += ssid + "\n";
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
        string finalText = "";
        foreach (string ssid in BlackSSIDs)
        {
            finalText += ssid + "\n";
        }
        BlackListText.text = finalText;
    }

}

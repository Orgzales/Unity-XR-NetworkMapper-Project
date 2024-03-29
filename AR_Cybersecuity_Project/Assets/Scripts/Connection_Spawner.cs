using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Connection_Spawner : MonoBehaviour
{
    public Checking_Internet Wifi_script; //Drag script internetchecking into here to acess strings
    public Button_Manager button_script; // Values for overwrite mode and demo mode

    // public GameObject CurrentAnchorParentObject; //Parent
    public GameObject CurrentAnchorParentObject; //Where the WifiScans will be placed

    public GameObject prefabToInstantiate; //Child
    public GameObject bssidprefabToInstantiate; //Child
    public float spawnHeight = 1.75f; //Distance from Camera
    public float checkRadius = 0.45f; //Distance between Points of access
    public string WifiPrefab_Details_Text = ""; //name of objects display

    public string BSSIDPrefab_Details_Text = ""; //name of bssid display
    public int dBm_value;//based on dBm Value
    public string Secuirty_type_value; //security for change_manager script


    // public string debugssid; //windows testing
    // public string debugbssid; //windows testing
    private string previousNetworkName; //namint noconnection prefabs

    private string currentBSSID; //for bssid spawning
    private string previousBSSID;//for bssid spawning

    private bool Overwrite_Mode;
    private bool Demo_Mode;

    private int Demo_Counter = 0; //For spawning bssid prefabs during demo mode

    void Start()
    {
        InvokeRepeating("InstantiateChildObject", 3, 3);

    }

    private void InstantiateChildObject()
    {

        if (button_script.Overwrite_Mode == true)
        {
            Overwrite_Mode = true;
        }
        else
        {
            Overwrite_Mode = false;
        }

        if (CanInstantiateHere() || Overwrite_Mode) //Checking if Object of prefab is near.
        {
            Demo_Mode = button_script.Demo_Mode; //Demo mode varaible for change_manager
            dBm_value = Wifi_script.wifiSignalStrength; //based on dBm Value for change_manager
            Secuirty_type_value = Wifi_script.wifiAuthentication; //based on security type for change_manager
            string Wifi_ScreenDisplay = ""; //Change later with wifi info
            int prefab_array = 0; //0 = good | 1 = ok | 2 = Bad

            // Check if the parent object and prefab are set
            if (CurrentAnchorParentObject != null && prefabToInstantiate != null)
            {
                //instantate here to user
                Vector3 cameraPosition = Camera.main.transform.position;
                Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

                bool BSSID_Condition = (string.IsNullOrEmpty(Wifi_script.wifiSSID) || Wifi_script.wifiSSID.Equals("<unknown ssid>"));
                // bool BSSID_Condition = true; //windows testing
                bool create_BSSIDPillar = false;


                //check if new bssid
                if (currentBSSID == null)
                {// if bssid has not been set to new network
                    currentBSSID = Wifi_script.wifiBSSID;
                    // currentBSSID = debugbssid; //windows testing
                }
                else if (currentBSSID != Wifi_script.wifiBSSID && previousNetworkName == Wifi_script.wifiSSID && !BSSID_Condition)
                { //if bssid is different but ssid is the same but not no connection then create pillar
                    previousBSSID = currentBSSID;
                    currentBSSID = Wifi_script.wifiBSSID;
                    // currentBSSID = debugbssid; //windows testing
                    create_BSSIDPillar = true;
                }
                else
                {
                    currentBSSID = Wifi_script.wifiBSSID;
                    // currentBSSID = debugbssid; //windows testing
                }

                //if overwrite mode is on then delete all wifi prefabs with same name within 5m radius
                if (Overwrite_Mode)
                {
                    OverWriteRadius(previousNetworkName);
                }
                //if demo mode is on then randomize dBm and security type
                if (Demo_Mode)
                {
                    Demo_Counter++;
                    dBm_value = Random.Range(-50, -90);
                    int random_secuirty = Random.Range(0, 4);
                    switch (random_secuirty)
                    {
                        case 0:
                            Secuirty_type_value = "WPA/WPA2";
                            break;
                        case 1:
                            Secuirty_type_value = "Orion's Awesome Secuirty";
                            break; //mimic unknown secuirty
                        case 2:
                            Secuirty_type_value = "WEP";
                            break;
                        case 3:
                            Secuirty_type_value = "OPEN";
                            break;
                    }
                }

                //Instantiate prefab connection info
                GameObject newObject;
                if (create_BSSIDPillar || Demo_Counter >= 8)
                {//create bssid prefab instead of ssid prefab
                    Demo_Counter = 0; //reset demo counter
                    create_BSSIDPillar = true; // for demon mode
                    newObject = Instantiate(bssidprefabToInstantiate, spawnPosition, Quaternion.identity);
                }
                else
                {//If no new bssid then create ssid prefab
                    newObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                }
                newObject.transform.SetParent(CurrentAnchorParentObject.transform);

                // setting names of objects for database_manager
                // if (Wifi_script.wifiSSID == "No Networks in Area") windows testing
                if (string.IsNullOrEmpty(Wifi_script.wifiSSID) || Wifi_script.wifiSSID.Equals("<unknown ssid>"))
                {
                    newObject.name = "No Networks in Area:" + previousNetworkName;
                }
                else
                {
                    newObject.name = Wifi_script.wifiSSID.ToString();
                    previousNetworkName = Wifi_script.wifiSSID.ToString();
                }

                //This displays on the AR Screen
                Wifi_ScreenDisplay = "SSID: " + Wifi_script.wifiSSID +
                "\nBSSID: " + Wifi_script.wifiBSSID +
                "\ndBm: " + dBm_value.ToString() +
                "\nCURR AUTH: " + Secuirty_type_value +
                "\nBEST AUTH: " + Wifi_script.bestSecuirty +
                "\nDATA RECEIVE & TRANSMIT RATE: " + Wifi_script.DataSpeedRate.ToString() + " Mbps" +
                "\nNETWORK FREQUENCY: " + Wifi_script.Freq_Network.ToString() + " MHz";

                SetTextRecursively(newObject.transform, Wifi_ScreenDisplay);

                if (create_BSSIDPillar)
                {//if new bssid then display previous and current bssid info aswell
                    string bssid_Display = Wifi_script.wifiSSID + "\nPrevious BSSID: " + previousBSSID +
                    "\nCurrent BSSID: " + currentBSSID;
                    SetBSSIDRecursively(newObject.transform, bssid_Display);
                }

            }
            else
            {
                Debug.LogError("Parent object or prefab not set!");
            }

        }
    }

    void SetTextRecursively(Transform parent, string text)
    {
        // Get Parent -> Parent -> Textobject
        Transform textObjectTransform = parent.Find(WifiPrefab_Details_Text);

        if (textObjectTransform != null)
        {
            // Access the TextMeshPro Text and changes it 
            TextMeshPro textComponent = textObjectTransform.GetComponent<TextMeshPro>();
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

    public void OverWriteRadius(string prefab_name)
    {
        GameObject[] all_wifi_prefabs = FindObjectsOfType<GameObject>();

        foreach (GameObject wifi_prefab in all_wifi_prefabs)
        {
            if (wifi_prefab.name == prefab_name || wifi_prefab.name == "No Networks in Area:" + prefab_name)
            {
                // Check if the object is within the deletion radius of the VR/AR headset
                if (Vector3.Distance(wifi_prefab.transform.position, transform.position) <= 2.0f)
                {
                    Destroy(wifi_prefab);
                }
            }
        }
    }


    void SetBSSIDRecursively(Transform parent, string text)
    {
        // Get Parent -> Parent -> Textobject
        Transform textObjectTransform = parent.Find(BSSIDPrefab_Details_Text);

        if (textObjectTransform != null)
        {
            // Access the TextMeshPro Text and changes it 
            TextMeshPro textComponent = textObjectTransform.GetComponent<TextMeshPro>();
            textComponent.text = text;
        }
        else
        {
            foreach (Transform child in parent)
            {
                SetBSSIDRecursively(child, text);
            }
        }

    }

    private bool CanInstantiateHere()
    {
        // Get the position of the VR/AR Headset
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 checkPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

        // Check for colliders in the specified radius
        Collider[] colliders = Physics.OverlapSphere(checkPosition, checkRadius);

        //if the length of the colliders isn't close enough, an object can be set here
        if (colliders.Length == 0)
        {
            return true;
        }
        else
        {
            // Debug.Log("ANother object too Close!");
            return false;
        }
    }
}


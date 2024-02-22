using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Connection_Spawner : MonoBehaviour
{
    public Checking_Internet Wifi_script; //Drag script internetchecking into here to acess strings

    public GameObject parentObject; //Parent
    public GameObject prefabToInstantiate; //Child
    public GameObject bssidprefabToInstantiate; //Child
    public float spawnHeight = 1.75f; //Distance from Camera
    public float checkRadius = 0.45f; //Distance between Points of access
    public string WifiPrefab_Details_Text = ""; //name of objects display

    public string BSSIDPrefab_Details_Text = ""; //name of bssid display
    public int dBm_value;//based on dBm Value
    public string Secuirty_type_value; //based on security type


    public string debugssid;
    private string previousNetworkName;

    private string currentBSSID;
    private string previousBSSID;

    void Start()
    {
        InvokeRepeating("InstantiateChildObject", 3, 3);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateChildObject()
    {
        if (CanInstantiateHere()) //Checking if Object of prefab is near.
        {
            dBm_value = Wifi_script.wifiSignalStrength; //based on dBm Value
            Secuirty_type_value = Wifi_script.wifiAuthentication; //based on security type
            string text_Display = ""; //Change later with wifi info
            int prefab_array = 0; //0 = good | 1 = ok | 2 = Bad


            // Check if the parent object and prefab are set
            if (parentObject != null && prefabToInstantiate != null)
            {
                Vector3 cameraPosition = Camera.main.transform.position;

                //instantate here to user
                Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

                bool BSSID_Condition = (string.IsNullOrEmpty(Wifi_script.wifiSSID) || Wifi_script.wifiSSID.Equals("<unknown ssid>"));
                bool create_BSSIDPillar = false;


                //check if new bssid
                if (currentBSSID == null) // if bssid has not been set to new network
                {
                    currentBSSID = Wifi_script.wifiBSSID;
                }
                else if (currentBSSID != Wifi_script.wifiBSSID && previousNetworkName == Wifi_script.wifiSSID && BSSID_Condition)
                { //if bssid is different but ssid is the same but not no connection then create pillar
                    previousBSSID = currentBSSID;
                    currentBSSID = Wifi_script.wifiBSSID;
                    create_BSSIDPillar = true;
                }
                else
                {
                    currentBSSID = Wifi_script.wifiBSSID;
                }


                //instatnte the object under parent of MRTK
                // GameObject newObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                GameObject newObject;
                //create bssid prefab instead of ssid prefab
                if (create_BSSIDPillar)
                {
                    newObject = Instantiate(bssidprefabToInstantiate, spawnPosition, Quaternion.identity);
                }
                else
                {
                    newObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                }
                newObject.transform.SetParent(parentObject.transform);

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


                text_Display = "SSID: " + Wifi_script.wifiSSID + "\nBSSID: " + Wifi_script.wifiBSSID +
                "\ndBm: " + dBm_value.ToString() + "\nAUTH: " + Wifi_script.wifiAuthentication;

                SetTextRecursively(newObject.transform, text_Display);

                if (create_BSSIDPillar)
                {
                    string bssid_Display = Wifi_script.wifiSSID + "\nPrevious BSSID: " + previousBSSID +
                    " --> Current BSSID: " + currentBSSID;
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
        Transform textObjectTransform = parent.Find(BSSIDPrefab_Details_Text);

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

    void SetBSSIDRecursively(Transform parent, string text)
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

    private bool CanInstantiateHere()
    {
        // Get the position of the main camera
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 checkPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

        // Check for colliders in the specified radius
        Collider[] colliders = Physics.OverlapSphere(checkPosition, checkRadius);

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


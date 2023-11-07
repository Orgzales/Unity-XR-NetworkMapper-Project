using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Connection_Spawner : MonoBehaviour
{
    public Checking_Internet Wifi_script; //Drag script internetchecking into here to acess strings

    public GameObject parentObject; //Parent
    public GameObject[] prefabToInstantiate; //Child
    public float spawnHeight = 0.45f; //Distance from Camera
    public float checkRadius = 0.5f; //Distance between Points of access
    public string Prefab_Text_Name = ""; //name of objects display


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateChildObject", 0, 6);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateChildObject()
    {
        if (CanInstantiateHere()) //Checking if Object of prefab is near.
        {
            int dBm_value = Wifi_script.wifiSignalStrength; //based on dBm Value
            string text_Display = ""; //Change later with wifi info
            int prefab_array = 0; //0 = good | 1 = ok | 2 = Bad

            if (dBm_value >= -67) //-67 = Amazing in dbm
            {
                prefab_array = 0;
            }
            else if (dBm_value >= -79 && dBm_value < -70) // -70 = okay
            {
                prefab_array = 1;
            }
            else if (dBm_value < -80) // -80 or lower is bad
            {
                prefab_array = 2;
            }
            if (dBm_value == 0) //windows testing delete for quest 2
            {
                prefab_array = 2;
            }

            // Check if the parent object and prefab are set
            if (parentObject != null && prefabToInstantiate != null)
            {
                Vector3 cameraPosition = Camera.main.transform.position;

                // Calculate the position to spawn the object
                Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

                // Instantiate the prefab and set the parent to the parentObject
                GameObject newObject = Instantiate(prefabToInstantiate[prefab_array], spawnPosition, Quaternion.identity);
                newObject.transform.SetParent(parentObject.transform);

                text_Display = "SSID: " + Wifi_script.wifiSSID + "\nBSSID: " + Wifi_script.wifiBSSID +
                "\ndBm: " + dBm_value.ToString() + "\nAUTH: " + Wifi_script.wifiAuthentication.ToString();

                SetTextRecursively(newObject.transform, text_Display);

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
        Transform textObjectTransform = parent.Find(Prefab_Text_Name);

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
            Debug.Log("ANother object too Close!");
            return false;
        }
    }
}


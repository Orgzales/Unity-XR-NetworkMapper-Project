using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    public Camera mainCamera; // Reference to the VR Headset
    public Checking_Internet Wifi_script; // Reference to the SSID Varaible
    public HiddenSSID_Scan HiddenSSID_ScanScript; // Reference to the HiddenSSID_Scan Script

    public Other_Spawner_Manager Other_Spawner_ManagerScript; // Reference to the Other_Spawner_Manager Script

    public bool Demo_Mode = false; //for Connection_Spawner
    public bool Overwrite_Mode = false;//for Connection_Spawner


    public void ToggleDemoButtonPress()
    {
        if (Demo_Mode == true)
            Demo_Mode = false;
        else
            Demo_Mode = true;
    }

    public void ToggleOverwriteButtonPress()
    {
        if (Overwrite_Mode == true)
            Overwrite_Mode = false;
        else
            Overwrite_Mode = true;
    }

    public void DeleteRadiusButtonPress()
    {
        string prefab_name = Wifi_script.wifiSSID;
        GameObject[] all_wifi_prefabs = FindObjectsOfType<GameObject>();

        foreach (GameObject wifi_prefab in all_wifi_prefabs)
        {
            if (wifi_prefab.name == prefab_name || wifi_prefab.name == "No Networks in Area:" + prefab_name)
            {
                // Check if the object is within the deletion radius of the VR headset
                if (Vector3.Distance(wifi_prefab.transform.position, mainCamera.transform.position) <= 2.0f)
                {
                    Destroy(wifi_prefab);
                }
            }
        }
    }

    public void ReScanShadowITButtonPress()
    {
        HiddenSSID_ScanScript.BeginScanningShadow();
        // Other_Spawner_ManagerScript.SpawnShadowITPrefab();
    }



}

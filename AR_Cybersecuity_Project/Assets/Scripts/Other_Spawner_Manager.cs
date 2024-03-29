using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Other_Spawner_Manager : MonoBehaviour
{

    public float spawnHeight = 1.75f; //Distance from Camera
    public GameObject parentObject; //Change to current anchor later

    public GameObject AnchorParentObject; //Where the Anchors will be placed

    public HiddenSSID_Scan HiddenSSID_ScanScript; // Reference to the HiddenSSID_Scan Script
    public Button_Manager Button_ManagerScript; // Reference to the Button_Manager Script
    public DataBase_Manager DataBase_ManagerScript; // Reference to the DataBase_Manager Script
    public Connection_Spawner Connection_SpawnerScript; // Reference to the Connection_Spawner Script

    public GameObject ShadowITPrefab;
    public GameObject AnchorPrefab;
    public string WhiteList_Details_Text = "";
    public string BlackList_Details_Text = "";

    private int AnchorCount = 0;

    public void SpawnShadowITPrefab()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

        GameObject newObject = Instantiate(ShadowITPrefab, spawnPosition, Quaternion.identity);
        newObject.transform.SetParent(parentObject.transform);

        string WhiteListData = "~ White List ~\n";
        string BlackListData = "~ Black List ~\n";
        if (Button_ManagerScript.Demo_Mode)
        {
            newObject.name = "ShadowITPrefabDemo";
            WhiteListData = "~ DEMO White List ~\n";
            BlackListData = "~ DEMO Black List ~\n";
        }
        else
        {
            newObject.name = "ShadowITPrefab";
        }

        foreach (string ssid in HiddenSSID_ScanScript.WhiteSSIDs)
        {
            WhiteListData += "   " + ssid + ":" + HiddenSSID_ScanScript.SSIDSignal[ssid] + "\n";
        }
        foreach (string ssid in HiddenSSID_ScanScript.BlackSSIDs)
        {
            BlackListData += "   " + ssid + ":" + HiddenSSID_ScanScript.SSIDSignal[ssid] + "\n";
        }


        // Debug.Log("SpawnShadowITPrefab");
        SetTextRecursively(newObject.transform, WhiteListData, WhiteList_Details_Text);
        SetTextRecursively(newObject.transform, BlackListData, BlackList_Details_Text);
    }


    public void SpawnAnchorPrefab()
    {

        //Setting all other anchors text to false active to let user know which anchor is currently active
        foreach (Transform child in AnchorParentObject.transform)
        {
            SetTextRecursively(child, "Active: False", "ActiveText");
        }

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - 0.5f, cameraPosition.z);

        GameObject newObject = Instantiate(AnchorPrefab, spawnPosition, Quaternion.identity);
        newObject.transform.SetParent(AnchorParentObject.transform);
        newObject.name = "AnchorPrefab" + AnchorCount;
        AnchorCount++;

        DataBase_ManagerScript.cloneParentObjects.Add(newObject.transform.Find("Followers").gameObject);
        parentObject = newObject.transform.Find("Followers").gameObject;
        Connection_SpawnerScript.CurrentAnchorParentObject = parentObject;
        SetTextRecursively(newObject.transform, "Anchor: #" + AnchorCount, "AnchorText");

    }

    void SetTextRecursively(Transform parent, string text, string Parent_text)
    {
        // Get Parent -> Parent -> Textobject
        Transform textObjectTransform = parent.Find(Parent_text);

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
                SetTextRecursively(child, text, Parent_text);
            }
        }

    }


}

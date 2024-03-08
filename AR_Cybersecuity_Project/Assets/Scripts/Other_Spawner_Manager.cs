using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Other_Spawner_Manager : MonoBehaviour
{

    public float spawnHeight = 1.75f; //Distance from Camera
    public GameObject parentObject; //MRTK scene


    public HiddenSSID_Scan HiddenSSID_ScanScript; // Reference to the HiddenSSID_Scan Script
    public GameObject ShadowITPrefab;
    public string ShadowPrefab_Details_Text = "";

    public void SpawnShadowITPrefab()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

        GameObject newObject = Instantiate(ShadowITPrefab, spawnPosition, Quaternion.identity);
        newObject.transform.SetParent(parentObject.transform);
        newObject.name = "ShadowITPrefab";

        string ShadowData = "White List:\n";
        foreach (string ssid in HiddenSSID_ScanScript.WhiteSSIDs)
        {
            ShadowData += "   " + ssid + "\n";
        }
        ShadowData += "\nBlack List:\n";
        foreach (string ssid in HiddenSSID_ScanScript.BlackSSIDs)
        {
            ShadowData += "   " + ssid + "\n";
        }

        // Debug.Log("SpawnShadowITPrefab");
        SetTextRecursively(newObject.transform, ShadowData);
    }

    public void SpawnInfoAnchorPrefab()
    {
        // Debug.Log("SpawnInfoAnchorPrefab");
        // GameObject infoAnchorPrefab = Instantiate(infoAnchorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void SpawnAnchorPrefab()
    {
        // Debug.Log("SpawnAnchorPrefab");
        // GameObject anchorPrefab = Instantiate(anchorPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    void SetTextRecursively(Transform parent, string text)
    {
        // Get Parent -> Parent -> Textobject
        Transform textObjectTransform = parent.Find(ShadowPrefab_Details_Text); //change later to a variable

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

}

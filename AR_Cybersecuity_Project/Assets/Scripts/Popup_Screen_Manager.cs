using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Popup_Screen_Manager : MonoBehaviour
{
    // public GameObject popupPrefab;
    // public float CountDown = 10f; // windoes testingq
    // private float timer = 0f;  //windows testing
    // private bool POPUP = false;

    // public string NetworkSSID = "Resnet";
    // public string DescriptionTextPrefabName = "Description"; //name of object to change text
    // public GameObject parentObject;

    // void Update()
    // {

    // }

    // void Start()
    // {
    //     ShowPopup();
    // }
    // public void ShowPopup()
    // {
    //     Transform CameraPos = transform; //camera's XYZ position
    //     Vector3 Straight = CameraPos.forward; // Get the forward direction of VR
    //     Straight.y = 0; // To prevent camera tilt affecting roatation
    //     Straight.Normalize(); // Normalize vector scale

    //     Quaternion Rotation = Quaternion.LookRotation(Straight); // Calculate POPUP rotation
    //     Vector3 POS = CameraPos.position + Straight * 0.5f; // To popup in front
    //     GameObject newObject = Instantiate(popupPrefab, POS, Rotation); // Create Popup
    //     POPUP = true;
    //     newObject.transform.SetParent(parentObject.transform);
    //     string DescriptionText = "SSID Detected, Do you trust this Network? : " + NetworkSSID;

    //     SetTextRecursively(newObject.transform, DescriptionText); // Set the text of the popup

    // }

    // void SetTextRecursively(Transform parent, string text)
    // {
    //     // Get Parent -> Parent -> Textobject
    //     Transform textObjectTransform = parent.Find(DescriptionTextPrefabName);


    //     if (textObjectTransform != null)
    //     {
    //         // Access the TextMeshPro Text and changes it 
    //         TextMeshProUGUI textComponent = textObjectTransform.GetComponent<TextMeshProUGUI>();
    //         textComponent.text = text;
    //     }
    //     else
    //     {
    //         // Keep looking for the text for later code for BSSID
    //         foreach (Transform child in parent)
    //         {
    //             SetTextRecursively(child, text);
    //         }
    //     }

    // }
}

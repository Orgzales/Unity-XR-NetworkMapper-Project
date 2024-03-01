using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Screen_Manager : MonoBehaviour
{
    public GameObject popupPrefab;
    public float CountDown = 10f; // windoes testingq
    private float timer = 0f;  //windows testing
    private bool POPUP = false;

    void Update()
    {
        if (!POPUP)
        {
            timer += Time.deltaTime;
            if (timer >= CountDown)
            {
                ShowPopup();
            }
        }
    }

    public void ShowPopup()
    {
        Transform CameraPos = transform; //camera's XYZ position
        Vector3 Straight = CameraPos.forward; // Get the forward direction of VR
        Straight.y = 0; // To prevent camera tilt affecting roatation
        Straight.Normalize(); // Normalize vector scale

        Quaternion Rotation = Quaternion.LookRotation(Straight); // Calculate POPUP rotation
        Vector3 POS = CameraPos.position + Straight * 0.5f; // To popup in front
        Instantiate(popupPrefab, POS, Rotation); // Create Popup
        POPUP = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Screen_Manager : MonoBehaviour
{
    public GameObject popupPrefab;
    public float CountDown = 10f;
    private float timer = 0f;
    private bool POPUP = false;

    void Start()
    {

    }

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
        Transform cameraTransform = transform;
        Vector3 popupPosition = cameraTransform.position + cameraTransform.forward * 2f;
        Instantiate(popupPrefab, popupPosition, cameraTransform.rotation);
        POPUP = true;
    }
}

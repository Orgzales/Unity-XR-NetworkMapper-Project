using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Manager : MonoBehaviour
{

    public float min_Height;
    public float max_Height;

    public GameObject[] Wifi_Symbol;

    public GameObject[] Wifi_Color_Object;

    private Connection_Spawner mainCameraScript;

    private void Start()
    {
        //debugging 
        // float Pillar_Height = Random.Range(min_Height, max_Height);
        float Pillar_Height = 0f;

        //Get the main camera script for dbm values from wifi script
        GameObject mainCamera = Camera.main.gameObject;
        mainCameraScript = mainCamera.GetComponent<Connection_Spawner>();
        int dBmValue = mainCameraScript.dBm_value;
        string Secuirty_type_value = mainCameraScript.Secuirty_type_value;

        if (dBmValue >= -60) //-67 = Amazing in dbm
        {
            Pillar_Height = max_Height;
        }
        else if (dBmValue >= -79 && dBmValue < -60) // -70 = okay
        {
            // float input_value = (float)dBmValue;
            float input_value = (float)dBmValue;
            Pillar_Height = (input_value - (-80f)) * (max_Height - min_Height) / ((-60f) - (-80f)) + min_Height;

            //https://math.stackexchange.com/questions/2833778/converting-between-different-scales

        }
        else if (dBmValue < -80) // -80 or lower is bad
        {
            Pillar_Height = min_Height;
        }
        if (dBmValue == 0) //windows testing delete for quest 2
        {
            Pillar_Height = 0f;
        }

        // Debug.Log("dbmValue height: " + dBmValue + "| random height: " + Pillar_Height);
        Pillar_Height = Random.Range(min_Height, max_Height); //for windows testing
        Adjust_Scale(Pillar_Height);
        Change_Wifi_Symbol(Secuirty_type_value);
        Change_Color(Pillar_Height);


    }

    public void Adjust_Scale(float height_Value)
    {
        transform.localScale = new Vector3(0.01f, height_Value, 0.01f);
        transform.localPosition = new Vector3(transform.localPosition.x, height_Value, transform.localPosition.z);
    }

    public void Change_Wifi_Symbol(string security_Type)
    {
        if (security_Type == "WPA/WPA2" || security_Type == "WPA3")
        {
            //change Wifi_Symbol to Green lock
            Wifi_Symbol[0].SetActive(true);
        }
        else if (security_Type == "WEP")
        {
            //change Wifi_Symbol to Caution
            Wifi_Symbol[1].SetActive(true);
        }
        else if (security_Type == "OPEN" || security_Type == "No Security/Signal")
        {
            //change Wifi_Symbol to Unlock
            Wifi_Symbol[2].SetActive(true);
        }
        else
        {
            //change Wifi_Symbol to Unknown
            Wifi_Symbol[3].SetActive(true);
        }
    }

    public void Change_Color(float height_Value)
    {

        float Color_Value = Mathf.Clamp01((height_Value - min_Height) / (max_Height - min_Height)); // Normalize for color 0% to 100%

        Color New_Color;
        if (Color_Value >= 0.5f)
        {
            New_Color = Color.Lerp(Color.yellow, Color.green, (Color_Value - 0.5f) * 2); // green to yellow | 0.6 to 0.3
        }
        else
        {
            New_Color = Color.Lerp(Color.red, Color.yellow, Color_Value * 2); //yellow to red | 0.3 to 0.0
        }

        for (int i = 0; i < Wifi_Color_Object.Length; i++)
        {
            Renderer Wifi_Color_Renderer = Wifi_Color_Object[i].GetComponent<Renderer>(); // Get the renderer of prefab
            Wifi_Color_Renderer.material.color = New_Color; // Change color of  prefab
        }

    }


    //https://www.crowdstrike.com/cybersecurity-101/cloud-security/shadow-it/
    //https://cai.io/resources/thought-leadership/shadow-IT-meaning-risks
    //shadow it - find other non secure routers that are dangerious to the secure ones that IT isn't aware.

    // public void Change_Wifi_Symbol(int radnom) //windows testing
    // {
    //     if (radnom == 0)
    //     {
    //         //change Wifi_Symbol to Green lock
    //         Wifi_Symbol[0].SetActive(true);
    //     }
    //     else if (radnom == 1)
    //     {
    //         //change Wifi_Symbol to Caution
    //         Wifi_Symbol[1].SetActive(true);
    //     }
    //     else if (radnom == 2)
    //     {
    //         //change Wifi_Symbol to Unlock
    //         Wifi_Symbol[2].SetActive(true);
    //     }
    //     else
    //     {
    //         //change Wifi_Symbol to Unknown
    //         Wifi_Symbol[3].SetActive(true);
    //     }
    // }


}

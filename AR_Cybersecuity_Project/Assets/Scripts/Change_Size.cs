using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Size : MonoBehaviour
{

    public float min_Height;
    public float max_Height;

    public GameObject[] symbol;


    private Connection_Spawner mainCameraScript;

    private void Start()
    {
        //debugging 
        // float random_Height = Random.Range(min_Height, max_Height);
        float random_Height = 0f;

        //Get the main camera script for dbm values from wifi script
        GameObject mainCamera = Camera.main.gameObject;
        mainCameraScript = mainCamera.GetComponent<Connection_Spawner>();
        int dBmValue = mainCameraScript.dBm_value;
        string Secuirty_type_value = mainCameraScript.Secuirty_type_value;

        if (dBmValue >= -60) //-67 = Amazing in dbm
        {
            random_Height = max_Height;
        }
        else if (dBmValue >= -79 && dBmValue < -60) // -70 = okay
        {
            // float input_value = (float)dBmValue;
            float input_value = (float)dBmValue;
            random_Height = (input_value - (-80f)) * (max_Height - min_Height) / ((-60f) - (-80f)) + min_Height;

            //https://math.stackexchange.com/questions/2833778/converting-between-different-scales

        }
        else if (dBmValue < -80) // -80 or lower is bad
        {
            random_Height = min_Height;
        }
        if (dBmValue == 0) //windows testing delete for quest 2
        {
            random_Height = 0f;
        }

        // Debug.Log("dbmValue height: " + dBmValue + "| random height: " + random_Height);
        Adjust_Scale(random_Height);
        Change_Symbol(Secuirty_type_value);




    }

    public void Adjust_Scale(float height_Value)
    {
        transform.localScale = new Vector3(0.01f, height_Value, 0.01f);
        transform.localPosition = new Vector3(transform.localPosition.x, height_Value, transform.localPosition.z);
    }

    public void Change_Symbol(string security_Type)
    {
        if (security_Type == "WPA/WPA2" || security_Type == "WPA3")
        {
            //change symbol to Green lock
            symbol[0].SetActive(true);
        }
        else if (security_Type == "WEP")
        {
            //change symbol to Caution
            symbol[1].SetActive(true);
        }
        else if (security_Type == "OPEN" || security_Type == "No Security/Signal")
        {
            //change symbol to Unlock
            symbol[2].SetActive(true);
        }
        else
        {
            //change symbol to Unknown
            symbol[3].SetActive(true);
        }
    }




    //https://www.crowdstrike.com/cybersecurity-101/cloud-security/shadow-it/
    //https://cai.io/resources/thought-leadership/shadow-IT-meaning-risks
    //shadow it - find other non secure routers that are dangerious to the secure ones that IT isn't aware.

    // public void Change_Symbol(int radnom) //windows testing
    // {
    //     if (radnom == 0)
    //     {
    //         //change symbol to Green lock
    //         symbol[0].SetActive(true);
    //     }
    //     else if (radnom == 1)
    //     {
    //         //change symbol to Caution
    //         symbol[1].SetActive(true);
    //     }
    //     else if (radnom == 2)
    //     {
    //         //change symbol to Unlock
    //         symbol[2].SetActive(true);
    //     }
    //     else
    //     {
    //         //change symbol to Unknown
    //         symbol[3].SetActive(true);
    //     }
    // }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change_Size : MonoBehaviour
{

    public float min_Height;
    public float max_Height;


    private Connection_Spawner mainCameraScript;

    private void Start()
    {
        //debugging 
        float random_Height = Random.Range(min_Height, max_Height);

        //Get the main camera script for dbm values from wifi script
        GameObject mainCamera = Camera.main.gameObject;
        mainCameraScript = mainCamera.GetComponent<Connection_Spawner>();
        float dBmValue = (float)mainCameraScript.dBm_value;

        if (dBmValue <= 0f)
        {
            random_Height = 0.01f;
        } //later on attempt the conversion rate of -79 to a range of 0 through 0.3f
          //use this later https://math.stackexchange.com/questions/2833778/converting-between-different-scales
          //Changes the prefab size based on the dBm value
          //https://www.crowdstrike.com/cybersecurity-101/cloud-security/shadow-it/
          //https://cai.io/resources/thought-leadership/shadow-IT-meaning-risks
          //shadow it - find other non secure routers that are dangerious to the secure ones that IT isn't aware.
        Adjust_Scale(random_Height);
        Debug.Log("dbmValue height: " + dBmValue + "| random height: " + random_Height);
    }

    public void Adjust_Scale(float height_Value)
    {
        transform.localScale = new Vector3(0.01f, height_Value, 0.01f);
        transform.localPosition = new Vector3(transform.localPosition.x, height_Value, transform.localPosition.z);
    }


}

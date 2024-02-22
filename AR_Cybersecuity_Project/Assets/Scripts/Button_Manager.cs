using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Manager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool Demo_Mode = false;
    private bool Overwrite_Mode = false;


    public void ToggleDemoButtonPress()
    {
        if (Demo_Mode == true)
        {
            Demo_Mode = false;
            Debug.Log("Demo Mode: " + Demo_Mode);
        }
        else
        {
            Demo_Mode = true;
            Debug.Log("Demo Mode: " + Demo_Mode);
        }
    }

    public void ToggleOverwriteButtonPress()
    {
        if (Overwrite_Mode == true)
        {
            Overwrite_Mode = false;
            Debug.Log("Overwrite Mode: " + Overwrite_Mode);
        }
        else
        {
            Overwrite_Mode = true;
            Debug.Log("Overwrite Mode: " + Overwrite_Mode);
        }
    }

    public void DeleteRadiusButtonPress()
    {
        Debug.Log("Delete Radius");
    }



}

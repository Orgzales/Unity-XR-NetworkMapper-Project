using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_ButtonManager : MonoBehaviour
{
    public GameObject PopupPrefab;

    public void WhiteList_ButtonPress()
    {
        Debug.Log("WhiteList Button Pressed");
        Destroy(PopupPrefab);
    }

    public void BlackList_ButtonPress()
    {
        Debug.Log("BlackList Button Pressed");
        Destroy(PopupPrefab);
    }

}

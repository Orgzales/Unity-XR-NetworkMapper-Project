using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp_ButtonManager : MonoBehaviour
{

    public HiddenSSID_Scan HiddenSSID_ScanScript;
    public GameObject PopupPrefab;

    public void WhiteList_ButtonPress()
    {
        // Debug.Log("WhiteList Button Pressed");
        Destroy(PopupPrefab);
        HiddenSSID_ScanScript.popupClosed = true;
        HiddenSSID_ScanScript.AddWhiteList();
    }

    public void BlackList_ButtonPress()
    {
        // Debug.Log("BlackList Button Pressed");
        Destroy(PopupPrefab);
        HiddenSSID_ScanScript.popupClosed = true;
        HiddenSSID_ScanScript.AddBlackList();
    }

}

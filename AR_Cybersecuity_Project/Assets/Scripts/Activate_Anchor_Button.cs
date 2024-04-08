using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Activate_Anchor_Button : MonoBehaviour
{

    public GameObject ActiveText;
    public GameObject FollowersPrefab;
    public GameObject VisibleText;

    private Connection_Spawner ConnectionSpawnerScript;
    private Other_Spawner_Manager otherSpawnerManagerScript;

    private bool isVisable = true;

    private void Start()
    {
        GameObject mainCamera = Camera.main.gameObject;
        ConnectionSpawnerScript = mainCamera.GetComponent<Connection_Spawner>();
        otherSpawnerManagerScript = mainCamera.GetComponent<Other_Spawner_Manager>();

    }

    public void ActivateAnchor()
    {
        otherSpawnerManagerScript.SetAllAnchorsTextFalse(); //setting any other anchor text that are active to false 
        ConnectionSpawnerScript.CurrentAnchorParentObject = FollowersPrefab; //Setting this anchor to be the active parent
        otherSpawnerManagerScript.parentObject = FollowersPrefab; //Setting this anchor to be the active parent

        TextMeshPro textComponent = ActiveText.GetComponent<TextMeshPro>();
        textComponent.text = "Active: True";

    }

    public void HideScan()
    {

        if (isVisable)
        {
            isVisable = false;
            TextMeshPro textComponent = VisibleText.GetComponent<TextMeshPro>();
            textComponent.text = "Visible: False";
            foreach (Transform child in FollowersPrefab.transform)
            {
                child.gameObject.SetActive(false);
                child.name = "HiddenScan";

            }
        }
        else
        {
            isVisable = true;
            TextMeshPro textComponent = VisibleText.GetComponent<TextMeshPro>();
            textComponent.text = "Visible: True";
            foreach (Transform child in FollowersPrefab.transform)
            {
                child.gameObject.SetActive(true);
                child.name = ConnectionSpawnerScript.previousNetworkName;
            }
        }


    }


}

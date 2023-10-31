using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Connection_Spawner : MonoBehaviour
{
    public GameObject parentObject; //Parent
    public GameObject[] prefabToInstantiate; //Child
    public float spawnHeight = 0.3f; //Distance from Camera
    public float checkRadius = 0.3f; //Distance between Points of access
    public string Prefab_Text_Name = ""; //name of objects display


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateChildObject", 0, 6);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateChildObject()
    {
        if (CanInstantiateHere()) //Checking if Object of prefab is near.
        {
            int randomValue = Random.Range(1, 4); //Change later based on dBm Value
            string text_Display = ""; //Change later with wifi info

            // Check if the parent object and prefab are set
            if (parentObject != null && prefabToInstantiate != null)
            {
                Vector3 cameraPosition = Camera.main.transform.position;

                // Calculate the position to spawn the object
                Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

                // Instantiate the prefab and set the parent to the parentObject
                GameObject newObject = Instantiate(prefabToInstantiate[randomValue - 1], spawnPosition, Quaternion.identity);
                newObject.transform.SetParent(parentObject.transform);

                switch (randomValue)
                {
                    case 1:
                        text_Display = "This is the Green Color";
                        break;
                    case 2:
                        text_Display = "This is the Yellow Color";
                        break;
                    case 3:
                        text_Display = "This is the Red Color";
                        break;
                };

                SetTextRecursively(newObject.transform, text_Display);

            }
            else
            {
                Debug.LogError("Parent object or prefab not set!");
            }

        }
    }

    void SetTextRecursively(Transform parent, string text)
    {
        // Get Parent -> Parent -> Textobject
        Transform textObjectTransform = parent.Find(Prefab_Text_Name);

        if (textObjectTransform != null)
        {
            // Access the TextMeshPro Text and changes it 
            TextMeshPro textComponent = textObjectTransform.GetComponent<TextMeshPro>();
            textComponent.text = text;
        }
        else
        {
            // Keep looking for the text for later code for BSSID
            foreach (Transform child in parent)
            {
                SetTextRecursively(child, text);
            }
        }

    }


    private bool CanInstantiateHere()
    {
        // Get the position of the main camera
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 checkPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

        // Check for colliders in the specified radius
        Collider[] colliders = Physics.OverlapSphere(checkPosition, checkRadius);

        if (colliders.Length == 0)
        {
            return true;
        }
        else
        {
            Debug.Log("ANother object too Close!");
            return false;
        }
    }
}


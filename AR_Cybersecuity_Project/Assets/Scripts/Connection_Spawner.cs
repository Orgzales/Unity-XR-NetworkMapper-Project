using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection_Spawner : MonoBehaviour
{
    public GameObject parentObject; // Reference to the parent object
    public GameObject[] prefabToInstantiate; // The prefab you want to instantiate
    public float spawnHeight = 0.3f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("InstantiateChildObject", 0, 6);
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     InstantiateChildObject();
        // }
    }

    void InstantiateChildObject()
    {

        int randomValue = Random.Range(1, 4);

        // Check if the parent object and prefab are set
        if (parentObject != null && prefabToInstantiate != null)
        {
            Vector3 cameraPosition = Camera.main.transform.position;

            // Calculate the position to spawn the object
            Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

            // Instantiate the prefab and set the parent to the parentObject
            GameObject newObject = Instantiate(prefabToInstantiate[randomValue - 1], spawnPosition, Quaternion.identity);

            // Set the parent of the new object to the parentObject
            newObject.transform.SetParent(parentObject.transform);

        }
        else
        {
            Debug.LogError("Parent object or prefab not set!");
        }
    }

    // private void test()
    // {
    //     // Get the position of the main camera
    //     Vector3 cameraPosition = Camera.main.transform.position;

    //     // Calculate the position to spawn the object
    //     Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

    //     // Instantiate the object at the calculated position
    //     Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    //     // Instantiate(cubePrefab, spawnPosition, ParentPrefab.identity);
    // }

}

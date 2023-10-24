using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection_Spawner : MonoBehaviour
{

    public GameObject cubePrefab;
    public float spawnHeight = 0.3f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //OLD SPAWN CODE
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Instantiate(cubePrefab, transform.position, Quaternion.identity);
        // }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Get the position of the main camera
            Vector3 cameraPosition = Camera.main.transform.position;

            // Calculate the position to spawn the object
            Vector3 spawnPosition = new Vector3(cameraPosition.x, cameraPosition.y - spawnHeight, cameraPosition.z);

            // Instantiate the object at the calculated position
            Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Checking_Internet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No Networks in Area");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("Wifi Networks are Availiable");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Network/Wifi Found through Mobile Accounts, Please log in"); //FOR RESNET
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

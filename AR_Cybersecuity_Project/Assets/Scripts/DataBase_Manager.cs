using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase_Manager : MonoBehaviour
{
    public string debugSSID;
    public GameObject textPrefab;
    public GameObject parentObject;


    private Dictionary<string, NetworkCounters> networkCounters = new Dictionary<string, NetworkCounters>();


    private class NetworkCounters
    {
        public int good_Counter = 0;
        public int ok_Counter = 0;
        public int bad_Counter = 0;
        public int vulnerable_Counter = 0;
        public int secure_Counter = 0;
    }

    void Start()
    {


    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            string Test_Key = debugSSID.ToString(); // Change Later

            if (!networkCounters.ContainsKey(Test_Key))
            {

                networkCounters[Test_Key] = new NetworkCounters();

                // GameObject newTextObject = Instantiate(textPrefab, parentObject.transform);


            }

            IncrementCounter(debugSSID.ToString(), CounterType.Good);
            UpdateCounterText(Test_Key, textPrefab);
        }


    }

    public enum CounterType
    {
        Good,
        Ok,
        Bad,
        Vulnerability,
        Secure
    }

    public void IncrementCounter(string networkName, CounterType counterType)
    {

        switch (counterType)
        {
            case CounterType.Good:
                // Debug.Log("Good Counter incremented" + networkCounters[networkName].good_Counter.ToString());
                networkCounters[networkName].good_Counter++;
                // Debug.Log("Good Counter incremented" + networkCounters[networkName].good_Counter.ToString());
                break;
            case CounterType.Ok:
                networkCounters[networkName].ok_Counter++;
                break;
            case CounterType.Bad:
                networkCounters[networkName].bad_Counter++;
                break;
            case CounterType.Vulnerability:
                networkCounters[networkName].vulnerable_Counter++;
                break;
            case CounterType.Secure:
                networkCounters[networkName].secure_Counter++;
                break;
            default:
                Debug.LogError("Unknown counter type!");
                break;
        }
    }


    void UpdateCounterText(string network_Key, GameObject textObject)
    {
        if (networkCounters.ContainsKey(network_Key))
        {
            NetworkCounters counters = networkCounters[network_Key];

            Text[] texts = textObject.GetComponentsInChildren<Text>();
            foreach (Text contents in texts)
            {
                switch (contents.gameObject.name)
                {
                    case "DATA_SSID_TEXT":
                        contents.text = network_Key;
                        break;
                    case "GOOD_TEXT":
                        Debug.Log("Good Counter incremented" + counters.good_Counter.ToString());
                        contents.text = "Good:" + counters.good_Counter.ToString();
                        break;
                    case "OK_TEXT":
                        contents.text = "Ok:" + counters.ok_Counter.ToString();
                        break;
                    case "BAD_TEXT":
                        contents.text = "Bad:" + counters.bad_Counter.ToString();
                        break;
                    case "VULNERABLE_TEXT":
                        contents.text = "Vulnerability:" + counters.vulnerable_Counter.ToString();
                        break;
                    case "SECURE_TEXT":
                        contents.text = "Secure:" + counters.secure_Counter.ToString();
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            Debug.LogWarning("Network key not found: " + network_Key);
        }
    }



}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase_Manager : MonoBehaviour
{

    public Checking_Internet Wifi_script;

    public string debugSSID;
    public GameObject textPrefab;
    public GameObject screenParentObejct; // for making new text objects 

    public GameObject cloneParentObject; //Where all the wifi clones are made


    private Dictionary<string, NetworkCounters> networkCounters = new Dictionary<string, NetworkCounters>();
    private string previousNetworkName;
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

        InvokeRepeating("UpdateData", 3, 3);
    }

    private void UpdateData()
    {


        string Test_Key = debugSSID.ToString(); // Change Later windows testing
        // string Test_Key = Wifi_script.wifiSSID; //Change Later with bssid

        // Debug.Log("Test_Key: " + Test_Key.ToString());
        if (!networkCounters.ContainsKey(Test_Key))
        {
            if (Test_Key == "No Networks in Area")
            {
                Test_Key = previousNetworkName;
            }
            else
            {
                networkCounters[Test_Key] = new NetworkCounters();
                previousNetworkName = Test_Key;

            }

            // GameObject newTextObject = Instantiate(textPrefab, screenParentObejct.transform);
        }
        else
        {
            previousNetworkName = Test_Key;
        }


        int dBm_value = Wifi_script.wifiSignalStrength; //based on dBm Value
        string Secuirty_type_value = Wifi_script.wifiAuthentication; //based on security type


        // string Secuirty_type_value = ""; //windows testing
        // int dBm_value = Random.Range(-60, -90);
        // int random = Random.Range(0, 2);
        // if (random == 0)
        // {
        //     Secuirty_type_value = "WPA2";
        // }
        // else
        // {
        //     Secuirty_type_value = "WEP";
        // }

        IncrementCounter(Test_Key.ToString(), dBm_value, Secuirty_type_value);
        UpdateCounterText(Test_Key, textPrefab);
        ChangeMapping(Test_Key);



    }



    public enum CounterType
    {
        Good,
        Ok,
        Bad,
        Vulnerability,
        Secure,
        Nothing //for no connection change later
    }

    public void IncrementCounter(string networkName, int networkStrength, string networkSecuirty)
    {

        CounterType SignalCounter;

        if (networkStrength >= -67)
        {
            SignalCounter = CounterType.Good;
        }
        else if (networkStrength >= -79 && networkStrength < -67)
        {
            SignalCounter = CounterType.Ok;
        }
        else if (networkStrength < -79)
        {
            SignalCounter = CounterType.Bad;
        }
        else
        {
            SignalCounter = CounterType.Bad;
        }

        CounterType SecurityCounter;
        if (networkSecuirty == "WPA3")
        {
            SecurityCounter = CounterType.Secure;
        }
        else if (networkSecuirty == "WPA/WPA2")
        {
            SecurityCounter = CounterType.Secure;
        }
        else if (networkSecuirty == "WEP")
        {
            SecurityCounter = CounterType.Vulnerability;
        }
        else if (networkSecuirty == "OPEN")
        {
            SecurityCounter = CounterType.Vulnerability;
        }
        else
        {
            SecurityCounter = CounterType.Vulnerability;
        }

        switch (SignalCounter)
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
            default:
                Debug.LogError("Unknown counter type!");
                break;
        }

        switch (SecurityCounter)
        {
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


    public void ChangeMapping(string networkSSID)
    {

        foreach (Transform wifiObject in cloneParentObject.transform)
        {
            if (wifiObject.name == networkSSID)
            {
                wifiObject.gameObject.SetActive(true);
            }
            else if (wifiObject.name == "No Networks in Area:" + previousNetworkName.ToString())
            {
                wifiObject.gameObject.SetActive(true);
            }
            else
            {
                wifiObject.gameObject.SetActive(false);
            }
        }

    }

    public void UpdateCounterText(string network_Key, GameObject textObject)
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
                        // Debug.Log("Good Counter incremented" + counters.good_Counter.ToString());
                        contents.text = "GOOD:" + counters.good_Counter.ToString();
                        break;
                    case "OK_TEXT":
                        contents.text = "OK:" + counters.ok_Counter.ToString();
                        break;
                    case "BAD_TEXT":
                        contents.text = "BAD:" + counters.bad_Counter.ToString();
                        break;
                    case "VULNERABLE_TEXT":
                        contents.text = "VULNERABLE:" + counters.vulnerable_Counter.ToString();
                        break;
                    case "SECURE_TEXT":
                        contents.text = "SECURED:" + counters.secure_Counter.ToString();
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


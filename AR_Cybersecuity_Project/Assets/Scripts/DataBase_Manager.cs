using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataBase_Manager : MonoBehaviour
{

    public Checking_Internet Wifi_script;

    public string debugSSID;
    public GameObject DataBase_Screen;
    public Text DataBase_BSSID_Screen;
    // public Text DataBase_WhiteList_Screen;
    // public Text DataBase_BlackList_Screen;
    public GameObject screenParentObejct; // for making new text objects 

    // public GameObject cloneParentObject; //Where all the wifi clones are made 
    public List<GameObject> cloneParentObjects; //Change to list of gameobjects that are all multiple parents



    private Dictionary<string, NetworkCounters> networkCounters = new Dictionary<string, NetworkCounters>();
    private Dictionary<string, Dictionary<string, int>> BSSID_History_Counters = new Dictionary<string, Dictionary<string, int>>();

    private string previousNetworkName;


    // public string BSSIDDEBUGTEXT; //windows testing
    private class NetworkCounters
    {
        public int good_Counter = 0;
        public int ok_Counter = 0;
        public int bad_Counter = 0;
        public int vulnerable_Counter = 0;
        public int secure_Counter = 0;

        public int no_Connection_Counter = 0;
    }

    void Start()
    {
        InvokeRepeating("UpdateData", 3, 3);
    }

    private void UpdateData()
    {


        // string SSID_Key = debugSSID.ToString(); // Change Later windows testing
        string SSID_Key = Wifi_script.wifiSSID;
        string BSSID_Key = Wifi_script.wifiBSSID;
        // string BSSID_Key = BSSIDDEBUGTEXT; // windows testing

        bool BSSID_ADD = true; //if no network connection do not update bssid counter

        if (!networkCounters.ContainsKey(SSID_Key))
        {
            // if (SSID_Key == "No Networks in Area") windows testing
            if (string.IsNullOrEmpty(SSID_Key) || SSID_Key.Equals("<unknown ssid>"))
            {
                SSID_Key = previousNetworkName;
                BSSID_ADD = false;
            }
            else
            {
                networkCounters[SSID_Key] = new NetworkCounters();
                previousNetworkName = SSID_Key;

            }

        }
        else
        {
            previousNetworkName = SSID_Key;
        }

        if (!(string.IsNullOrEmpty(BSSID_Key) || BSSID_Key.Equals("<unknown bssid>")) && BSSID_ADD)
        {
            if (!BSSID_History_Counters.ContainsKey(SSID_Key))
            { //if new network connection
                BSSID_History_Counters[SSID_Key] = new Dictionary<string, int>();
                BSSID_History_Counters[SSID_Key][BSSID_Key] = 1;
            }
            else
            { //if network dictonary already exists
                if (!BSSID_History_Counters[SSID_Key].ContainsKey(BSSID_Key))
                {//if same network but different bssid
                    BSSID_History_Counters[SSID_Key][BSSID_Key] = 1;
                }
                else
                {
                    BSSID_History_Counters[SSID_Key][BSSID_Key]++;
                }

            }
        }

        // Debug.Log(SSID_Key + ":" + BSSID_Key + "= " + BSSID_History_Counters[SSID_Key][BSSID_Key]); //windows testing

        int dBm_value = Wifi_script.wifiSignalStrength; //based on dBm Value
        string Secuirty_type_value = Wifi_script.wifiAuthentication; //based on security type

        IncrementCounter(SSID_Key.ToString(), dBm_value, Secuirty_type_value); //update network connection
        UpdateBSSIDHistory(SSID_Key); //update bsid history + counters to text on screen
        UpdateCounterText(SSID_Key, DataBase_Screen); //update the screen text
        ChangeMapping(SSID_Key); //change the mapping of the prefabs in scene


    }


    //Unity's Custom Variable Types 
    public enum CounterType
    {
        Good,
        Ok,
        Bad,
        Vulnerability,
        Secure,
        No_Connection
    }

    public void UpdateBSSIDHistory(string SSIDKEY)
    {

        Dictionary<string, int> innerDictionary = BSSID_History_Counters[SSIDKEY];
        string BSSID_History_text = "";
        foreach (var innerEntry in innerDictionary)
        {
            string BSSID_Key = innerEntry.Key;
            int BSSID_Counter = innerEntry.Value;
            BSSID_History_text += "► " + SSIDKEY + ":" + BSSID_Key + " = " + BSSID_Counter + "\n";
        }
        DataBase_BSSID_Screen.text = BSSID_History_text;

    }

    public void IncrementCounter(string networkName, int networkStrength, string networkSecuirty)
    {

        //Storing the Signal Strength Value
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
            SignalCounter = CounterType.No_Connection;
        }

        //Storing the Security Type Value
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
            SecurityCounter = CounterType.No_Connection;
        }

        //Incrementing the Counter for Signal Strength
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
                break;
        }

        //Incrementing the Counter for Security or Vulnerability
        switch (SecurityCounter)
        {
            case CounterType.Vulnerability:
                networkCounters[networkName].vulnerable_Counter++;
                break;
            case CounterType.Secure:
                networkCounters[networkName].secure_Counter++;
                break;
            default:
                break;
        }

        //Incrementing the Counter for no connection
        if (SignalCounter == CounterType.No_Connection || SecurityCounter == CounterType.No_Connection)
        {
            networkCounters[networkName].no_Connection_Counter++;
        }





    }


    public void ChangeMapping(string networkSSID)
    {

        // list of prefabs of names I dont want to change
        List<string> namesToKeep = new List<string> { "ShadowITPrefab", "ShadowITPrefabDemo", "OriginAnchor" };
        cloneParentObjects.RemoveAll(item => item == null); //incase the user deletes the anchor later on

        foreach (GameObject parentObject in cloneParentObjects)
        {
            //Setting each object of certain SSID to inactive or active base on name
            foreach (Transform wifiObject in parentObject.transform)
            {
                if (wifiObject.name == networkSSID || namesToKeep.Contains(wifiObject.name)) //debating if shadow should be with same mapping or not
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


    }

    public void UpdateCounterText(string network_Key, GameObject textObject)
    {
        //change this later for real database later
        if (networkCounters.ContainsKey(network_Key))
        {
            NetworkCounters counters = networkCounters[network_Key];

            // Get Parent -> Textobject to change the text
            Text[] texts = textObject.GetComponentsInChildren<Text>();
            foreach (Text contents in texts)
            {
                switch (contents.gameObject.name)
                {
                    case "DATA_SSID_TEXT":
                        contents.text = network_Key;
                        break;
                    case "GOOD_TEXT":
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
                    case "NOSIGNAL_TEXT":
                        contents.text = "NO SIGNAL:" + counters.no_Connection_Counter.ToString();
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


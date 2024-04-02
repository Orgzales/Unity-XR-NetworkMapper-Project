# OX-r TRAIL (AUGMENTED REALITY WIRELESS NETWORK SECURITY MAPPER)
An AR/VR/XR application made within a Unity Engine that will Map out wireless network connections to visualize signal strengths, access points, vulnerabilities, and more coming soon. Development was on the Meta Quest/ Meta Quest 2, however, applications with Android-supported AR/VR headsets should be supported. Some Code is supported with Windows Machines such as Apple Vision Pro but not fully tested.
(My Stetson University Senior Research Project for the year 2023 Fall- Spring 2024)

![OX_Trail_Logo](Documented/OxLogoLong.png)

 
 Some Commands:
```
- netsh wlan show profiles
- netsh wlan show interface
```
 Some Plugins: 
- MRTK Toolkit
- Andriod Unity Engine
- ARCORE XR

Added This project to Backdrop Builds V3 (Feb 26, 2024): https://backdropbuild.com/builds/v3/xr-wireless-network-mapper

Linkdin: https://www.linkedin.com/in/orion-gonzales-030b78196/
Toolkit Testing: [Linkdin Post](https://www.linkedin.com/feed/update/urn:li:activity:7117574652138266624/?updateEntityUrn=urn%3Ali%3Afs_feedUpdate%3A%28V2%2Curn%3Ali%3Aactivity%3A7117574652138266624%29)
 XR BackDropBuild:[Update Post](https://www.linkedin.com/feed/update/urn:li:activity:7166152294290419712/?updateEntityUrn=urn%3Ali%3Afs_feedUpdate%3A%28V2%2Curn%3Ali%3Aactivity%3A7166152294290419712%29)
Watch Short Demo Video of Program: [Youtube Demo](https://www.youtube.com/watch?v=wRlASP3Mfyk&t=13s)

## Unity Andriod -> Meta Quest 2
```ruby
AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
		
AndroidJavaObject wifiInfo = wifiManager.Call<AndroidJavaObject>("getConnectionInfo");
AndroidJavaObject connectivityManager = activity.Call<AndroidJavaObject>("getSystemService", "connectivity");
AndroidJavaObject networkInfo = connectivityManager.Call<AndroidJavaObject>("getNetworkInfo", 1);
AndroidJavaObject detailedState = networkInfo.Call<AndroidJavaObject>("getDetailedState");
		
AndroidJavaObject activeNetwork = connectivityManager.Call<AndroidJavaObject>("getActiveNetwork");
AndroidJavaObject networkCapabilities = connectivityManager.Call<AndroidJavaObject>("getNetworkCapabilities", activeNetwork);
```

## Step 0 Plans (Augest 2023)
	- 🗸 Activate Passthrough mode / AR - Finished
	- 🗸 Test Unity Webservices - Testing Process
	- 🗸 Get Hand Tracking to operate - Finished
	- 🗸 Get Developer Acess - RSA Finger Print Finished
	
## Step 1 Plans
	- 🗸 Build APK Files for Wireless AR - FINISHED
	- 🗸 WIFI Engines added for detecting nearby signals  - Quest 2 Shows SSID
	- 🗸 Custom Marking Code that marks WIFI Strengths: - COMPLETED
		- 🗸 No Signal = Red | -67dbm is Good signal (Max bars)
		- 🗸 Weak Signal = Yellow | -68dbm to -79dbm is Okay signal (Half Bars)
		- 🗸 Strong Signal = Green | -80dbm or lower is Very poor signal (Single or SOS bars)
		
## Step 2 Plans
	- 🗸 Get Recording with passthrough enabled - OBS/Wireless Computer (Not possible on quest 2) Wired Recording
	- 🗸 Have Flags or Tracking Color Floor based on signal
		- 🗸 Find a way to create a cube in the exact spot that the wifi connection is updated - CUBES SPAWN
		- 🗸 Create the cube's color based on the wifi connection - Wifi Points are Created based on dBs
		- 🗸 Have a radius possibly for the cubes to prevent to many being created - Prevents Cubes spawning within 2ft
	- 🗸 Get netgear router to purposly to show unsecure authencation - DONE

## Step 3 Plans
	- 🗸 Have it display the wifi scuirty to connected router.
	- 🗸 If authencation type is bad, give a alert to the cube being made.
		- 🗸 0 = Open or None = !!!!!DANGER!!!!! | Red
		- 🗸 1 = Wired Equivalent Privacy (WEP) = !!! HIGH ALERT !!! | Yellow
		- 🗸 2 = Wi-Fi Protected Access 2 (WPA2) = @ Secure @  | Green
		- 🗸 3 = Wi-Fi Protected Access 3 (WPA3) = #Protected# | Blue

## Step 4 Plans
	- 🗸 Have the Marked down cubes display information
		-  🗸 When cubed is touched, have it display previous data 
		-  🗸 Displays SSID BSSID and dBm when cube was created
	- 🗸 Possibly show the range of the cube that was created wiht new prefab. 
	- 🗸 Accident: Have program test if wifi/router has access to Better secuirty | (NEEDS MORE TESTING)
		- 🗸 EX: Router is set to no security, but it still is able to check if it can have wpa2

### Calling from Managers
```ruby
wifiSSID = wifiInfo.Call<string>("getSSID").Replace("\"", "");
wifiBSSID = wifiInfo.Call<string>("getBSSID");
wifiSignalStrength = wifiInfo.Call<int>("getRssi");
int LinkSpeed = wifiInfo.Call<int>("getLinkSpeed"); //Speed in Mbps
int frequencyInt = wifiInfo.Call<int>("getFrequency"); // Frequency in MHz
DataSpeedRate = LinkSpeed;
Freq_Network = frequencyInt;

string stateString = detailedState.Call<string>("toString");
string capabilities = wifiInfo.Call<string>("toString");
int securityTypeIndex = capabilities.IndexOf("Security type: ");
int endOfSecurityTypeIndex = capabilities.IndexOf(",", securityTypeIndex);
string securityTypeValue = capabilities.Substring(securityTypeIndex + 15, endOfSecurityTypeIndex - securityTypeIndex - 15);

bool hasInternet = networkCapabilities.Call<bool>("hasCapability", 12);
bool hasWep = networkCapabilities.Call<bool>("hasCapability", 15);
bool hasWpa2 = networkCapabilities.Call<bool>("hasCapability", 13);
bool hasWpa3 = networkCapabilities.Call<bool>("hasCapability", 26);
```

## Step 5 Plans (January 2024)
	- 🗸 Make New prefabs of Cubes to be Wifi Pillars
		- 🗸 Have a radius transparcy
		- 🗸 Change height of pillar based on dBm signal
		- 🗸 Still have a cube on top that displays data
		- 🗸 Keep other old properties
		- 🗸 Depending on secuirty, create a symbol on top
		- 🗸 Make small background for Debug Text.
		- 🗸 Change color hex code based on signal. 
	- 🗸 Increase the rate of the cubes
	- 🗸 Have a button on the Prefab to delete itself. 
		- 🗸 Small button on bottom of Prefab to set for false for now
	
	
## Step 6 Plans
	- 🗸 Has data display in a AR-hud so position does not matter and better layout
		- 🗸 add enough transparency 
		- 🗸 Remake a grid layout for better visualization
		- 🗸 Add Have the following display
			- 🗸 Both netwokr security and potential network secuirty
			- 🗸 Transmit Rate + receive Rate
			- 🗸 add this to prefab
	- 🗸 Create a data base of current properties of each scan for wifi network
		- 🗸 Make a new hud that will say the following
			- 🗸 New button on top tab to switch screens
			- 🗸 (Network SSID + Network BSSID)
				- 🗸 Good signal = int 
				- 🗸 Ok Signal = int
				- 🗸 Bad Signal = int
				- 🗸 Secure = int
				- 🗸 VUlnerable = int
			- 🗸 New network...
		- 🗸 Create Script that keeps the DataBase updating
			- 🗸 Create Dictionary that keeps each data of network
			- 🗸 Make sure No_Connection is included to the previous network
	- 🗸 make new prefab for BSSID connection
		- 🗸 Same SSID but DIFFERENT BSSID = New Pillars
		- 🗸 Create the new prefab 
		- 🗸 text should always be showing
			- 🗸 text = previous bssid -> new bssid 
		- 🗸 should not be created when first connection
		- 🗸 prefabs should disappear and reappear based same as wifi clones
	- 🗸 Create a button that delete wifiobjects on click
		- 🗸 One button: Toggle = Overwrite Nearby Connections.
		- 🗸 One button: Press = Get rid of all wifi objects within radius.
	- 🗸 SPELL CHECK

	
## Step 7 Plans	
	- 🗸 Add Demo Button 
		- 🗸 Toggle if strength values become random for demo purposes
		- 🗸 Toggle to make random secuirty/vulnerable values
	- 🗸 Clean Up code & Bug Check
		- 🗸 Add Data receive  & Transmit code
		- 🗸 Add Best secuirty and Current secuirty 
		- 🗸 Add these to Screen and Wifi Prefabs
		- 🗸 Remove unnessary code from here on out 
	- 🗸 Remove Delete object button higher
	- 🗸 Create Wifi prefab background transparent 
	- 🗸 Fix When no connection during database:
		- 🗸 Add No Connection counter
		- 🗸 Remove increment of VUlnerable counter
	- 🗸 Do final test for APK.9
		- 🗸 Test to make sure no connection doesn't interfere
		- 🗸 Make Sure BSSID Prefabs Work under correct conditions
		- 🗸 Test DATA Speeds + Resize Screen to fit new data
		- 🗸 Change color for corespsonding speed of DATA & Frequency
		- 🗸 Edit base perfab for BSSID quality
	- 🗸 Added network's Frequency


## Step 8 Plans
	- 🗸 Made Seperate Dictionary for later Database for the BSSID history
		- 🗸 Keep track of each connection of a bssid within the same network
		- 🗸 Display own text to data base screen
		- 🗸 if no connection BSSID shouldn't change
	- 🗸 Create Prefabs name based on SSID
	- 🗸 IF change to new SSID, set all objects to false
		- 🗸 If changed back reverse the true and false condition
	- 🗸 This will be to mimic saving for future functions. 
		- 🗸 If no connection, defualt to previous SSID
	- 🗸 Mimic Finding Shadow ITs Networks that are hidden
		- 🗸 install ARcore XR plugin
		- 🗸 Detect Hidden SSIDs when scanning
			- 🗸 Add list to new data screen in AR
			- 🗸 Make script for popup for new SSID
				- 🗸 Ask if it's white or black listed
				- 🗸 Remove any Duplicates with shadow scan
				- 🗸 get how many hidden networks within area 
					- 🗸 make prefab that holds this info
					- 🗸 add info of number of white or black listed 
			- 🗸 When new hidden SSID is scanned, popup window appears
				- 🗸 When Popup window alert prefab Instantiates, be in correct position
					- 🗸 Rotation of screen is locked
					- 🗸 Y-axis position is locked 
					- 🗸 Dynamic X+Z Axis within MRTK Scene
				- 🗸 Using IEnumerators, only make a screen when user gives input
				- 🗸 Cycle through all hidden/surrounding SSIDs
				- 🗸 Accumulate the user input within the White List & Black List networks
			- 🗸 Create new dictionary for sudo database of shadow itself
				- 🗸 make new dictionary (Whitelsited or blacklisted)
		- 🗸 Have SSIDs become white or black listed
	- 🗸 Create a button that repeats the Shadow IT scan
		- 🗸 button should clear out Allprevious SSID scan history to rescan in current location
			- 🗸 button should keep white + black List data
		- 🗸 New prefab should be placed where rescan
			- 🗸 Should display both white + black lists data
		- 🗸 if network gets changed it will remove it from previous list
		- 🗸 prefabs should not change when network changes
			- 🗸 Have delete button with shadow prefab
		- 🗸 Make button not pressable when the scan is already happening to prevent crash
		- 🗸 Add the color violet to these prefabs to have some material 
		- 🗸 Within Shadow Scan Prefab create two seperate text boxes for each data set
			- 🗸 Within Text Details, insert Delete button here instead
			- 🗸 Stop from users accidentally deleting info 
		- 🗸 Change function if Demo mode is activated
			- 🗸 Should have set list of SSIDs ready for example scanning 
			- 🗸 User can see if Prefab is a Demo or not 

### Method For Start of ShadowITScan
```ruby
    private IEnumerator ScanForHiddenSSIDs()
    {
        yield return new WaitForSeconds(1.0f);

        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
        wifiManager.Call<bool>("startScan"); //works for restarting scan
        AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");

        int scanResultsCount = scanResults.Call<int>("size");
        int hiddenFound = 0;

        for (int i = 0; i < scanResultsCount; i++)
        {
            AndroidJavaObject scanResult = scanResults.Call<AndroidJavaObject>("get", i);
            string ssid = scanResult.Get<string>("SSID");
            int signalStrength = scanResult.Get<int>("level"); //works

            if (string.IsNullOrEmpty(ssid)) //For Rouge or unnamed ssid networks 
            {
                ssid = "Hidden/NoName SSID Found #" + hiddenFound.ToString();
                hiddenFound++;
            }

            string SSIDInfo = ssid + " (" + signalStrength.ToString() + " dBm)";

            if (!allSSIDs.Contains(ssid))
            {
                allSSIDs.Add(ssid);
                CurrentSSID = ssid;
                SSIDSignal[ssid] = signalStrength;
                ShowPopup(SSIDInfo);
                yield return new WaitUntil(() => popupClosed);
                popupClosed = false; // Reset
            }

        }
        Other_Spawner_ManagerScript.SpawnShadowITPrefab(); //makes new prefab
        NoActiveScanning = true; //Scanning ends here when prefab is created
        SetWhiteText();
        SetBlackText();
        InvokeRepeating("UpdateSignals", 2.0f, 6.0f);
    }
```


## Step 9 Plans
	- 🗸 Fix Shadow Prefab Delte Bug
	- 🗸 Fix StartCourtine Bug
		- 🗸 List and Dictionary should clear each scan
		- 🗸 Update the text of the Screen after scan if finished
		- 🗸 Set all important varaibles when button is press not during scan
	- 🗸 Add signal strength tracking on hidden ssids scan 
		- 🗸 Have each one colorized based on dBm
		- 🗸 Add signal Str to shadow Prefab
		- 🗸 add a tracking function within Shadow Scan function to keep updated signals 
			- 🗸 When user travels, the function updates the scan signals  
			- 🗸 When the user rescans the hidden SSIDs, the scan should stop to prevent crashes
			- 🗸 Update the database screen when the function repeats 
			- 🗸 If new SSID or HIDDEN SSID is detected notify the user 
			- 🗸 if a SSID within the list of tracking and there is no signal update the signal to -999
			- 🗸 Repeat this for Demo Mode 
				- 🗸 Add in a random function to mimic a new network detected 
			- 🗸 Change color of dBm based on new Hidden SSID and no signal to be more noticable
	- 🗸 During Shadow SSID Scan, if a network with no ssid is detected or found, rename it to "Hidden Network Found" 
	- 🗸 Get Wifi Pineapple configured to see if mimicing networking risk can be detected
		- 🗸 Test with same SSID as Stetson vs Stetson Named SSID
		- Record Results of VR application w/ Wifi_Pineapple + NETGEAR
	- 🗸 Make a seperate manager script that handles all other non-connection strength prefab spawns.
		- 🗸 Shadow IT Prefab
		-  Regular Anchor Prefab
	
### ReScanning Hidden SSID AndriodJavaObjects
```ruby
AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
wifiManager.Call<bool>("startScan"); //works for restarting scan
AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");
int scanResultsCount = scanResults.Call<int>("size");
```

## Step 10 plans
	- 🗸 Create an Anchor point that the prefabs spawn locations are based
		- 🗸 Install ARCORE XR Plugin
		- 🗸 Create Anchor Prefab
			- 🗸 Each Anchor should have different name
			- 🗸 Anchor should have ability to be deleted with each isntance within 
			- 🗸 Not being affected by the override or delete commands 
		- 🗸 Have a button to drop an anchor 
		- 🗸 Allow the user to grab ancor and adjust it 
			- 🗸 This is incase the VR adjusts or resets user's position. 
		- 🗸 Have these anchors be storeable
			- 🗸 Have the Wifi Scans, Bssid Scans, or Shadow Scans within hierarchy 
			- 🗸 When a new Anchor is set, it should automatically become the new Origin for scans
			- 🗸 Anchors should not switch between networks
				- 🗸 Example: User places anchor in roomA with WifiA
				- 🗸 User scans with Wifi A, then switches network to WifiB
				- 🗸 The anchor would continue to store wifi maps on both networks 
				- 🗸 When the user shrinks the anchor to see full map, switching networks would adjust to same size aswell.
	- 🗸 Add the following functions to Anchor
		- 🗸 Delete Anchor button to delete all prefabs
		- 🗸 Activate button to change which Anchor you would use
			- 🗸 Text should change when Anchor is active or not
			- 🗸 Wifi Scans woul switch to the anchor that has been activated
		- 🗸 Ability to move the anchor only on the X & Y axis
			- 🗸 Make sure Rotation is locked to avoid slanted objects
			- 🗸 Size of anchors can be modified. 
	- 🗸 Can anchors be inside of anchors? 
		- 🗸 Technically yes but for now gonna attempt to avoid that. 
	- 🗸 Have the user be able to shrink and adjust of whole mapped out anchor
		- 🗸 This could allow the impressive display within the AR enviroment
		- 🗸 Easier to export photos and videos built into the VR functions
	- 🗸 Make Origin Anchor not able to be deleted
		- 🗸 If Current Active Anchor is Deleted, the program will default to the Origin Anchor.
		
## Step 11 Plans
	- 🗸 Clean Up Scene 1
		- 🗸 Keep Scene 0 as Backup 
	- 🗸 FIX BSSID Bug. 
	- 🗸 FIX WHITE + BLACK LIST BUG. 
	- 🗸 Make custom script to turn Diagnostics off
	-  Double check bugs and clean unneeded code	
	- 🗸 Make and Change splash screen 
	- 🗸 Add LOGO to Popup Screen 
	- Make a new Tutorial/info Screen about OXr Trail
		- One Screen should display what the program does
		- Second Screen should explain button controls
	- Readjust all prefabs in opening scene
	- Find out how to make popup logs in the quest 2 manager 
	

## Step 12 Plans
	-  Finish Research Paper + Presentation Video Field Testing 
	-  If there is time will continue the rest in this step 
	-  Add a button or UI to let user create their own cubes for info
		-  The custom cube can hold info such as room number and etc. 
	-  Save objects layout when logging off or switching
		-  first, find a way to save scene
			-  Save position/direction that previous was looking
			-  When in that position, recreate all objects
			-  Reload save
		-  Second, find a way to save all objects
			-  Save all locations of (x,y,z) cords of objects
			-  Save data of those prefabs 
		-  When log off or shut down, reloading saves all locations 
	

# APK Versions
	- APK 0.0.1: Testing Mixed Reality 
	- APK 0.0.2: Getting SSID
	- APK 0.0.3: Getting SSID + BSSID
	- APK 0.0.4: Getting SSID + BSSID + RSSI/Signal
	- APK 0.0.5: Takes all 3 and makes prefabs base on location
	- APK 0.0.6: Added Auth/Secuirty Detection
	- APK 0.0.7: debugging Auth/Secuirty Detection
	- APK 0.0.8: New Prefabs/screen + Quality of Life
	- APK 0.0.9: Sudo DataBase + Access Points + Data Rates + New Prefabs 
	- APK 0.1.0: Shadow IT Scan function 
	- APK 0.1.1: BackDrop Build Demo 
	- APK 0.1.2: Anchor Points 
	- APK 0.1.3: Offical Release V1 
	
### Future Cybersecuirty Ideas
	- Packet Sniffing 
	- Historic Log Organization 
	- Simulate Network Traffic 
	- Simulate firewall 
	- Simulate Shadow IT 
	- Mac Adress Filtering
		
### Some Sources

	- https://math.stackexchange.com/questions/2833778/converting-between-different-scales
	- https://www.crowdstrike.com/cybersecurity-101/cloud-security/shadow-it/
    - https://cai.io/resources/thought-leadership/shadow-IT-meaning-risks
	- https://skarredghost.com/2022/01/05/how-to-oculus-spatial-anchors-unity-2/ 
	- https://docs.unity3d.com/Packages/com.unity.xr.arcore@4.1/manual/index.html
	
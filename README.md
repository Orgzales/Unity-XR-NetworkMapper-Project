# AUGMENTED REALITY WIRELESS NETWORK SECURITY MAPPER
An AR/VR/XR application made within a Unity Engine that will Map out wireless network connections to visualize signal strengths, access points, vulnerabilities, and more coming soon. Development was on the Meta Quest/ Meta Quest 2, however, applications with Android-supported AR/VR headsets should be supported. Some Code is supported with Windows Machines such as Apple Vision Pro but not fully tested.
(My Stetson University Senior Research Project for the year 2023 Fall- Spring 2024)

 
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

##Unity Andriod -> Meta Quest 2
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
	- [x] Activate Passthrough mode / AR - Finished
	- [x] Test Unity Webservices - Testing Process
	- [x] Get Hand Tracking to operate - Finished
	- [x] Get Developer Acess - RSA Finger Print Finished
	
## Step 1 Plans
	- [x] Build APK Files for Wireless AR - FINISHED
	- [x] WIFI Engines added for detecting nearby signals  - Quest 2 Shows SSID
	- [x] Custom Marking Code that marks WIFI Strengths: - COMPLETED
		- [x] No Signal = Red | -67dbm is Good signal (Max bars)
		- [x] Weak Signal = Yellow | -68dbm to -79dbm is Okay signal (Half Bars)
		- [x] Strong Signal = Green | -80dbm or lower is Very poor signal (Single or SOS bars)
		
## Step 2 Plans
	- [x] Get Recording with passthrough enabled - OBS/Wireless Computer (Not possible on quest 2) Wired Recording
	- [x] Have Flags or Tracking Color Floor based on signal
		- [x] Find a way to create a cube in the exact spot that the wifi connection is updated - CUBES SPAWN
		- [x] Create the cube's color based on the wifi connection - Wifi Points are Created based on dBs
		- [x] Have a radius possibly for the cubes to prevent to many being created - Prevents Cubes spawning within 2ft
	- [x] Get netgear router to purposly to show unsecure authencation - DONE

## Step 3 Plans
	- [x] Have it display the wifi scuirty to connected router.
	- [x] If authencation type is bad, give a alert to the cube being made.
		- [x] 0 = Open or None = !!!!!DANGER!!!!! | Red
		- [x] 1 = Wired Equivalent Privacy (WEP) = !!! HIGH ALERT !!! | Yellow
		- [x] 2 = Wi-Fi Protected Access 2 (WPA2) = @ Secure @  | Green
		- [x] 3 = Wi-Fi Protected Access 3 (WPA3) = #Protected# | Blue

## Step 4 Plans
	- [x] Have the Marked down cubes display information
		-  [x] When cubed is touched, have it display previous data 
		-  [x] Displays SSID BSSID and dBm when cube was created
	- [x] Possibly show the range of the cube that was created wiht new prefab. 
	- [x] Accident: Have program test if wifi/router has access to Better secuirty | (NEEDS MORE TESTING)
		- [x] EX: Router is set to no security, but it still is able to check if it can have wpa2

###Calling from Managers
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
	- [x] Make New prefabs of Cubes to be Wifi Pillars
		- [x] Have a radius transparcy
		- [x] Change height of pillar based on dBm signal
		- [x] Still have a cube on top that displays data
		- [x] Keep other old properties
		- [x] Depending on secuirty, create a symbol on top
		- [x] Make small background for Debug Text.
		- [x] Change color hex code based on signal. 
	- [x] Increase the rate of the cubes
	- [x] Have a button on the Prefab to delete itself. 
		- [x] Small button on bottom of Prefab to set for false for now
	
	
## Step 6 Plans
	- [x] Has data display in a AR-hud so position does not matter and better layout
		- [x] add enough transparency 
		- [x] Remake a grid layout for better visualization
		- [x] Add Have the following display
			- [x] Both netwokr security and potential network secuirty
			- [x] Transmit Rate + receive Rate
			- [x] add this to prefab
	- [x] Create a data base of current properties of each scan for wifi network
		- [x] Make a new hud that will say the following
			- [x] New button on top tab to switch screens
			- [x] (Network SSID + Network BSSID)
				- [x] Good signal = int 
				- [x] Ok Signal = int
				- [x] Bad Signal = int
				- [x] Secure = int
				- [x] VUlnerable = int
			- [x] New network...
		- [x] Create Script that keeps the DataBase updating
			- [x] Create Dictionary that keeps each data of network
			- [x] Make sure No_Connection is included to the previous network
	- [x] make new prefab for BSSID connection
		- [x] Same SSID but DIFFERENT BSSID = New Pillars
		- [x] Create the new prefab 
		- [x] text should always be showing
			- [x] text = previous bssid -> new bssid 
		- [x] should not be created when first connection
		- [x] prefabs should disappear and reappear based same as wifi clones
	- [x] Create a button that delete wifiobjects on click
		- [x] One button: Toggle = Overwrite Nearby Connections.
		- [x] One button: Press = Get rid of all wifi objects within radius.
	- [x] SPELL CHECK

	
## Step 7 Plans	
	- [x] Add Demo Button 
		- [x] Toggle if strength values become random for demo purposes
		- [x] Toggle to make random secuirty/vulnerable values
	- [x] Clean Up code & Bug Check
		- [x] Add Data receive  & Transmit code
		- [x] Add Best secuirty and Current secuirty 
		- [x] Add these to Screen and Wifi Prefabs
		- [x] Remove unnessary code from here on out 
	- [x] Remove Delete object button higher
	- [x] Create Wifi prefab background transparent 
	- [x] Fix When no connection during database:
		- [x] Add No Connection counter
		- [x] Remove increment of VUlnerable counter
	- [x] Do final test for APK.9
		- [x] Test to make sure no connection doesn't interfere
		- [x] Make Sure BSSID Prefabs Work under correct conditions
		- [x] Test DATA Speeds + Resize Screen to fit new data
		- [x] Change color for corespsonding speed of DATA & Frequency
		- [x] Edit base perfab for BSSID quality
	- [x] Added network's Frequency


## Step 8 Plans
	- [x] Made Seperate Dictionary for later Database for the BSSID history
		- [x] Keep track of each connection of a bssid within the same network
		- [x] Display own text to data base screen
		- [x] if no connection BSSID shouldn't change
	- [x] Create Prefabs name based on SSID
	- [x] IF change to new SSID, set all objects to false
		- [x] If changed back reverse the true and false condition
	- [x] This will be to mimic saving for future functions. 
		- [x] If no connection, defualt to previous SSID
	- [x] Mimic Finding Shadow ITs Networks that are hidden
		- [x] install ARcore XR plugin
		- [x] Detect Hidden SSIDs when scanning
			- [x] Add list to new data screen in AR
			- [x] Make script for popup for new SSID
				- [x] Ask if it's white or black listed
				- [x] Remove any Duplicates with shadow scan
				- [x] get how many hidden networks within area 
					- [x] make prefab that holds this info
					- [x] add info of number of white or black listed 
			- [x] When new hidden SSID is scanned, popup window appears
				- [x] When Popup window alert prefab Instantiates, be in correct position
					- [x] Rotation of screen is locked
					- [x] Y-axis position is locked 
					- [x] Dynamic X+Z Axis within MRTK Scene
				- [x] Using IEnumerators, only make a screen when user gives input
				- [x] Cycle through all hidden/surrounding SSIDs
				- [x] Accumulate the user input within the White List & Black List networks
			- [x] Create new dictionary for sudo database of shadow itself
				- [x] make new dictionary (Whitelsited or blacklisted)
		- [x] Have SSIDs become white or black listed
	- [x] Create a button that repeats the Shadow IT scan
		- [x] button should clear out Allprevious SSID scan history to rescan in current location
			- [x] button should keep white + black List data
		- [x] New prefab should be placed where rescan
			- [x] Should display both white + black lists data
		- [x] if network gets changed it will remove it from previous list
		- [x] prefabs should not change when network changes
			- [x] Have delete button with shadow prefab
		- [x] Make button not pressable when the scan is already happening to prevent crash
		- [x] Add the color violet to these prefabs to have some material 
		- [x] Within Shadow Scan Prefab create two seperate text boxes for each data set
			- [x] Within Text Details, insert Delete button here instead
			- [x] Stop from users accidentally deleting info 
		- [x] Change function if Demo mode is activated
			- [x] Should have set list of SSIDs ready for example scanning 
			- [x] User can see if Prefab is a Demo or not 

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
	- [x] Fix Shadow Prefab Delte Bug
	- [x] Fix StartCourtine Bug
		- [x] List and Dictionary should clear each scan
		- [x] Update the text of the Screen after scan if finished
		- [x] Set all important varaibles when button is press not during scan
	- [x] Add signal strength tracking on hidden ssids scan 
		- [x] Have each one colorized based on dBm
		- [x] Add signal Str to shadow Prefab
		- [x] add a tracking function within Shadow Scan function to keep updated signals 
			- [x] When user travels, the function updates the scan signals  
			- [x] When the user rescans the hidden SSIDs, the scan should stop to prevent crashes
			- [x] Update the database screen when the function repeats 
			- [x] If new SSID or HIDDEN SSID is detected notify the user 
			- [x] if a SSID within the list of tracking and there is no signal update the signal to -999
			- [x] Repeat this for Demo Mode 
				- [x] Add in a random function to mimic a new network detected 
			- [x] Change color of dBm based on new Hidden SSID and no signal to be more noticable
	- [x] During Shadow SSID Scan, if a network with no ssid is detected or found, rename it to "Hidden Network Found" 
	- [x] Get Wifi Pineapple configured to see if mimicing networking risk can be detected
		- [x] Test with same SSID as Stetson vs Stetson Named SSID
		- Record Results of VR application w/ Wifi_Pineapple + NETGEAR
	- [x] Make a seperate manager script that handles all other non-connection strength prefab spawns.
		- [x] Shadow IT Prefab
		- [] Regular Anchor Prefab
	
###ReScanning Hidden SSID AndriodJavaObjects
```ruby
AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
AndroidJavaObject wifiManager = activity.Call<AndroidJavaObject>("getSystemService", "wifi");
wifiManager.Call<bool>("startScan"); //works for restarting scan
AndroidJavaObject scanResults = wifiManager.Call<AndroidJavaObject>("getScanResults");
int scanResultsCount = scanResults.Call<int>("size");
```

## Step 10 plans
	- Create an Anchor point that the prefabs spawn locations are based
		- [x] LOOK INTO ARCORE XR Plugin: https://docs.unity3d.com/Packages/com.unity.xr.arcore@4.1/manual/index.html
		- [] Create Anchor Prefab
			- [] Each Anchor should have different name
			- [] Anchor should have ability to be deleted with each isntance within 
			- [] Not being affected by the override or delete commands 
		- [] Have a button to drop an anchor 
		- [] Allow the user to grab ancor and adjust it 
			- [] This is incase the VR adjusts or resets user's position. 
		- [] Have these anchors be storeable
			- [] Have the Wifi Scans, Bssid Scans, or Shadow Scans within hierarchy 
			- [] Anchors should not switch between networks
				- [] Example: User places anchor in roomA with WifiA
				- [] User scans with Wifi A, then switches network to WifiB
				- [] The anchor would continue to store wifi maps on both networks 
				- [] When the user shrinks the anchor to see full map, switching networks would adjust to same size aswell.
	- [] Add the following functions to Anchor
		- [] Delete Anchor button to delete all prefabs
		- [] Activate button when changing Anchors 
		- [] Ability to move the anchor only on the X & Y axis
			- [] Make sure Rotation is locked to avoid slanted objects
			- [] Size of anchors can be modified. 
	- [] Can anchors be inside of anchors? 
		- [] Technically yes but for now gonna attempt to avoid that. 
	- [] Have the user be able to shrink and adjust of whole mapped out anchor
		- [] This could allow the impressive display within the AR enviroment
		- [] Easier to export photos and videos built into the VR functions

		
## Step 11 Plans
	- [] FIX BSSID Bug. 
	- [] FIX WHITE + BLACK LIST BUG. 
	- [] Double check bugs and unneeded code	
	- [] Change splash screen 
	- [] Save objects layout when logging off or switching
		- [] first, find a way to save scene
			- [] Save position/direction that previous was looking
			- [] When in that position, recreate all objects
			- [] Reload save
		- [] Second, find a way to save all objects
			- [] Save all locations of (x,y,z) cords of objects
			- [] Save data of those prefabs 
		- [] When log off or shut down, reloading saves all locations 

## Step 12 Plans
	- [] Finish Research Paper + Presentation Video Field Testing 
	- [] If there is time will continue the rest in this step 
	- [] Add a button or UI to let user create their own cubes for info
		- [] The custom cube can hold info such as room number and etc. 
	- [] Have a delete button on the wifi pillars
	

# APK Versions
	- APK 1: Testing Mixed Reality 
	- APK 2: Getting SSID
	- APK 3: Getting SSID + BSSID
	- APK 4: Getting SSID + BSSID + RSSI/Signal
	- APK 5: Takes all 3 and makes prefabs base on location
	- APK 6: Added Auth/Secuirty Detection
	- APK 7: debugging Auth/Secuirty Detection
	- APK 8: New Prefabs/screen + Quality of Life
	- APK 9: Sudo DataBase + Access Points + Data Rates + New Prefabs 
	- APK 10: Shadow IT Scan function 
	- APK 11: BackDrop Build Demo 
	- APK 12: Anchor Points 
	
### Future Cybersecuirty Ideas
	- Packet Sniffing 
	- Historic Log Organization 
	- Simulate Network Traffic 
	- Simulate firewall 
	- Simulate Shadow IT 
	- Mac Adress Filtering
		
### Some Sources

	-https://math.stackexchange.com/questions/2833778/converting-between-different-scales
	-https://www.crowdstrike.com/cybersecurity-101/cloud-security/shadow-it/
    -https://cai.io/resources/thought-leadership/shadow-IT-meaning-risks
	-https://skarredghost.com/2022/01/05/how-to-oculus-spatial-anchors-unity-2/ 

	
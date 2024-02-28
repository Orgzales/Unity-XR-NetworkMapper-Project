# AUGMENTED REALITY WIRELESS NETWORK SECURITY MAPPER
An AR/VR/XR application made within a Unity Engine that will Map out wireless network connections to visualize signal strengths, access points, vulnerabilities, and more coming soon. Development was on the Meta Quest/ Meta Quest 2, however, applications with Android-supported AR/VR headsets should be supported. Some Code is supported with Windows Machines such as Apple Vision Pro but not fully tested.
(My Stetson University Senior Research Project for the year 2023 Fall- Spring 2024)

 
 Some Commands:
- netsh wlan show profiles
- netsh wlan show interface
	
 Some Plugins: 
- MRTK Toolkit
- Andriod Unity Engine
- ARCORE XR

Added This project to Backdrop Builds V3 (Feb 26, 2024): https://backdropbuild.com/builds/v3/xr-wireless-network-mapper

Post on Linkdin: https://www.linkedin.com/in/orion-gonzales-030b78196/
	
 Toolkit Testing: https://www.linkedin.com/feed/update/urn:li:activity:7117574652138266624/?updateEntityUrn=urn%3Ali%3Afs_feedUpdate%3A%28V2%2Curn%3Ali%3Aactivity%3A7117574652138266624%29
	
 XR BackDropBuild Annocement: https://www.linkedin.com/feed/update/urn:li:activity:7166152294290419712/?updateEntityUrn=urn%3Ali%3Afs_feedUpdate%3A%28V2%2Curn%3Ali%3Aactivity%3A7166152294290419712%29


# Step 0 Plans (Augest 2023)
	- 🗸 Activate Passthrough mdoe / AR - Finished
	- 🗸 Test Unity Webservices - Testing Process
	- 🗸 Get Hand Tracking to operate - Finished
	- 🗸 Get Developer Acess - RSA Finger Print Finished
	
# Step 1 Plans
	- 🗸 Build APK Files for Wireless AR - FINISHED
	- 🗸 WIFI Engines added for detecting nearby signals  - Quest 2 Shows SSID
	- 🗸 Custom Marking Code that marks WIFI Strengths: - COMPLETED
		- 🗸 No Signal = Red | -67dbm is Good signal (Max bars)
		- 🗸 Weak Signal = Yellow | -68dbm to -79dbm is Okay signal (Half Bars)
		- 🗸 Strong Signal = Green | -80dbm or lower is Very poor signal (Single or SOS bars)
		
# Step 2 Plans
	- 🗸 Get Recording with passthrough enabled - OBS/Wireless Computer (Not possible on quest 2) Wired Recording
	- 🗸 Have Flags or Tracking Color Floor based on signal
		- 🗸 Find a way to create a cube in the exact spot that the wifi connection is updated - CUBES SPAWN
		- 🗸 Create the cube's color based on the wifi connection - Wifi Points are Created based on dBs
		- 🗸 Have a radius possibly for the cubes to prevent to many being created - Prevents Cubes spawning within 2ft
	- 🗸 Get netgear router to purposly to show unsecure authencation - DONE

# Step 3 Plans
	- 🗸 Have it display the wifi scuirty to connected router.
	- 🗸 If authencation type is bad, give a alert to the cube being made.
		- 🗸 0 = Open or None = !!!!!DANGER!!!!! | Red
		- 🗸 1 = Wired Equivalent Privacy (WEP) = !!! HIGH ALERT !!! | Yellow
		- 🗸 2 = Wi-Fi Protected Access 2 (WPA2) = @ Secure @  | Green
		- 🗸 3 = Wi-Fi Protected Access 3 (WPA3) = #Protected# | Blue

# Step 4 Plans
	- 🗸 Have the Marked down cubes display information
		-  🗸 When cubed is touched, have it display previous data - Displays SSID BSSID and dBm when cube was created
	- 🗸 Possibly show the range of the cube that was created wiht new prefab. 
	- 🗸 Accident: Have program test if wifi/router has access to Better secuirty | (NEEDS MORE TESTING)
		- 🗸 EX: Router is set to no security, but it still is able to check if it can have wpa2

# Step 5 Plans (January 2024)
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
	
	
# Step 6 Plans
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
	
	
# Step 7 Plans	
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
	- Do final test for APK.9
		- 🗸 Test to make sure no connection doesn't interfere
		- 🗸 Make Sure BSSID Prefabs Work under correct conditions
		- 🗸 Test DATA Speeds + Resize Screen to fit new data
		- Change color for corespsonding speed of DATA & Frequency
		- 🗸 Edit base perfab for BSSID quality
	- 🗸 Added network's Frequency
	- Add BSSIDs to data base 
		- Create larger creation radius 
		- Timer till creation 
		
# Step 8 Plans
	- 🗸 Create Prefabs name based on SSID
	- 🗸 IF change to new SSID, set all objects to false
		- 🗸 If changed back reverse the true and false condition
	- 🗸 This will be to mimic saving for future functions. 
		- 🗸 If no connection, defualt to previous SSID
	- Mimic Finding Shadow ITs Networks that are hidden
		- Detect Hidden SSIDs when scanning
			- Add list to new data screen in AR
			- Make script for popup for new SSID
				- Ask if it's white or black listed
		- Have SSIDs become white or black listed
	- Create a button that repeats the Shadow IT scan
		- Detect rouge access points 
			- Add these rogue access points to AR screen
			- when scans, checks if access points SSID matches any known rogue AP SSIDs.
			- If there are new ones, pop up the window
			- create a new prefab for those rouge access points
				- possibly create a larger radius
				- prefab should have no interference with scripts of wifi objects.
	- Create an AR Slider for Delete Radius Button to increase size

# Step 9 Plans
	- Create an Anchor point that the prefabs spawn locations are based
		- LOOK INTO ARCORE XR Plugin: https://docs.unity3d.com/Packages/com.unity.xr.arcore@4.1/manual/index.html
		- Have a button to drop an anchor 
		- Allow the user to grab ancor and adjust it 
			- This is incase the VR adjusts or resets user's position. 
		- Have these anchors be storeable
			- Have text editing for these prefabs
	- Add the following functions to Anchor
		- Delete Anchor button to delete all prefabs
		- Ability to move the anchor only on the X & Y axis
			- Make sure Rotation is locked to avoid slanted objects
		- A button to press to edit text
			- Store the anchor within user's data
	- Can anchors be inside of anchors? 
			
		
		
# Step ? Plans
	- Create Save button for mapping
	- Work on saving flat image of mapped area
		-First need to make the camera that can see everything
			- Make a camera renderer that can move with player
			- Have a small screen that user can see what the camera sees
			- Use render textures 
		- Second have what the camera sees save as an image
			- Make sure that the camera can see all objects
			- Make sure it works on multiple floors (possibly a angle view)
			- Save into temp file
		- third export to the headset as an image it can open
			- Build settings have access to downloading files
			- export as Jpeg conversion
			- Saves multiple names (possibly change name with text area?)
		
# Step ? Plans
	- Save objects layout when logging off or switching
		- first, find a way to save scene
			- Save position/direction that previous was looking
			- When in that position, recreate all objects
			- Reload save
		- Second, find a way to save all objects
			- Save all locations of (x,y,z) cords of objects
			- Save data of those prefabs 
		- When log off or shut down, reloading saves all locations 

# Step ? Plans
	- Add a button or UI to let user create their own cubes for info
		- The custom cube can hold info such as room number and etc. 
	- Have a delete button on the wifi pillars
	- Change splash screen 
	- If a new access point is created (BSSID), make a pillar to mark it.

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
	- APK 10: Shadow IT Scans + Anchor Point Saves
	
# Cybersecuirty Ideas

	- Packet Sniffing - Less possible
	- Historic Log Organization - Less possible

	- Simulate Network Traffic - possible
	- Simulate firewall - possible
	- Simulate Shadow IT - possible
	- Mac Adress Filtering - possible
	
	- Shadow IT function - possible
		- Rogue Access Point Detection - Very possible
		- Detect Hidden SSID networks - Very possible
		
# Some Sources

	-https://math.stackexchange.com/questions/2833778/converting-between-different-scales
	-https://www.crowdstrike.com/cybersecurity-101/cloud-security/shadow-it/
    -https://cai.io/resources/thought-leadership/shadow-IT-meaning-risks

	
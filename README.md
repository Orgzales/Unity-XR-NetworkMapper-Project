# Unity-Ar-Project
 My Senior Research Project for the year 2023 Fall- Spring 2024. It is going to use AR in the unity engine. 
 Some Commands:
	- netsh wlan show profiles
	- netsh wlan show interface

# Step 0 Plans
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
	- If a new access point is created (BSSID), make a pillar to mark it.
	- 🗸 Have it display the wifi scuirty to connected router.
	- 🗸 If authencation type is bad, give a alert to the cube being made.
		- 🗸 0 = Open or None = !!!!!DANGER!!!!! | Red
		- 🗸 1 = Wired Equivalent Privacy (WEP) = !!! HIGH ALERT !!! | Yellow
		- 🗸 2 = Wi-Fi Protected Access 2 (WPA2) = @ Secure @  | Green
		- 🗸 3 = Wi-Fi Protected Access 3 (WPA3) = #Protected# | Blue

# Step 4 Plans
	- 🗸 Have the Marked down cubes display information
		-  🗸 When cubed is touched, have it display previous data - Displays SSID BSSID and dBm when cube was created
		- Has data display in a AR-hud so position does not matter
				- add enough transparency 
	- Possibly show the range of the cube that was created wiht new prefab. 
	- Add a button or UI to let user create their own cubes for info
		- The custom cube can hold info such as room number and etc. 
	
# Step 5 Plans
	 - 🗸 Accident: Have program test if wifi/router has access to Better secuirty | (NEEDS MORE TESTING)
		- EX: Router is set to no security, but it still is able to check if it can have wpa2
		
		
		
# APK Versions
	- APK 1: Testing Mixed Reality 
	- APK 2: Getting SSID
	- APK 3: Getting SSID + BSSID
	- APK 4: Getting SSID + BSSID + RSSI/Signal
	- APK 5: Takes all 3 and makes prefabs base on location
	- APK 6: Added Auth/Secuirty Detection
	
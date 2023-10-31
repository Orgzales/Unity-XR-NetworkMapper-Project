# Unity-Ar-Project
 My Senior Research Project for the year 2023 Fall- Spring 2024. It is going to use AR in the unity engine. 
 Some Commands:
	- netsh wlan show profiles
	- netsh wlan show interface

# Step 0 Plans
	- Activate Passthrough mdoe / AR - Finished
	- Test Unity Webservices - Testing Process
	- Get Hand Tracking to operate - Finished
	- Get Developer Acess - RSA Finger Print Finished
	
# Step 1 Plans
	- Build APK Files for Wireless AR - FINISHED
	- WIFI Engines added for detecting nearby signals  - Quest 2 Shows SSID
	- Custom Marking Code that marks WIFI Strengths: - COMPLETED
		- No Signal = Red | -67dbm is Good signal (Max bars)
		- Weak Signal = Yellow | -68dbm to -79dbm is Okay signal (Half Bars)
		- Strong Signal = Green | -80dbm or lower is Very poor signal (Single or SOS bars)
		
# Step 2 Plans
	- Get Recording with passthrough enabled - OBS/Wireless Computer (Not possible on quest 2) Wired Recording
	- Have Flags or Tracking Color Floor based on signal
		- Find a way to create a cube in the exact spot that the wifi connection is updated - CUBES SPAWN
		- Create the cube's color based on the wifi connection 
		- If a new access point is created, make a pillar to mark it.
		- Have a radius possibly for the cubes to prevent to many being created
		- add enough transparency

# Step 3 Plans
	- Have the Marked down cubes display information
		- When cubed is touched, have it display previous data
		- Has data display in a AR-hud so position does not matter
	- Possibly show the range of the cube that was created. 
	- Add a button or UI to let user create their own cubes for info
		- The custom cube can hold info such as room number and etc. 
		
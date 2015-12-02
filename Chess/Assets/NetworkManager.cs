using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private float btnX, btnY, btnW, btnH;
	private string gameName = "edu.gcc.wb.NetworkPongV1Sophia";  // You need to change this for your game!
	private bool refreshingHostList = false;
	private bool hostDataFound = false;
	private HostData[] hostData;
	public GameObject player1;
	public GameObject player2;
	public GameObject puck;
	private bool puckInstantiated = false;
	GUIStyle customButtonStyle;
	public bool useAWSserver = false;
	public string AWS_URL;
	
	
	// Use this for initialization
	void Start ()
	{
		btnX = Screen.width * 0.01f;
		btnY = Screen.width * 0.01f;
		btnW = Screen.width * 0.15f;
		btnH = Screen.width * 0.01f;
		
		//Modify the Master server and facilitator attributes to connect to my locally
		//hosted server instead of the Unity HQ Master Server
		if (useAWSserver) {
			//Use the following four lines if using the class AWS Master Server
			MasterServer.ipAddress = AWS_URL; //"54.187.184.133"; //"10.37.101.31";
			MasterServer.port = 23466;
			Network.natFacilitatorIP = AWS_URL; //"54.187.184.133"; //"10.37.101.31";
			Network.natFacilitatorPort = 50005;
		}
	}
	
	//Host a server and register it to the master server
	void startServer ()
	{
		Network.InitializeServer (2, 25001, !Network.HavePublicAddress ());
		MasterServer.RegisterHost (gameName, "NetworkTestWB", "Testing  stuff");
	}
	
	//Get the list of servers from the Master Server
	void refreshHostList ()
	{
		MasterServer.RequestHostList (gameName);
		refreshingHostList = true;
		Debug.Log ("Getting Host List");
	}
	
	//Create a player that can be controlled by the user
	void spawnPlayer ()
	{
		GameObject foo;
		if (Network.isClient) {
			//foo = (Network.Instantiate(player2, new Vector2(3.75f,0),Quaternion.identity,0)) as GameObject;
		}		
		else {
			//foo =( Network.Instantiate (player1,new Vector2(-3.75f,0),Quaternion.identity,0))as GameObject;	
			//foo = (Network.Instantiate (puck, new Vector2(0,0),Quaternion.identity,0)) as GameObject;	
		}
	}
	
	//Messages
	void OnServerInitialized ()
	{
		Debug.Log ("Server initialized");
		spawnPlayer ();
	}
	
	void OnConnectedToServer ()
	{
		Debug.Log ("Connected to server");
		spawnPlayer ();
	}
	
	void OnMasterServerEvent (MasterServerEvent mse)
	{
		if (mse == MasterServerEvent.RegistrationSucceeded) {
			Debug.Log ("Server registered");
		}
	}
	
	//Update functions for GUI and Per-frame update
	void OnGUI ()
	{
		if (customButtonStyle == null) {
			customButtonStyle = new GUIStyle (GUI.skin.button);
			customButtonStyle.fontSize = 15;
		}
		if (!Network.isClient && !Network.isServer) {
			GUILayout.BeginArea (new Rect (Screen.width * .05f, Screen.height * .05f, Screen.width * 0.1f, Screen.height * 0.1f));
			if (GUILayout.Button ("Start Server", customButtonStyle)) {
				Debug.Log ("Starting Server");
				startServer (); 
			}
			
			if (GUILayout.Button ("Refresh Host List", customButtonStyle)) {
				Debug.Log ("Refreshing...");
				refreshHostList ();
			}
			
			if (hostDataFound) {
				Debug.Log ("Host data recieved");
				for (int i=0; i<hostData.Length; i++) {
					if (GUILayout.Button (hostData [i].gameName, customButtonStyle)) {
						Network.Connect (hostData [i]);
					}
				}
			}
			GUILayout.EndArea ();
		}
	}
	
	void Update ()
	{
		//If we have started to look for available servers, look every frame until we find one.
		if (refreshingHostList) {
			if (MasterServer.PollHostList ().Length > 0) {
				refreshingHostList = false;
				hostDataFound = true;
				hostData = MasterServer.PollHostList ();
			}
		}
	}
	
	
}




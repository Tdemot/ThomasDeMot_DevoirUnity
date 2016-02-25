using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	public Texture2D HUDBorder;

	//acces to script to catch health and armor of the player
	public CharacterSecondaryScript characterScript;

	public GUIStyle myGui;

	// Use this for initialization
	void Start () {
		myGui.fontSize = Screen.height / 9;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//display border of the HUD + dynamically display HP and amor of player
		//GUI.DrawTexture (new Rect (Screen.width/10.0f, Screen.height - HUDBorder.height - Screen.height/10.0f, Screen.width - 2.0f*Screen.width/10.0f, HUDBorder.height + Screen.height/10.0f ), HUDBorder, ScaleMode.StretchToFill);
		GUI.Label (new Rect (Screen.width/4.05f, Screen.height/1.17f , Screen.width - 2.0f*Screen.width/10.0f,  Screen.height/10.0f ), characterScript.healthPoint.ToString(), myGui);
		GUI.Label (new Rect (Screen.width/1.67f, Screen.height/1.17f , Screen.width - 2.0f*Screen.width/10.0f, Screen.height/10.0f ), characterScript.armorPoint.ToString(), myGui);
	}
}

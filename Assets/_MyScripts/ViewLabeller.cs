using UnityEngine;

using System.Collections;

public class ViewLabeller : MonoBehaviour {


	
	// Update is called once per frame
	void OnGUI () {
		//GUI.color = Color.yellow;
		//guiText.fontSize = 12;
        GUI.Label(new Rect(Screen.width/2-50,Screen.height - 20,100,20),"Main");
		GUI.Label(new Rect(2*Screen.width/3,Screen.height - 20,100,20), RoomNames);
	
	}
	public static string RoomNames {get;set;}
}

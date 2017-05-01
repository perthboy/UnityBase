using UnityEngine;
using System.Collections;

public class MenuText : MonoBehaviour
{
	public int selectedToolbar;
	public TextAsset infoHelp;
	public GUISkin HSL_skin;
	public Rect windowRect = new Rect (80, 80, 120, 80);
	
//	static int h =Screen.height/10;
//	static int w =Screen.width/10;
	public Rect HelpArea = new Rect (20, 150 ,420,300);

	// Use this for initialization
	void OnGUI ()
	{
		//GUI.color = Color.yellow;
		GUI.skin.button.wordWrap = true;

		GUI.skin = HSL_skin;

				
		//GUI.Label (new Rect ( 80, 40, 350, 20), "BUSSELTON HOSPITAL COMPLEX");
		
//		GUI.Label (new Rect (100, 240, 240, 20), "INSTRUCTIONS");
//		GUI.Label (new Rect (100, 270, 240, 20), "Use A. W, D, S keys or the arrow keys");
//		GUI.Label (new Rect (100, 290, 240, 20), "to move through the building");
//		GUI.Label (new Rect (100, 310, 240, 20), "Use the right mouse button to");
//		GUI.Label (new Rect (100, 330, 240, 20), "drive the camera");	
//		GUI.Label (new Rect (100, 350, 240, 20), "Turn the Plan Camera on or off");
//		GUI.Label (new Rect (100, 370, 240, 20), "with button in the bottom RH corner");
//		GUI.Label (new Rect (100, 390, 240, 20), "Select Building Area from the tabs at the top of the page");
//		GUI.Label (new Rect (100, 410, 240, 20), "Select Items with Middle Button or the I key");
//		GUI.Label (new Rect (100, 430, 240, 20), "Clear Label with the L key");
//		GUI.Label (new Rect (100, 450, 240, 20), "Rotate and object with the R key");
//		GUI.Label (new Rect (100, 470, 240, 20), "Reverse the rotation with the T key");
		GUI.TextArea(HelpArea,infoHelp.text);
		
		
	}
	
	
}

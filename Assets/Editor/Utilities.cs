using UnityEngine;
using UnityEditor;
using System.Collections;

// Shows the Assets menu when you right click on the contextRect Rectangle.
public class Utilities : MonoBehaviour
{

//class EditorUtilityDisplayPopupMenu extends EditorWindow
	void OnGUI ()
	{
			GUI.Label (new Rect (10, 45, 200, 20), "Time " );
		GUI.Label (new Rect (10, 155, 200, 20), "Time " );
	doPopUp();
	}
	
	void doPopUp()
	{
		Event evt = Event.current;
		Rect contectRect = new Rect(10,50,100,100);
		if( evt.type == EventType.ContextClick)
		{
			var mousePos = evt.mousePosition;
			if(contectRect.Contains(mousePos))
			{
				EditorUtility.DisplayPopupMenu(new Rect(mousePos.x,mousePos.y,0,0),"Assets/",null);
				evt.Use();
				
			}
		}
		
//				var evt = Event.current;
//		var contextRect = Rect (10, 10, 100, 100);
//		if (evt.type == EventType.ContextClick) {
//			var mousePos = evt.mousePosition;
//			if (contextRect.Contains (mousePos)) {
//				EditorUtility.DisplayPopupMenu (Rect (mousePos.x, mousePos.y, 0, 0), "Assets/", null);
//				evt.Use ();
//			}
//		}
	}
	
}





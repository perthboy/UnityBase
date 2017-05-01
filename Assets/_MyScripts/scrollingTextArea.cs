using UnityEngine;
using System.Collections;

public class scrollingTextArea : MonoBehaviour {
	string traceLog = "this is a message";
	int i;
public void LogDebug(string msg)
 {
    		Debug.Log(msg);
   		 	traceLog += msg + "\n\n";
			//yield return new WaitForSeconds(1);
    // setting the "y" value of scrollPosition puts the scrollbar at the bottom
    scrollPosition = new Vector2(scrollPosition.x, Mathf.Infinity);
 }
	void Update()
		
	{
	
		i++;
		LogDebug (" hello world: " +i);
	
	}
 private Vector2 scrollPosition;
 public void OnGUI()
 {
    // we want to place the TextArea in a particular location - use BeginArea and provide Rect
    GUILayout.BeginArea(new Rect(60, 15, 250, 250));
    scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width (250), GUILayout.Height (250));

    // We just add a single label to go inside the scroll view. Note how the
    // scrollbars will work correctly with wordwrap.
    GUILayout.Label (traceLog);

    // End the scrollview we began above.
    GUILayout.EndScrollView ();
    GUILayout.EndArea();
 }

}

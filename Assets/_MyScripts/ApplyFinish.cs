///Created 23/3/12 by Chris Hewer
///attach this script to the main building gameobject
///drag the individual element types into Elements to change
///drag materials into Materials and ensure that
///the names are in the matching sequence in the arrays


using UnityEngine;

using System.Collections;
using System;

public class ApplyFinish : MonoBehaviour
{
	//public string[] MaterialName;
	public Material[] ApplyMaterial;
	public GameObject[] ElementToChange;
	private int cnt = 0;

	private string tmp = null;
	void Awake ()
	{
		///how to have a dialogbox
		///to display a message
//		bool answer=EditorUtility.DisplayDialog("title","Do you want to modify the colors?","Ok","nooo");
//		if (!answer)
//			return;
//		GameObject go;
		int a = ApplyMaterial.Length;
		int b = ElementToChange.Length;
		if (a != b) {
			Debug.Log ("Number of materials to apply must equal number of elements");
			Debug.Log (transform.name);
			return;
			
		}
		if (ElementToChange.Length == 0) {
			return;
		}
		//remove element ID
		foreach (GameObject g in ElementToChange) {
			
			if (g.name.Contains ("[")) {
				//go = g;
				int i = g.name.IndexOf ("[");
				
				tmp = (g.name.Substring (0, i - 1));
				tmp = tmp.Trim ();
				
				g.name = tmp;
			}
			g.transform.GetComponent<Renderer>().material = ApplyMaterial[cnt];
			//Debug.Log ("Changing color to " + ApplyMaterial[cnt].ToString ());
			cnt++;
			
		}
		
		
		foreach (Transform t in transform) {
			cnt = 0;
			foreach (GameObject g in ElementToChange) {
				if (t.GetComponent<Renderer>().name.Contains (g.name)) {
					t.GetComponent<Renderer>().material = ApplyMaterial[cnt];
					//Debug.Log ("new  color to " + ApplyMaterial[cnt].ToString ());
					cnt++;
				}
			}
		}
		
	}
	
}


using UnityEngine;
using System.Collections;

public class GUIControl : MonoBehaviour {
	
	public  GameObject[] waypoints;
	public string[] wayNames;
	public static GUIControl control;

	// Use this for initialization
	void Start () {
		control = this;
		if (waypoints.Length >0)	
		{
			 wayNames= new string[waypoints.Length];
			for(int i=0;i < wayNames.Length;i++)
				wayNames[i]=waypoints[i].name;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

//Created 23/3/12 by Chris Hewer

//attach this script to the main empty light Gameobject



using UnityEngine;
using System.Collections;
using System;

public class LightController : MonoBehaviour
{
	public Light[] lights;
	public float RangeOfLight;
	public float SpotAngle;
	public float IntensityOfLight;
	private LightController instance;
	
	
	void Update ()
	{
		instance = this;
		
			lights = GetComponentsInChildren<Light>();
			foreach (Light g in lights) {
				g.GetComponent<Light>().range = RangeOfLight;
				g.GetComponent<Light>().intensity = IntensityOfLight;
			g.GetComponent<Light>().spotAngle = SpotAngle;

				Debug.Log ("Changing  Light settings");

				
			}
			
	
			
	}
	
}



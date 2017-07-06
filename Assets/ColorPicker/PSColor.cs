using UnityEngine;
using System.Collections;

public class PSColor : MonoBehaviour {

	public void OnSetColor(Color color)
	{
		GetComponent<ParticleSystem>().GetComponent<Renderer>().material.SetColor("_TintColor", color);
	}
	
	public void OnGetColor(ColorPicker picker)
	{
		picker.NotifyColor(GetComponent<ParticleSystem>().GetComponent<Renderer>().material.GetColor("_TintColor"));
	}
}

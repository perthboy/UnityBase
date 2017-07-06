using UnityEngine;
using System.Collections;
using ChangeTheColor;
public class ObjectColor : MonoBehaviour {
	//ChangeTheColor.ChangeColor  colr = new ChangeColor();
	void OnSetColor(Color color)
	{
		Material mt = new Material(GetComponent<Renderer>().sharedMaterial);
		mt.color = color;
		ChangeTheColor.UtilityClass.ChangeColor = color;
		Debug.Log (color.ToString ());
		GetComponent<Renderer>().material = mt;
	}

	void OnGetColor(ColorPicker picker)
	{
		picker.NotifyColor(GetComponent<Renderer>().material.color);
	}
}

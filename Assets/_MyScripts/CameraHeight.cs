using UnityEngine;
using System.Collections;

public class CameraHeight : MonoBehaviour {


	public Camera cam;
	public float currCamHeight = 2f;
	float vSliderValue;

	void Start () {
		moveCameraHeight ();
		vSliderValue = currCamHeight;
	}

	void Update()
	{
		moveCameraHeight ();
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (10, 10, 100, 20), "Cam Height");

		vSliderValue = GUI.VerticalSlider(new Rect(25, 30, 100, 100), vSliderValue, 12.0F, -5.0F);

		//currCamHeight = vSliderValue;
		Debug.Log ("Slider height:=" + vSliderValue );

	}

	void moveCameraHeight ()
	{
		float H = transform.localPosition.y;
		Vector3 temp = new Vector3 (0, vSliderValue, 0);
		//cam.transform.position = temp;
		Debug.Log ("height is " + H);
		transform.localPosition = temp;
		Debug.Log (transform.localPosition.y);
	}
}

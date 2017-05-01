using UnityEngine;
using System.Collections;

public class FlyOver : MonoBehaviour
{

	public float vSliderSpeed = 60f;
	public Transform target;
	public float adjustSpeed = 2;
//	private bool FlyMode = true;
	private string flyMode = "Fly";
	private bool IsPaused = false;
	public GameObject Cam;

	// Use this for initialization
	void OnAwake ()
	{
		
	}

	void Update ()
	{
		float t = vSliderSpeed;
		
		
		vSliderSpeed = t;
		transform.LookAt (target);
		
		//allow space bar to pause the fly over
		if (Input.GetButtonUp ("Jump")) {
			if (!IsPaused) {
				iTween.Pause ();
				IsPaused = true;
			} else {
				iTween.Resume ();
				IsPaused = false;
			}
		}
		
	}
	IEnumerator runFlyOver (float speed)
	{
		iTween.MoveTo (Cam, iTween.Hash ("path", iTweenPath.GetPath ("FlyOver"), "EaseType", "linear", "time", speed));
		yield return new WaitForSeconds (.01f);
		
	}
	void OnGUI ()
	{
		//GUI.color = Color.yellow;
		
		
		GUI.Label (new Rect (5, 45, 200, 20), "Time " + (int)vSliderSpeed);
		GUI.Label (new Rect (20, 65, 200, 20), "5");
		GUI.Label (new Rect (20, 185, 200, 20), "120");
		
		vSliderSpeed = GUI.VerticalSlider (new Rect (5, 65, 50, 130), vSliderSpeed, 5.0f, 120.0f);
		
		if (GUI.Button (new Rect (5, 25, 70, 20), flyMode)) {
			
			Fly ();
			
		}
		
	}
	void Fly ()
	{
		StartCoroutine (runFlyOver (vSliderSpeed));
	}
	
	
}

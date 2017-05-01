using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour
{
	public float timing;
	public float time_scale;
	public AudioClip metronomeSound;
	public int BPM = 60;


	public bool sound = false;

	public Light metronomeLight;
	private float Timing;

	// Use this for initialization
	void Awake ()
	{
		
		if (timing == 0)
			timing = 1;
	}

	// Update is called once per frame
	//FixedUpdate
	void FixedUpdate ()
	{
		
		if (timing > 0.0f && timing <= 3) {
			Time.timeScale = time_scale;
			Time.fixedDeltaTime = timing * Time.timeScale;
		}
		
		//Timing = 60 * Time.deltaTime;
		BPM = (int)Mathf.Ceil (60 * (1/Timing));
		Debug.Log ("BPM=" + Timing);
		//timing =  timing * Time.smoothDeltaTime;
		
		StartCoroutine (beat (timing * Time.deltaTime));
	}

	IEnumerator beat (float times)
	{
		getTime = !getTime;
		yield return new WaitForSeconds (times);
		metronomeLight.GetComponent<Light>().enabled = getTime;
		if (sound)
			transform.GetComponent<AudioSource>().PlayOneShot (metronomeSound);
		
		
	}

	bool getTime { get; set; }
	public void settime (float times)
	{
		
		timing = times;
		
	}

	void OnGUI ()
	{
		GUILayout.Label ("Time Scale: " + Time.timeScale.ToString (".##"));
		GUILayout.Label ("Timedelta: " + Time.deltaTime.ToString (".###"));
		//float bpm = 1f / 60 * Time.deltaTime;
		//GUILayout.Label ("BPM: " + bpm.ToString ());
		
		
		Timing = GUILayout.VerticalSlider (Timing, 3.0f, 0.1f);
		GUI.Label (new Rect (15, 170, 50, 20), BPM.ToString ());
		
		
	}
		/*	
	 GUILayout.Label ("Time Scale: " + Time.timeScale.ToString (".##"));
		float timeScale = Mathf.Log (Time.timeScale, 2);
		timeScale = GUILayout.HorizontalSlider (timeScale, -3f, 3f);
		if (timeScale > -0.1f && timeScale < 0.1f)
			timeScale = 0;
		Time.timeScale = Mathf.Pow (2, timeScale);
		
		*/		
	
	}


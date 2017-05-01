/// <summary>
/// attach this to the Plan Camera only
/// mmodified HEWCJ 18/7/12
/// </summary>

using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{

	//public GameObject target;
	//public GameObject arrow;
	
	public Camera mainCam;
	public Camera planCam;
	public float Distance = 5.0f;
	private bool Enabled { get; set; }
	
	private bool camSizeSmall = true;

	public bool CameraOn;
	
	private bool camEnabled { get; set; }


	private string viewAngle;
	private Transform myTransform;
	private Quaternion startAngles;


	public float vSliderValue = 0.5f;
	public float vSliderZoom ;

	//private float desiredDistance = 0f;
//private float distanceSmooth = 0f;
	//private float preOccludedDistance = 0f;
	void Awake ()
	{
		
		Enabled = CameraOn;
		planCam.enabled = camEnabled;
	}

	// Use this for initialization
	void Start ()
	{
		myTransform = transform;
		viewAngle = myTransform.gameObject.GetComponent<Camera>().fieldOfView.ToString ();
		startAngles = myTransform.localRotation;
		vSliderZoom = 60;// gameObject.camera.fieldOfView;
		planCam.enabled = CameraOn;
		smallPlan = planCam.rect;
		largePlan = new  Rect(0,0,Screen.width,Screen.height);
	}

	void LateUpdate ()
	{
		
		myTransform.position = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y , mainCam.transform.position.z);
		myTransform.rotation = startAngles;
//		if (vSliderValue > 0 && vSliderValue < Screen.width)
//			CameraResize (vSliderValue);
		//arrow.transform.position = new Vector3 (mainCam.transform.position.x, mainCam.transform.position.y + 0.55f, mainCam.transform.position.z);
		//arrow.transform.rotation = startAngles;
		CameraZoom (vSliderZoom);
	}

	void OnGUI ()
	{
		//GUI.color = Color.yellow;
		
//		GUI.Label(new Rect(5, 140, 100, 20), "Plan Size");
//		vSliderValue = GUI.VerticalSlider (new Rect (5, 5, 50, 130), vSliderValue, 1.0f, 0.0f);
		GUI.Label (new Rect (Screen.width - 120, 140, 100, 20), "Cam Angle");
		vSliderZoom = GUI.VerticalSlider (new Rect (Screen.width - 10, 50, 50, 130), vSliderZoom, 100.0f, 30.0f);
		
		viewAngle = GUI.TextField (new Rect (Screen.width - 35, 140, 25, 20), viewAngle);
		//turn plan camera on off
		if (GUI.Button (new Rect (Screen.width - 15, Screen.height - 15, 10, 10), "Plan")) {
			if (planCam.enabled == true)
				planCam.enabled = false;
			else
				planCam.enabled = true;	
		}
			///
			///resize plan view
			if (GUI.Button (new Rect (Screen.width - 40, Screen.height - 15, 10, 10), "Size")) {
			if (camSizeSmall== true){
				camSizeSmall = false;
				planCam.rect= smallPlan;
				}
			else{
				camSizeSmall= true;
				planCam.rect =largePlan;
				}
			
			
		}

	}
	/// <summary>
	/// Gets or sets the small plan.
	/// size to allow double clicking the plan view
	/// </value>
	Rect smallPlan {set;get;}
	Rect largePlan {set;get;}
	void CameraResize (float sValue)
	{
		
//		float pos = 0f;
		float pcx = planCam.rect.x;
		float pcy = planCam.rect.y;
//		float pWidth = planCam.rect.width;
//		float pHeight = planCam.rect.height;
		Rect newsize = new Rect (0, 0, 0, 0);
//		Rect newMainCamSize = new Rect (0, 0, 0, 0);
		newsize.x = pcx;
		newsize.y = pcy;
		newsize.width = sValue + .05f;
		newsize.height = sValue;
		planCam.rect = newsize;
		//Debug.Log( "cam size= " + newsize.width +"," + newsize.height);
//		pos = planCam.rect.width;
//
//		newMainCamSize.x=pos;
//		newMainCamSize.y=0;
//		newMainCamSize.width =  1- sValue;
//		newMainCamSize.height =  1- sValue ;
//		mainCam.rect =    newMainCamSize; 
		
	}
	void CameraZoom (float zValue)
	{
		mainCam.fieldOfView = zValue;
		viewAngle = zValue.ToString ();
	}

}
	


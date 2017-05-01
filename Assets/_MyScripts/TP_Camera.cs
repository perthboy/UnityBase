using UnityEngine;
using System.Collections;

//REV: I HAVE DONE THE WORK UPTO VIDEO S03_019 29/11/30
public class TP_Camera : MonoBehaviour
{
	public static TP_Camera Instance;
	public Transform TargetLookAt ;
	public float Distance = 5.0f;
	public float DistanceMin = 3.0f;
	public float DistanceMax = 10.0f;
	public float DistanceSmooth = 0.05f;
	public float DistanceResumeSmooth =1f;
	public float X_MouseSensitivity = 5f;
	public float Y_MouseSensitivity = 5f;
	public float MouseWheelSensitivity = 5f;
	public float X_Smooth = 0.05f;
	public float Y_Smooth = 0.1f;
	public float Y_MinLimit = -40f;
	public float Y_MaxLimit = 80f;
	public float OcclusionDistanceStep = 0.5f;
	public int MaxOcclusionChecks = 10;

	private float mouseX = 0f;
	private float mouseY = 0f;
	private float velX = 0;
	private float velY = 0;
	private float velZ = 0;
	private float velDistance = 0f;
	private float startDistance = 0f;
	private Vector3 position = Vector3.zero;
	private Vector3 desiredPosition = Vector3.zero;
	private float desiredDistance = 0f;
	private float distanceSmooth =0f;
	private float preOccludedDistance=0f;

	void Awake ()
	{
		Instance = this;
	}

	void Start ()
	{
		Distance = Mathf.Clamp (Distance, DistanceMin, DistanceMax);
		startDistance = Distance;
		Reset ();
	}

	void LateUpdate ()
	{
		if (TargetLookAt == null)
			return;
		HandlePlayerInput ();
		
		var count = 0;
		do
		{
			CalculateDesiredPosition();
				count ++;
		} while (CheckIfOccluded(count));	
	//	CheckCameraPoints (TargetLookAt.position, desiredPosition);  not wanted eventually
		UpdatePosition ();
	}


	void HandlePlayerInput ()
	{
		var deadZone = 0.001f;
		//RMB right mouse button
		if (Input.GetMouseButton (1)) {
			//The RMB is down, get mouse axis input
			mouseX += Input.GetAxis ("Mouse X") * X_MouseSensitivity;
			mouseY -= Input.GetAxis ("Mouse Y") * Y_MouseSensitivity;
			// note the inversion by subtraction -=
		}
		// This is where we limit mouseY rotation
		mouseY = Helper.ClampAngle (mouseY, Y_MinLimit, Y_MaxLimit);
		
		
		if (Input.GetAxis ("Mouse ScrollWheel") < -deadZone || Input.GetAxis ("Mouse ScrollWheel") > deadZone) {
			desiredDistance = Mathf.Clamp (Distance - Input.GetAxis ("Mouse ScrollWheel") * MouseWheelSensitivity, DistanceMin, DistanceMax);
			preOccludedDistance = Distance;
			distanceSmooth = DistanceSmooth;
		}
//		float t = 0f;
//		t = Input.GetAxis ("Mouse ScrollWheel");
		//Debug.Log ("Mouse ScrollWheel= " + t);
	}


	void CalculateDesiredPosition ()
	{
		//Evaluate Distance
		ResetDesiredDistance();
		
		Distance = Mathf.SmoothDamp (Distance, desiredDistance, ref velDistance, distanceSmooth);
		
		//Calculate desired position
		desiredPosition = CalculatePosition (mouseY, mouseX, Distance);
		//note reversals
	}



	Vector3 CalculatePosition (float rotationX, float rotationY, float distance)
	{
		Vector3 direction = new Vector3 (0, 0, -distance);
		//-ve distance points behind camera
		Quaternion rotation = Quaternion.Euler (rotationX, rotationY, 0);
		return TargetLookAt.position + (rotation * direction);
	}

		bool CheckIfOccluded (int count)
	{
		var isOccluded = false;
		var nearestDistance = CheckCameraPoints (TargetLookAt.position, desiredPosition);
		if (nearestDistance != -1) {
			if (count < MaxOcclusionChecks) 
			{
				isOccluded = true;
				Distance -= OcclusionDistanceStep;
				if (Distance < 0.8f)//.25 ORI test for shudder
					Distance = 0.8f;//.25 ORI
			}
			else
				Distance = nearestDistance - Camera.main.nearClipPlane;
			
			desiredDistance = Distance;
			distanceSmooth = DistanceResumeSmooth;
			}
		return isOccluded;
	}

	float CheckCameraPoints (Vector3 fromm, Vector3 to)
	{
		var nearestDistance = -1f;
		RaycastHit hitInfo;
		Helper.ClipPlanePoints clipPlanePoints = Helper.ClipPlaneAtNear (to);
		// Draw lines in the editor to make it easier to visual
		Debug.DrawLine (fromm, to + transform.forward * -GetComponent<Camera>().nearClipPlane, Color.red);
		//centreline
		Debug.DrawLine (fromm, clipPlanePoints.UpperLeft);
		Debug.DrawLine (fromm, clipPlanePoints.LowerLeft);
		Debug.DrawLine (fromm, clipPlanePoints.UpperRight);
		Debug.DrawLine (fromm, clipPlanePoints.LowerRight);
		//box it in
		Debug.DrawLine (clipPlanePoints.LowerLeft, clipPlanePoints.UpperLeft);
		Debug.DrawLine (clipPlanePoints.UpperLeft, clipPlanePoints.UpperRight);
		Debug.DrawLine (clipPlanePoints.UpperRight, clipPlanePoints.LowerRight);
		Debug.DrawLine (clipPlanePoints.LowerRight, clipPlanePoints.LowerLeft);
		//
		if (Physics.Linecast (fromm, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
		nearestDistance = hitInfo.distance;
		//
		if (Physics.Linecast (fromm, clipPlanePoints.UpperLeft, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearestDistance || nearestDistance == -1)
				nearestDistance = hitInfo.distance;
		
		if (Physics.Linecast (fromm, clipPlanePoints.LowerLeft, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearestDistance || nearestDistance == -1)
				nearestDistance = hitInfo.distance;
		//
		if (Physics.Linecast (fromm, clipPlanePoints.LowerRight, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearestDistance || nearestDistance == -1)
				nearestDistance = hitInfo.distance;
		
		if (Physics.Linecast (fromm, clipPlanePoints.UpperRight, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearestDistance || nearestDistance == -1)
				nearestDistance = hitInfo.distance;
		
		if (Physics.Linecast (fromm, to + transform.forward * -GetComponent<Camera>().nearClipPlane, out hitInfo) && hitInfo.collider.tag != "Player")
			if (hitInfo.distance < nearestDistance || nearestDistance == -1)
				nearestDistance = hitInfo.distance;
		return nearestDistance;
	}
	
	void ResetDesiredDistance()
	{
		if(desiredDistance < preOccludedDistance)
		{
			var pos= CalculatePosition(mouseY,mouseX,preOccludedDistance);
			
			var nearestDistance = CheckCameraPoints(TargetLookAt.position,pos);
				
			if(nearestDistance == -1 || nearestDistance > preOccludedDistance)
			{
				desiredDistance =preOccludedDistance;	
			}
		}
	}
	
	void UpdatePosition ()
	{
		var posX = Mathf.SmoothDamp (position.x, desiredPosition.x, ref velX, X_Smooth);
		var posY = Mathf.SmoothDamp (position.y, desiredPosition.y, ref velY, Y_Smooth);
		var posZ = Mathf.SmoothDamp (position.z, desiredPosition.z, ref velZ, X_Smooth);
		//X_Smooth is correct
		position = new Vector3 (posX, posY, posZ);
		transform.position = position;
		
		transform.LookAt (TargetLookAt);
		
	}

	public void Reset ()
	{
		mouseX = 0;
		mouseY = 10;
		Distance = startDistance;
		desiredDistance = Distance;
		preOccludedDistance =Distance;
	}
	public static void UseExistingOrCreateMainCamera ()
	{
		GameObject tempCamera;
		GameObject targetLookAt;
		TP_Camera myCamera;
		
		if (Camera.main != null) {
			tempCamera = Camera.main.gameObject;
			
		} else {
			tempCamera = new GameObject ("Main Camera");
			tempCamera.AddComponent <Camera>();
			tempCamera.tag = "MainCamera";
		
		
		tempCamera.AddComponent <TP_Camera>();
		}
		myCamera = tempCamera.GetComponent ("TP_Camera") as TP_Camera;
		
		targetLookAt = GameObject.Find ("targetLookAt") as GameObject;
		
		if (targetLookAt == null) {
			targetLookAt = new GameObject ("targetLookAt");
			targetLookAt.transform.position = Vector3.zero;
		}
		
		myCamera.TargetLookAt = targetLookAt.transform;
		
	}
}

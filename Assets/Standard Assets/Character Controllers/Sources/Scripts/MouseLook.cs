using UnityEngine;
using System.Collections;


/// MouseLook rotates the transform based on the mouse delta.
/// Minimum and Maximum values can be used to constrain the possible rotation

/// To make an FPS style character:
/// - Create a capsule.
/// - Add the MouseLook script to the capsule.
///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
/// - Add FPSInputController script to the capsule
///   -> A CharacterMotor and a CharacterController component will be automatically added.

/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
/// - Add a MouseLook script to the camera.
///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
[AddComponentMenu("Camera-Control/Mouse Look")]
public class MouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;
	
	public float deadZone = 20f;

	private float rotationY = 0F;
	//from TPCamera
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
//	private float velX = 0;
	//private float velY = 0;
//	private float velZ = 0;
	private float velDistance = 0f;
//	private float startDistance = 0f;
	private Vector3 position = Vector3.zero;	void ResetDesiredDistance()
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
	private Vector3 desiredPosition = Vector3.zero;
	private float desiredDistance = 0f;
	private float distanceSmooth =0f;
	private float preOccludedDistance=0f;
	
	//end of TPCAMERA

	void Update ()
	{
		if(Input.GetMouseButton(1)){ //1 for rh and 0 for lh
			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}		if (Input.GetAxis ("Mouse ScrollWheel") < -deadZone || Input.GetAxis ("Mouse ScrollWheel") > deadZone) {
			desiredDistance = Mathf.Clamp (Distance - Input.GetAxis ("Mouse ScrollWheel") * MouseWheelSensitivity, DistanceMin, DistanceMax);
		//	preOccludedDistance = Distance;
		//	distanceSmooth = DistanceSmooth;
		}
		
		
		
		
	}
	
	void Start ()
	{
		// Make the rigid body not change rotation
		if (GetComponent<Rigidbody>())
			GetComponent<Rigidbody>().freezeRotation = true;
	}	
	//from TPCamera
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
	
		float CheckCameraPoints (Vector3 fromm, Vector3 to)
	{
		var nearestDistance = -1f;
		RaycastHit hitInfo;
	
		ClipPlanePoints clipPlanePoints = ClipPlaneAtNear (to);
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
	
	
		public struct ClipPlanePoints
	{
		public Vector3 UpperLeft;
		public Vector3 UpperRight;
		public Vector3 LowerLeft;
		public Vector3 LowerRight;
	}
	public static float ClampAngle (float angle, float min, float max)
	{
		do {
			if (angle < -360)
				angle += 360;
			if (angle > 360)
				angle -= 360;
		} while (angle < -360 || angle > 360);
		
		return Mathf.Clamp (angle, min, max);
	}
	public static ClipPlanePoints ClipPlaneAtNear (Vector3 pos)
	{
		var clipPlanePoints = new ClipPlanePoints ();
		if (Camera.main == null)
			return clipPlanePoints;
		var transform = Camera.main.transform;
		var halfFOV = (Camera.main.fieldOfView / 2) * Mathf.Deg2Rad;
		var aspect = Camera.main.aspect;
		var distance = Camera.main.nearClipPlane;
		var height = distance * Mathf.Tan (halfFOV);
		var width = height * aspect;
		//point 1
		clipPlanePoints.LowerRight = pos + transform.right * width;
		clipPlanePoints.LowerRight -= transform.up * height;
		//actually moving down
		clipPlanePoints.LowerRight += transform.forward * distance;
		
		//point 2
		clipPlanePoints.LowerLeft = pos - transform.right * width;
		clipPlanePoints.LowerLeft -= transform.up * height;
		//actually moving down
		clipPlanePoints.LowerLeft += transform.forward * distance;
		
		//point 3
		clipPlanePoints.UpperRight = pos + transform.right * width;
		clipPlanePoints.UpperRight += transform.up * height;
		//actually moving down
		clipPlanePoints.UpperRight += transform.forward * distance;
		
		//point 4
		clipPlanePoints.UpperLeft = pos - transform.right * width;
		clipPlanePoints.UpperLeft += transform.up * height;
		//actually moving down
		clipPlanePoints.UpperLeft += transform.forward * distance;
		
		return clipPlanePoints;
	}
	
}
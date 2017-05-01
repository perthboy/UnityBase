using UnityEngine;
using System.Collections;
//checked 17/11/2011 as copied ok
//rev updated from video
public class TP_Motor : MonoBehaviour
{

	public static TP_Motor Instance;
	public float MoveSpeed = 10.0f;
	public float JumpSpeed = 6f;
	public float Gravity = 21f;
	public float TerminalVelocity = 20f;
	public float SlideThreshold = 0.6f;
	public float MaxControllableSlideMagnitude = 0.4f;
	public Vector3 MoveVector { get; set; }
	public float VerticalVelocity { get; set; }

	private Vector3 slideDirection;

	void Awake ()
	{
		Instance = this;
	}


	public void UpdateMotor ()
	{
		SnapAlighCharacterWithCamera ();
		ProcessMotion ();
	}
	void ProcessMotion ()
	{
		//Transform MoveVector to Worldspace
		MoveVector = transform.TransformDirection(MoveVector);
		
		//Normalise MoveVector if Magnitude >1
		if (MoveVector.magnitude > 1)
			MoveVector = Vector3.Normalize(MoveVector);
		// apply sliding if applicable
		ApplySlide ();
		//Multiply MoveVector by MoveSpeed
		MoveVector *= MoveSpeed;
		
		//Multiply MoveVector by DeltaTime		NOT NEEDED WHEN GRAVITY APPLIED
		//MoveVector *= Time.deltaTime;
		
		//Reapply VerticalVelocity MoveVector.y
		MoveVector = new Vector3 (MoveVector.x, VerticalVelocity, MoveVector.z);
		
		//Apply gravity
		ApplyGravity ();
		//Move the Character in world space
		TP_Controller.CharacterController.Move (MoveVector * Time.deltaTime);
	}
	void SnapAlighCharacterWithCamera ()
	{
		if (MoveVector.x != 0 || MoveVector.z != 0)
			transform.rotation = Quaternion.Euler (transform.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.eulerAngles.z);
	}

	void ApplyGravity ()
	{
		if (MoveVector.y > -TerminalVelocity)
			MoveVector = new Vector3 (MoveVector.x, MoveVector.y - Gravity * Time.deltaTime, MoveVector.z);
		
		if (TP_Controller.CharacterController.isGrounded && MoveVector.y < -1)
			MoveVector = new Vector3 (MoveVector.x, -1, MoveVector.z);
		
	}
	void ApplySlide ()
	{
		if (!TP_Controller.CharacterController.isGrounded)
			//note NOT
			return;
		slideDirection = Vector3.zero;
		RaycastHit hitInfo;
		
		if (Physics.Raycast (transform.position + Vector3.up, Vector3.down, out hitInfo)) {
			if (hitInfo.normal.y < SlideThreshold)
				slideDirection = new Vector3 (hitInfo.normal.x, -hitInfo.normal.y, hitInfo.normal.z);
		}
		if (slideDirection.magnitude < MaxControllableSlideMagnitude)
			MoveVector += slideDirection;
		else
			MoveVector = slideDirection;
		
	}
	public void Jump ()
	{
		if (TP_Controller.CharacterController.isGrounded)
			VerticalVelocity = JumpSpeed;
	}
}

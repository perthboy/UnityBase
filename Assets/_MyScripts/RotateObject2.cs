using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {
		public float TravelDistance = 0.05f;
		public float speedOfRotation = 5f;
	
	
		private float left;
		private float right;
		private float amtToMove;
	
	
	// Use this for initialization
	void Start () {
			
		left = TravelDistance;
		right = -TravelDistance;
	}
	
	// Update is called once per frame
	void Update () {
		
		if  (Input.GetMouseButtonDown(2))
		{
			Rotate();
			
		}
	}
	
	
	public void Rotate()
	{
		amtToMove = speedOfRotation * Time.deltaTime;
		transform.Rotate(0,0,speedOfRotation* Time.deltaTime);
	}
}

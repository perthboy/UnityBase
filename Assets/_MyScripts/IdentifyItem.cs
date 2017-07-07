using UnityEngine;
using System.Collections;
using System;

using UnityEngine.SceneManagement;

using createMaterials;
using ChangeTheColor;

[RequireComponent(typeof(MeshCollider))]

//Attach this script to the camera
//set guiskin
//In Project Settings add Identify and ClearLabel to Input with I and L respectively
public class IdentifyItem : MonoBehaviour
{
//public Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);   
//		RaycastHit hit;
	// Use this for initialization

	public Rect WindowRect = new Rect (60, 20, 420, 30);
	public float RotationDegrees = 5;
	public float distanceFrom = 65.0f;
	public float speed = 1;
	//private RaycastHit hit;
	public GUISkin guiSkin;
	public bool doWindow0 = false;
	public GameObject DaisPrefab;
	public float X_MouseSensitivity = 5f;
	public float Y_MouseSensitivity = 5f;
	public int moveLayer = 27;
	private bool labelOn = false;
	private GameObject go;
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 scanPos;
	private Ray ray = new Ray ();
	private bool isPrinted = false;
	//GameObject read = Document_IO.readFile("NewObjectPosition.txt");
	public Color newColor = Color.green;

	//used to change element colors
	//ChangeTheColor.ChangeColor  newcolor= new ChangeColor();


	void Awake ()
	{
		labelOn = false;
		ChangeTheColor.extractColorFromString.readColorFile ("changedColour.txt");
	}
	void Update ()
	{
		IdentifyObject ();
		if (Input.GetButtonUp ("ClearLabel")) {
			ClickedObject = "";
			labelOn = !labelOn;
		}
		
		if (Input.GetMouseButtonDown (0) && Input.GetKey (KeyCode.LeftShift))
			Rotate (RotationDegrees);
		
		if (Input.GetMouseButtonDown (0) && Input.GetKey (KeyCode.RightShift))
			Rotate (-RotationDegrees);
		
		
		
		if (Input.GetKey (KeyCode.Z)) {
			MoveObject (-1, 1);
		}
		if (Input.GetKey (KeyCode.X)) {
			MoveObject (1, 1);
		}
		if (Input.GetKey (KeyCode.C)) {
			MoveObject (-1, 0);
		}
		if (Input.GetKey (KeyCode.V)) {
			MoveObject (1, 0);
		}
		
		///
		///Write the current GO from Ray to the file
		if (Input.GetKey (KeyCode.T)) {
			if (!isPrinted)
				writeGOtoFILE ("NewObjectPosition.txt", true);
		}
		
	}

	void writeGOtoFILE (string Filename, bool newline)
	{
		string tmp = "";
		
		switch (Filename) {
		case "NewObjectPosition.txt":
			Vector3 lines = go.transform.parent.localPosition;
			tmp = go.transform.parent.name + ",";
			tmp = tmp + lines.ToString () + ",";
			tmp = tmp + go.transform.parent.localRotation;
			break;
		case "changedColour.txt":
			//string str = hit.collider.gameObject.name;
			//tmp = go.transform.parent.name + ",";
			//tmp = tmp + str.ToString () + ",";

			;break;
		}
		
		if (!isPrinted) {
			Document_IO.writeFile (Filename, tmp, newline);
			//isPrinted = true;
		}
	}
	void writeGOtoFILE (string Filename, string content, bool newline)
	{
		string tmp = "";

		switch (Filename) {
		case "NewObjectPosition.txt":
			Vector3 lines = go.transform.parent.localPosition;
			tmp = go.transform.parent.name + ",";
			tmp = tmp + lines.ToString () + ",";
			tmp = tmp + go.transform.parent.localRotation;
			break;
		case "changedColour.txt":
			//string str = hit.collider.gameObject.name;
			//tmp = go.transform.parent.name + ",";
			//tmp = tmp + str.ToString () + ",";
			tmp=content;
			;break;
		}

		if (!isPrinted) {
			Document_IO.writeFile (Filename, tmp,newline);
			//isPrinted = true;
		}
	}
	void OnGUI ()
	{
		GUI.skin = guiSkin;
		if (labelOn && ClickedObject.Length > 0)
			GUI.Label (new Rect (Input.mousePosition.x, (Screen.height - Input.mousePosition.y + 20), 200, 80), ClickedObject);
		//Debug.Log (ClickedObject);
//		if (GUI.Button (new Rect (20, 20, 100, 20), "Re-position moved Objects")) {
//			Document_IO.readFile("NewObjectPosition.txt");
//			
//		}
	}

	private string ClickedObject { get; set; }

	void MoveObject (int direction, int button)
	{
		//go has been destroyed, used middle click to to turn on
		if (go == null)
			return;
		
		float moveForward = speed * Time.deltaTime * direction;
		float moveLeft = speed * Time.deltaTime * direction;
		
		Transform parent;
		parent = go.transform.parent;
		if (button == 0)
			parent.Translate (Vector3.up * moveForward); 
		else if (button == 1)
			parent.Translate (Vector3.left * moveLeft);
		
		isPrinted = false;
	}

	void IdentifyObject ()
	{
		RaycastHit hit = new RaycastHit ();
		
		ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		float tiltAroundX = Input.GetAxis ("Vertical");
		float tiltAroundZ = Input.GetAxis ("Horizontal");
		
		
		//Returns true during the frame the user touches the object
		if (Input.GetButtonUp ("Identify")) 
		{
			
			if (Physics.Raycast (ray, out hit)) {
				float lockPos = 0f;
				//show name of hit item
				//Debug.Log (hit.collider.gameObject.name);
				///identify the object
				/// 
				string tmpName = hit.collider.gameObject.name;

				Renderer rend =hit.collider.gameObject.GetComponent(typeof (Renderer)) as Renderer;
				//GameObject CP = GameObject.Find("ColorPickerPrefab");;

				//GameObject Pic  = Instantiate ( CP, hit.point, Quaternion.LookRotation (hit.normal)) as GameObject;

				string colr =  rend.material.color.ToString();
				writeGOtoFILE ("changedColour.txt",hit.transform.ToString() + " old color= " + colr.ToString(), true);
				Material nMaterial = new Material(Shader.Find("Standard"));


				//try this class to get color from colorpicker
				nMaterial.color = ChangeTheColor.UtilityClass.ChangeColor ;


				GetComponent<Renderer>().material = nMaterial;
				rend.sharedMaterial = nMaterial;
				isPrinted = false;
				writeGOtoFILE ("changedColour.txt",hit.transform.ToString() + " new color = " + nMaterial.color.ToString(), true);
					//TODO: save a new material to a different name
					//createMaterials.CreateMaterials.CreateMaterial(nMaterial);



				//revised name
				string[] newname = tmpName.Split('[');

				ClickedObject = newname[0]; //hit.collider.gameObject.name;
				///check for object layer
				if (hit.collider.gameObject.layer == moveLayer) {
					///get the prefab and attach to object
					///get the right transform location and rotation;
					///check if item already has Dais prefab, if so Destroy
					if (hit.collider.transform.childCount > 0) {
						foreach (Transform child in hit.collider.transform) {
							if (child.name == "Dais")
								Destroy (child.gameObject);
							return;
						}
					}
					go = Instantiate (DaisPrefab, hit.point, Quaternion.LookRotation (hit.normal)) as GameObject;
					Vector3 colGO = new Vector3 (hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z);
					go.transform.position = colGO;
					
					//go.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,lockPos,lockPos);
					//Quaternion target = Quaternion.Euler (tiltAroundX, 0, tiltAroundZ);
					go.transform.rotation = Quaternion.Euler (tiltAroundX, lockPos, tiltAroundZ);
					go.name = "Dais";
					go.transform.parent = hit.collider.transform;
					//	go.AddComponent("RotateObject");
					//	go.transform.parent.gameObject.AddComponent("RotateObject");
					
					return;
				}
			}
		}
		return;
		
	}

	public void Rotate (float amount)
	{
		go.transform.parent.Rotate (0, 0, amount);
		isPrinted = false;
	}
	
	public static void relocateObject(string[] newPosition)
		
	{
		
		float x, y ,z;
		x = Convert.ToSingle(newPosition[1]);
		y = Convert.ToSingle(newPosition[2]);
		z = Convert.ToSingle(newPosition[3]);
		GameObject go = GameObject.Find(newPosition[0]);
	
		Vector3 newPos = new Vector3(x,y,z);
		go.transform.position = newPos;
		
	}



	void resetScene()
	{
		Scene currScene = SceneManager.GetActiveScene();
		string sceneName =  currScene.name;
		SceneManager.LoadSceneAsync(sceneName);
		Debug.Log("Scene Reset");
	}

}

using UnityEngine;
using UnityEditor;


public class Refresh : Editor {

	void OnSceneGUI  () {
	SceneView.RepaintAll();
	}
	

}

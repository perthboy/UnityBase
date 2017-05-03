using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof (GUITexture))]
public class ForcedReset : MonoBehaviour
{
	
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
			Scene currScene = SceneManager.GetActiveScene();
			string sceneName =  currScene.name;
			SceneManager.LoadSceneAsync(sceneName);
			Debug.Log("Scene Reset");
			//(sceneName, SceneManagement.LoadSceneMode mode = LoadSceneMode.Single);

        }
    }
}

using UnityEngine;
using UnityEditor;

namespace createMaterials
{
	public class CreateMaterials : MonoBehaviour
	{

		static public int counter 
		{
			get;
			set;
		}
		[MenuItem ("GameObject/Create Material")]


		public static void CreateMaterial (Material newMat, string name)
		{
			// Create a simple material asset
			counter ++;
			//Material material = new Material (Shader.Find ("Specular"));
			AssetDatabase.CreateAsset (newMat, "Assets/MyMaterials " + name + ".mat");
			string path =  "Assets/" + newMat.ToString () + ".mat";
			// Print the path of the created asset
			Debug.Log (AssetDatabase.GetAssetPath (newMat));
			Debug.Log ("...the new material is " + path);
		}
	}
}
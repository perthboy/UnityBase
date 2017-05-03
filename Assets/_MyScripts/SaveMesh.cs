using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveMesh : MonoBehaviour 
{

	//You can use that mesh serializer http://wiki.unity3d.com/index.php?title=MeshSerializer2

	public static void CacheItem(string url, Mesh mesh)
	{
/*		string path = Path.Combine(Application.persistentDataPath, url);
		byte [] bytes = MeshSerializer.WriteMesh(mesh, true);
		File.WriteAllBytes(path, bytes);*7
	}
	//It won't save into the Asset folder since this one does not exist anymore at runtime. You would most likely save it to the persistent data path which is meant to store data actually.

	//Then you can retrieve it just going the other way around:

	public static Mesh GetCacheItem(string url)
	{
		/*string path = Path.Combine(Application.persistentDataPath, url);
		if(File.Exists(path) == true)
		{
			byte [] bytes = File.ReadAllBytes(path);
			return MeshSerialize.ReadMesh(bytes);
		}
		return null;
	}
	public static void CacheMaterial(string url, Material mat)
	{
		string path = Path.Combine(Application.persistentDataPath, url);
		byte [] bytes = CacheMaterial(url,mat);
		File.WriteAllBytes(path, bytes);*/
	}

}

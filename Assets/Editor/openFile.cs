using UnityEditor;
using UnityEngine;


// Opens a file selection dialog for a PNG file and overwrites any
// selected texture with the contents.

public class openFile
{

	public static string GetRoomNamesFile ()
	{
		
		var path = EditorUtility.OpenFilePanel ("Open text file with room names", "", "txt");
		if (path.Length != 0) {
			Debug.Log (path);
			return path;
		}
		else
			return null;
	}
}


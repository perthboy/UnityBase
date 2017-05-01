using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;

public class Document_IO : MonoBehaviour
{
	
		void Awake ()
	{
		//readFile("NewObjectPosition.txt");
	}
	public static void writeFile (string Filename, string lines, bool newline)
	{
		//System.IO.StreamWriter file = new System.IO.StreamWriter (@"C:\temp\xmlOutput.txt", true);
		string path;
		path= @"C:\temp\" + Filename;
		System.IO.StreamWriter filename = new System.IO.StreamWriter (path, true);
		if (newline)
			filename.WriteLine (lines);
		else
			filename.Write (lines);
		filename.Close ();
	}

	public static void readFile (string Filename)
	{
		string path;
		path= @"C:\temp\" + Filename;
		
		using (StreamReader sr = new StreamReader (path)) {
			string line;
			// Read and display lines from the file until the end of 
			// the file is reached.
			
			while ((line = sr.ReadLine ()) != null) {
				Debug.Log(line);
				line =  line.Replace("(","");
				line =  line.Replace(")","");
				string[] tmp = line.Split(',');		
				IdentifyItem.relocateObject(tmp);

			}
		}

	}
}

using UnityEngine;
using System.Collections;

public static class Helper_general

{
	
	public static void Main()

	{
		getArray();
	}
	
	
	public static string[] getArray()
	{
		//var url = "file://M:/Unity/abc_notation.txt";
		int c = 0;
		string[] str = new string[100];
		str = getFileAsString();

		//string[] words = str.Split (',');
		foreach (string word in str) {
			c += 1;
			Debug.Log (word + c);
		}

		return str;
		
	}
	
	public static string[] getFileAsString()
	{	
//		string line;
		string[] lines = System.IO.File.ReadAllLines (Application.dataPath + "/abc_notation.txt");
		Debug.Log("contents = " + lines.Length);
		//line=lines[1];
		return lines;
		
		/* from example
		    string[] lines = System.IO.File.ReadAllLines(@"C:\t1");
            Console.Out.WriteLine("contents = " + lines.Length);
            Console.In.ReadLine();
		 * */
	}
}

using UnityEngine;
using System.Collections;
using System.IO;


namespace ChangeTheColor
{



	#region Set Color from Color Picker

	public class UtilityClass
	{
		static public Color ChangeColor {
			get;
			set;
		}

	}

#endregion
	public class  extractColorFromString  
	{
		public Color32 color32 = new Color32(64, 128, 192, 255);

		public static void readColorFile (string Filename)
		{
			string path;
			path= @"/Users/VDC/Documents/tmp/" + Filename;

			//Basic Wall Double brick - 230 Rendered [203943] (UnityEngine.Transform) old color= RGBA(1.000, 1.000, 1.000, 1.000)

			using (StreamReader sr = new StreamReader (path)) {
				string line;
				string element;
				string colr;
				string nextbit;
				string[] last;
				string ID;


				while ((line = sr.ReadLine ()) != null) {
					Debug.Log(line);
					if (line.Contains("new"))
						{
						string[] newline = line.Split ('[');
						element = newline[0];

						nextbit = newline[1];

						last = nextbit.Split(']');
						ID = last[0];
						colr = last [1];
						int i = colr.IndexOf ('=');
						colr = colr.Substring (i+1).Trim();
						Debug.Log (element + "   " + ID + "   " + colr);
						}
					

				}
			}

		}
	}

}


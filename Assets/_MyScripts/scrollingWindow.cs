using UnityEngine;
using System.Collections;

public class scrollingWindow
: MonoBehaviour {

	public Vector2 scrollPosition;
	public float blx;
	public float brx;
	public float bty;
	public float bby;
	public float bw;
	public float bh;
	public float sx;
	public float sw;
	public float sy;
	public float sh;
	public float saw;
	public float sah;
void OnGUI() {
scrollPosition = GUI.BeginScrollView(new Rect(10, 10, saw, sah), scrollPosition, new Rect(0, 0, saw, sah));
GUI.Button(new Rect(blx, bty, 100, 20), "Top-left");
GUI.Button(new Rect(brx, bty, 100, 20), "Top-right");
GUI.Button(new Rect(blx, bby, 100, 20), "Bottom-left");
GUI.Button(new Rect(brx, bby, 100, 20), "Bottom-right");
GUI.EndScrollView();
}
}



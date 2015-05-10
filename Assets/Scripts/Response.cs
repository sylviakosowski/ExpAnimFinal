using UnityEngine;
using System.Collections;

public class Response : MonoBehaviour {

	// Variables for the responses
	private GameObject globObj;
	private Globals globScr;
	private string resp;

	// Variables for drawing
	private int cX;
	private int cY;
	
	private ArrayList rects;

	// Use this for initialization
	void Start () {
		cX = Screen.width / 2;
		cY = Screen.height / 2;
		
		globObj = GameObject.Find ("GlobalObj");
		globScr = globObj.GetComponent<Globals> ();
		resp = globScr.getGirlResp ();
		
		rects = new ArrayList ();
		rects.Add (new Rect (cX - 400, cY - 60, 300, 120)); // Where the girl will talk
		rects.Add (new Rect (cX - 150, cY + 70, 300, 60)); // Where you will click to continue

	}

	void OnGUI () {
		GUI.skin.button.wordWrap = true;
		GUI.skin.box.wordWrap = true;
		GUI.Box ((Rect)rects [0], resp);
		if (GUI.Button ((Rect)rects [1], "[Continue]")) {
			Application.LoadLevel ("RTC");
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

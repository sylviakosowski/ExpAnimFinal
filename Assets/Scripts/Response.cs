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
			// Update the girl's response to be the new one. Clear the second one just in case.
			// This is use for when the girl talks, then memory, then girl talks again.

			//globScr.updateGirlResp2(globScr.getGirlResp2(), "");
			globScr.updateGirlResp2(globScr.getGirlResp2(), globScr.getGirlResp2());

			// We want to consume the return spot put in here and reset it to be the normal conversation so that we don't forget to reset it.
			// This actually easily allows us to go from girl to memory to girl, since after we visit the girl we will go to the memory, but
			// by going to the memory we clear out the return point so we automatically return to the RTC scene.
			string ret = Globals.girlRet;
			Globals.girlRet = "RTC";
			if (!ret.Equals("RTC")) { // TODO: REMOVE THIS CRAP WHEN YOU ACTUALLY HAVE THE MEMORIES
				//Application.LoadLevel (ret);
				Application.LoadLevel ("opening");
			} else {
				Application.LoadLevel ("RTC");
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}

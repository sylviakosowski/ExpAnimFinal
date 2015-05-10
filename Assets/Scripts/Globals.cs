using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static int anger;
	public static ArrayList memories;

	public void updateAnger (int change) {
		anger = anger + change;
		if (anger < 0) {
			anger = 0;
		} else if (anger > 10) {
			anger = 10;
		}
		Debug.Log (anger.ToString ());
	}

	// Use this for initialization
	void Start () {
		anger = 5;
		memories = new ArrayList (10);
	}
}

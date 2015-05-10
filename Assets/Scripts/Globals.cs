using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static int anger;
	public static int angerThresh;
	public static ArrayList memories;
	public static int convoNumber;
	public static int convoStage;

	public static int conv10Angry;

	public void updateAnger (int change) {
		anger = anger + change;
		if (anger < 0) {
			anger = 0;
		} else if (anger > 10) {
			anger = 10;
		}
		Debug.LogFormat ("Anger: {0}", anger);
	}

	void Awake () {
		DontDestroyOnLoad (gameObject);
		anger = 5;
		angerThresh = 7;
		memories = new ArrayList (10);
		for (int i = 0; i < 10; ++i) {
			memories.Add (false);
		}
		convoNumber = 0;
		convoStage = 0;

		conv10Angry = 0;

		Debug.Log ("Awake was called in global");
	}

	// Use this for initialization
	void Start () {
		Application.LoadLevel ("opening");
	}
}

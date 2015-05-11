using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static int anger;
	public static int angerThresh;
	public static ArrayList memories;
	public static int convoNumber;
	public static int convoStage;
	private static int oldNumber;
	private static int oldStage;

	public static string girlResp;
	public static string girlResp2; // Needed for when the girl talks, we have a memory, and then the girl talks again.

	public static string girlRet;
	public static string memRet;

	public static bool conv10Angry;

	public void resetConvo () {
		convoNumber = oldNumber;
		convoStage = oldStage;
	}

	public void updateConvo (int number, int stage) {
		oldNumber = convoNumber;
		oldStage = convoStage;
		convoNumber = number;
		convoStage = stage;
	}

	public void updateAnger (int change) {
		anger = anger + change;
		if (anger < 0) {
			anger = 0;
		} else if (anger > 10) {
			anger = 10;
		}
		//Debug.LogFormat ("Anger: {0}", anger);
	}
	
	public void updateGirlResp (string resp) {
		girlResp = resp;
		Debug.LogFormat ("Girl: {0}", resp);
	}

	public void updateGirlResp2 (string resp1, string resp2) {
		girlResp = resp1;
		girlResp2 = resp2;
	}
	
	public string getGirlResp () {
		return girlResp;
	}

	public string getGirlResp2 () {
		return girlResp2;
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

		girlResp = "";

		girlRet = "RTC";
		memRet = "RTC";

		conv10Angry = false;
	}

	// Use this for initialization
	void Start () {
		Application.LoadLevel ("opening");
	}
}

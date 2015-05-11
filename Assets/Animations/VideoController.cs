using UnityEngine;
using System.Collections;

public class VideoController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer> ();
		MovieTexture movie = (MovieTexture)rend.material.mainTexture;
		movie.Play ();
		GetComponent<AudioSource>().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		Renderer rend = GetComponent<Renderer> ();
		MovieTexture movie = (MovieTexture)rend.material.mainTexture;
		if (Input.GetKeyDown (KeyCode.Space)) {
			movie.Stop();
			string ret = Globals.memRet;
			Debug.LogFormat ("Return: {0}", ret);
			Globals.memRet = "RTC";
			Application.LoadLevel(ret);
		} else if (!movie.isPlaying) {
			movie.Stop();
			string ret = Globals.memRet; // Consume the return location so that it returns to default behavior, just incase.
			Globals.memRet = "RTC";
			Application.LoadLevel(ret);
		}
	}
}

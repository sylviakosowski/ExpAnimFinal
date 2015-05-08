using UnityEngine;
using System.Collections;

public class VideoController : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		Renderer rend = GetComponent<Renderer> ();
		MovieTexture movie = (MovieTexture)rend.material.mainTexture;
		movie.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		Renderer rend = GetComponent<Renderer> ();
		MovieTexture movie = (MovieTexture)rend.material.mainTexture;
		if (!movie.isPlaying) {
			movie.Stop();
			Debug.Log("Video done. Change Scene.");
			//Application.LoadLevel("DemoScene");
		}
	}
}

﻿using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Application.Quit ();
		}
	}
}

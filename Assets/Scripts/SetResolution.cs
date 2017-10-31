using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetResolution : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Screen.SetResolution(1024, 720, false);
	}
	
}

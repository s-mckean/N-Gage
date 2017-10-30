using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalVectors : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
		Debug.Log("UP: " + transform.up);
		Debug.Log("For: " + transform.forward);
		Debug.Log("Right: " + transform.right);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaFireball : MonoBehaviour {

	public GameObject explosionFab;

	bool isActive = false;
	
	Vector3 speed;


	// Use this for initialization
	public void Inialized(Vector3 speed) {
		this.speed = speed;
	}

	public void Fire() {
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive) {
			transform.position += speed;
		}
	}

	void OnTriggerEnter(Collider other) {

	}
}

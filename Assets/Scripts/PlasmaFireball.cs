using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaFireball : MonoBehaviour {

	public GameObject explosionFab;

	bool isActive = false;
	
	Vector3 velocity;
	float scalar = 2.0f;
	
	public void Fire(Transform playerTransform) {
		velocity = (playerTransform.position - transform.position).normalized * scalar;
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive) {
			transform.position += velocity;
		}
	}

	void OnTriggerEnter(Collider other) {
		Instantiate(explosionFab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaFireball : MonoBehaviour {

	public GameObject explosionFab;

	bool isActive = false;
	
	Vector3 velocity;
	float scalar = 12.0f;

	float timeToLive = 3.0f;
	
	public void Fire(Transform playerTransform) {
		velocity = (playerTransform.position - transform.position).normalized * scalar;
		isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(isActive) {
			transform.position += velocity;

			if((timeToLive -= Time.deltaTime) <= 0.0f) {
				Instantiate(explosionFab, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		Instantiate(explosionFab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}

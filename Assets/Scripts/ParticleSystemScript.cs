using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemScript : MonoBehaviour {

	ParticleSystem ps;

	float explosionDamge = 4.0f;

	bool hasNOTdamgePlayer = true;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ps) {
			if(!ps.IsAlive()) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Player") {
			// explosion also damage the player
			if(hasNOTdamgePlayer) {
				other.GetComponent<Healthbar>().DecrementHealth(explosionDamge);
				hasNOTdamgePlayer = false;
			}
		}
	}
}

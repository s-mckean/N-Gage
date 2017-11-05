using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSFX : MonoBehaviour {

	AudioSource audioSource;

	// player health bar
	Healthbar hb;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		hb = GameObject.FindGameObjectWithTag("Player").GetComponent<Healthbar>();
	}
	
	public void PlaySFX() {
		// player is dead
		// we want to hear the player death scream
		if(hb && hb.HealthPoints() <= 0.0f) {
			audioSource.volume = 0.2f;
			audioSource.Play();
		}
		else { 
			audioSource.Play();
		}

		Destroy(gameObject, audioSource.clip.length);
	}
}

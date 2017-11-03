using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	#region Create new AudioClip for all sound effects here
	public AudioClip grandDaddySFX;
	public AudioClip gunFire;
	public AudioClip gunReload;
	public AudioClip forceFieldDownSFX;

	#endregion

	AudioSource audioSource;

	// singleton 
	public static AudioController instance = null;


	void Awake() {
		if(instance == null) {
			instance = this;
			audioSource = GetComponent<AudioSource>();
		}
		else if(instance != this) Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void PlayGrandDaddySFX() {
		audioSource.PlayOneShot(grandDaddySFX);
	}	

	public void PlayGunFire() {
		audioSource.PlayOneShot(gunFire);
	}	

	public void PlayGunReload() {
		audioSource.PlayOneShot(gunReload);
	}

	public void PlayForceFielddown() {
		audioSource.PlayOneShot(forceFieldDownSFX);
	}
}

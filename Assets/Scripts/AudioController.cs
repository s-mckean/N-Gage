using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

	#region Create new AudioClip for all sound effects here
	public AudioClip grandDaddySFX;
	public AudioClip gunFire;
	public AudioClip gunReload;
	public AudioClip forceFieldDownSFX;
    public AudioClip HurtNoise;

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
		audioSource.PlayOneShot(grandDaddySFX, 1.0f);
	}	

	public void PlayGunFire() {
		audioSource.PlayOneShot(gunFire, 1.0f);
	}	

	public void PlayGunReload() {
		audioSource.PlayOneShot(gunReload, 1.0f);
	}

	public void PlayForceFielddown() {
		audioSource.PlayOneShot(forceFieldDownSFX, 1.0f);
	}

    public void PlayHurtNoise()
    {
        audioSource.PlayOneShot(HurtNoise, 1.0f);
    }
}

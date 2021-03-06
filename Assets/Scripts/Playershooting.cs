﻿using System.Collections;
using UnityEngine;

public class Playershooting : MonoBehaviour {
    public float damage = 10f;
    public float range = 100f;
    public float clipSize = 10;
    public float reloadTime = 1f;


    public Camera playerCam;
    public ParticleSystem muzzleFlash;
    public Animator animator;


    public GameObject Canvas;
    public GameObject GunShots;

    private float currentClip;

	bool playReloadClip = true;

	int bossDamage = 20;

    void Start () {
		
	}

    void Update()
    {

        if (currentClip <= 0)
        {			
			if(playReloadClip) {
				AudioController.instance.PlayGunReload();
				playReloadClip = false;
			}
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            shotsFired();
        }
        
    }

    IEnumerator Reload()
    {
        animator.SetBool("Reloading", true);		
        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);
        currentClip = clipSize;
		playReloadClip = true;
    }

    void shotsFired()
    {

        muzzleFlash.Play();
		AudioController.instance.PlayGunFire();
        currentClip--;
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {			
			if(hit.transform.gameObject.tag == "Enemy") {
				EnemyControl enemy = hit.transform.GetComponent<EnemyControl>();
				if (enemy != null)
				{
					enemy.TakeDamage(damage);
				}

				GameObject shots = Instantiate(GunShots, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(shots, 1f);
			}
			else if(hit.transform.gameObject.tag == "BossBody") {
				GameObject.FindGameObjectWithTag("Boss").GetComponent<playerControl>().Hit(bossDamage);
				GameObject shots = Instantiate(GunShots, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(shots, 1f);
			}
			else if(hit.transform.gameObject.tag == "Generator") {
				GameObject shots = Instantiate(GunShots, hit.point, Quaternion.LookRotation(hit.normal));
				Destroy(shots, 1f);
			}
        }
    }
}

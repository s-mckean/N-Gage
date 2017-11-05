using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Healthbar : MonoBehaviour
{
    public float hitPoints = 100;
    public Image healthBar;
    private float fillAmount;

    public Image HurtColor;

    AudioSource audioSource;
    public AudioClip HurtSound;
    public AudioClip DeathSound;

    Color transparent;
    Color solid;

    private bool isDead = false;

	// let the ExplosionSFX know the player is dead
	// so it'll decrese the sfx 
	// we want to hear the death sound
	public float HealthPoints() {
		return hitPoints;
	}

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transparent = HurtColor.color;
        transparent.a = 0;
        solid = HurtColor.color;
        solid.a = .5f;
    }

    void Update()
    {
        handleBar();
        if (hitPoints <= 0)
        {
            StartCoroutine("LoadDeathScene");
        }
    }

    private void handleBar()
    {
        healthBar.fillAmount = Map(hitPoints, 0, 100, 0, 1);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void DecrementHealth(float hitAmount)
    {		

        HurtColor.color = solid;
        StartCoroutine(activateHurtColor());
		hitPoints -= hitAmount;
		if(hitPoints > 0.0f)
			audioSource.PlayOneShot(HurtSound);
        
    }

    IEnumerator LoadDeathScene()
    {
        if (isDead == false)
        {
            isDead = true;
            audioSource.PlayOneShot(DeathSound, 1.0f);
            yield return new WaitForSeconds(3);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("Lose");
            yield return null;
        }
    }

    IEnumerator activateHurtColor()
    {
        yield return new WaitForSeconds(1);
        HurtColor.color = transparent;
    }
}


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

    void Start()
    {
        
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
        hitPoints -= hitAmount;
    }

    IEnumerator LoadDeathScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Lose");
        yield return null;
    }
}


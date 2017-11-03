using System.Collections;
using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

    public float hitPoints = 100;

    private float health;
    // Use this for initialization
    void Start () {
        health = hitPoints;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

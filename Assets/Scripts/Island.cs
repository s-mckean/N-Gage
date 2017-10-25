using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {


    [HideInInspector]
    public bool wrongPlacement = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
            wrongPlacement = true;
    }
}

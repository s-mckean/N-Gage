using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Island : MonoBehaviour {

    public float angle;

    [HideInInspector]
    public bool wrongPlacement = false;

    void OnTriggerEnter(Collider other)
    {
            wrongPlacement = true;
    }
}

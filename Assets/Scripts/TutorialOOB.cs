using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialOOB : MonoBehaviour {

    public GameObject Checkpoint;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.position = Checkpoint.transform.position;
            other.transform.rotation = Checkpoint.transform.rotation;
            other.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
        }
    }
}

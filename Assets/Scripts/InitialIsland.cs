using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialIsland : MonoBehaviour {

    private GameObject forceField;
    private Rigidbody ffRigidBody;
    public float maxVerticalSpeed = 5000f;
    public float maxHeight = 50f;
    public float minHeight = 10f;
    private Vector3 originalPos = new Vector3();
    private bool moveUpwards = true;

	// Use this for initialization
	public void TriggeredStart () {
        forceField = this.transform.Find("ForceField").gameObject;
        ffRigidBody = forceField.GetComponent<Rigidbody>();
        originalPos = forceField.transform.position;
	}
	
	// Update is called once per frame
	public void TriggeredUpdate () {
        if (forceField != null)
        {
            if (moveUpwards == true)
            {
                if (forceField.transform.position.y < originalPos.y + maxHeight)
                {
                    ffRigidBody.AddForce(new Vector3(0, maxVerticalSpeed, 0));
                }
                else
                {
                    moveUpwards = false;
                }
            }
            else
            {
                if (forceField.transform.position.y > originalPos.y - minHeight)
                {
                    moveUpwards = true;
                }
            }
        }
	}

    void Scale()
    {

    }

    void ForceFieldFloat()
    {

    }
}

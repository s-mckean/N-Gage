using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementAdditions : MonoBehaviour {
    public static bool boostActive = false;
    bool isBoostOnCooldown = false;
    public float boostStrength = 100000;
    public float boostCooldown;
    public Camera mainCamera;
    Rigidbody Player;

	// Use this for initialization
	void Start () {
        Player = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey("e"))
            if (!isBoostOnCooldown)
            {
                StartCoroutine(JumpBoost());
            }
    }

    public IEnumerator JumpBoost()
    {       
        isBoostOnCooldown = true;
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        Vector3 angle = mainCamera.transform.forward;
        Vector3 v = Player.velocity;
        Player.AddForce(angle * boostStrength);
        boostActive = true;
        StartCoroutine(DragUpdater());
        yield return new WaitForSeconds(boostCooldown);
        isBoostOnCooldown = false;
    }

    IEnumerator DragUpdater()
    {
        Player.drag = 2;
        yield return new WaitForSeconds(1);
        boostActive = false;
    }
}

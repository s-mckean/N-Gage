﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * 
 * Forward is in the positive z direction
 * 
 */

public class EnemyControl : MonoBehaviour
{

    public float hitPoints = 50f;
    public Transform hero;

    Rigidbody rb;

    float angleRotation = 10 * Mathf.Deg2Rad;

    float deltaRotateDirTimer = 0.0f;
    float deltaRotateDirTimerLimit;
    const float LOWER_DELTA_ROTATE = 1.0f;
    const float UPPER_DELTA_ROTATE = 5.0f;


    float timerMoveInbound;
    const float MAX_MOVE_INBOUND_TIME = 3.0f;

    float speed;
    const float MAX_SPEED = 0.01f;

    float probabiliyOfStandingStill = 0.3f;

    float moveTimer = 0.0f;
    float moveTimeLimit = 0.0f;
    const float LOWER_MOVE_TIME = 0.2f;
    const float UPPER_MOVE_TIME = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
        speed = (Random.Range(0.0f, 1.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);
        angleRotation *= (Random.Range(0, 2) == 0 ? 1.0f : -1.0f);

        transform.Rotate(transform.up, Random.Range(-360, 360) * Mathf.PI, Space.World);

    }

    // Update is called once per frame
    void Update()
    {
        if (FollowPlayer())
        {
            //Debug.Log("Following");
        }
        else
        {
            // move forward
            Vector3 newPos = new Vector3(0.0f, 0.0f, speed);
            newPos = transform.rotation * newPos;
            transform.position = new Vector3(transform.position.x + newPos.x, transform.position.y + newPos.y, transform.position.z + newPos.z);

            // change the movement
            if ((moveTimer += Time.deltaTime) >= moveTimeLimit)
            {
                // get a new speed
                speed = (Random.Range(0.0f, 1.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);

                // reset timer
                moveTimer = 0.0f;
                moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
            }

            // if enemy is moving in bound, don't rotate the enemy.
            if (timerMoveInbound < 0.0f)
            {
                timerMoveInbound -= Time.deltaTime;
            }
            else
            {
                transform.Rotate(transform.up, angleRotation);
            }

            // change rotation direction
            if ((deltaRotateDirTimer += Time.deltaTime) >= deltaRotateDirTimerLimit)
            {
                angleRotation *= (Random.Range(0, 2) == 0 ? 1.0f : -1.0f);

                deltaRotateDirTimer = 0.0f;
                deltaRotateDirTimerLimit = Random.Range(LOWER_DELTA_ROTATE, UPPER_DELTA_ROTATE);
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GravityZone")
        {
            Debug.Log("Enemy Entered Island");
            this.gameObject.transform.SetParent(other.gameObject.transform, true);
        }
    }


    void OnTriggerExit(Collider other)
    {
        // enemy exiting the area, turn enemy around
        if (other.gameObject.tag == "CreatureArea")
        {
            transform.Rotate(transform.up, 180.0f * Mathf.PI, Space.World);
            timerMoveInbound = MAX_MOVE_INBOUND_TIME;
        }
    }


    public void TakeDamage(float amount)
    {
        hitPoints -= amount;
        if (hitPoints <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        // play sound effect
        AudioController.instance.PlayGrandDaddySFX();
        Destroy(gameObject);
    }

    public bool FollowPlayer()
    {
        Vector3 toPlayerVector = transform.position - hero.position;
        //Debug.Log(toPlayerVector);
        if (Mathf.Abs(toPlayerVector.x) < 20 && Mathf.Abs(toPlayerVector.z) < 20)
        {
            //Debug.Log("Player is in Range");
            Vector3 lookAtTransform = new Vector3(hero.position.x, hero.position.y + 0.8f, hero.position.z);
            //Debug.Log(lookAtTransform);
            transform.LookAt(lookAtTransform);
            transform.position = transform.position + (transform.rotation * new Vector3(0f, 0f, speed));
            return true;
        }
        else return false;
    }

}

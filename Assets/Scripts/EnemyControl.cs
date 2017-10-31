using System.Collections;
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

    float angleRotation = 0;

    float deltaRotateDirTimer = 0.0f;
    float deltaRotateDirTimerLimit;
    const float LOWER_DELTA_ROTATE = 1.0f;
    const float UPPER_DELTA_ROTATE = 5.0f;


    float timerMoveInbound;
    const float MAX_MOVE_INBOUND_TIME = 3.0f;

    float speed;
    const float MAX_SPEED = 4.0f;

    float probabiliyOfStandingStill = 0.3f;

    float moveTimer = 0.0f;
    float moveTimeLimit = 0.0f;
    const float LOWER_MOVE_TIME = 0.2f;
    const float UPPER_MOVE_TIME = 1.0f;

    void Start()
    {
        SetAnimation("walk");
        moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
        speed = (Random.Range(0.0f, 10.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);
        angleRotation *= (Random.Range(0, 2) == 0 ? 1.0f : -1.0f);

        transform.forward = Quaternion.AngleAxis(Random.Range(0f, 360f), transform.up) * transform.forward;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forwardTransform = transform.forward;
        forwardTransform.y = 0.0f;
        transform.forward = forwardTransform;

        Debug.Log(transform.forward);
        if (Vector3.Distance(transform.position, hero.position) <= 2.5)
        {
            transform.LookAt(hero);
            SetAnimation("hit");
        }
        else if (Vector3.Distance(transform.position, hero.position) <= 5)
        {
            SetAnimation("walk");
            transform.LookAt(hero);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            MoveRandomly();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GravityZone")
        {
            //Debug.Log("Enemy Entered Island");
            this.gameObject.transform.SetParent(other.gameObject.transform, true);
        }
    }


    void OnTriggerExit(Collider other)
    {
        // enemy exiting the area, turn enemy around
        if (other.gameObject.tag == "CreatureArea")
        {
            transform.forward = Quaternion.AngleAxis(180.0f, transform.up) * transform.forward;
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
        //AudioController.instance.PlayGrandDaddySFX();
        Destroy(this.gameObject);
    }

    public void MoveRandomly()
    {
        // move forward
        transform.position += transform.forward * speed * Time.deltaTime;

        // change the movement
        if ((moveTimer += Time.deltaTime) >= moveTimeLimit)
        {
            // get a new speed
            speed = (Random.Range(0.0f, 1.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);
            if (speed == 0) SetAnimation("idle");
            else SetAnimation("walk");

            // reset timer
            moveTimer = 0.0f;
            moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
        }

        // if enemy is moving in bound, don't rotate the enemy.
        if (timerMoveInbound > 0.0f)
        {
            timerMoveInbound -= Time.deltaTime;
        }

        // change rotation direction
        else if ((deltaRotateDirTimer += Time.deltaTime) >= deltaRotateDirTimerLimit)
        {
            angleRotation = Random.Range(0.0f, 180.0f);
            transform.forward = Quaternion.AngleAxis(angleRotation, transform.up) * transform.forward;
            deltaRotateDirTimer = 0.0f;
            deltaRotateDirTimerLimit = Random.Range(LOWER_DELTA_ROTATE, UPPER_DELTA_ROTATE);
        }
    }

    public void SetAnimation(string animationName)
    {
        GetComponent<Animation>().wrapMode = WrapMode.Loop;
        GetComponent<Animation>().CrossFade(animationName);
    }
}

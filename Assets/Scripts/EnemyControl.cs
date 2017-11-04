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

    public bool enemyIsDead = false;
    public bool enemyIsFalling = false;
    private Vector3 deadPosition;
    private Quaternion deadRotation;
    private bool exited = false;

	AudioSource audioSource;
	public AudioClip deathSFX;
	bool isNOTplayingDeathSFX = true;
    private float timeToHit = 0;
    public float enemyDamage = 5;

	public void TriggeredStart()
    {
        audioSource = GetComponent<AudioSource>();
        SetAnimation("walk");
        moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
        speed = (Random.Range(0.0f, 10.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);
        angleRotation *= (Random.Range(0, 2) == 0 ? 1.0f : -1.0f);

        transform.forward = Quaternion.AngleAxis(Random.Range(0f, 360f), transform.up) * transform.forward;

    }

    // Update is called once per frame
    public void TriggeredUpdate()
    {
        if (enemyIsDead == false)
        {
            Vector3 forwardTransform = transform.forward;
            forwardTransform.y = 0.0f;
            transform.forward = forwardTransform;
            timeToHit -= Time.deltaTime;

            if (Vector3.Distance(transform.position, hero.position) <= 2.5)
            {
                transform.LookAt(hero);
                SetAnimation("hit");
                if(timeToHit <= 0)
                {
                    timeToHit = 1.5f;
                    hero.GetComponent<Healthbar>().DecrementHealth(enemyDamage);
                }
            }
            else if (Vector3.Distance(transform.position, hero.position) <= 10)
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
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "GravityZone")
        {
            this.gameObject.transform.SetParent(other.gameObject.transform, true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "GravityZone")
        {
            this.gameObject.transform.SetParent(other.gameObject.transform, true);
        }
        else if (other.gameObject.tag == "CreatureArea")
        {
            exited = false;
        }
    }


    void OnTriggerExit(Collider other)
    {
        // enemy exiting the area, turn enemy around
        if (other.gameObject.tag == "CreatureArea")
        {
            transform.forward = Quaternion.AngleAxis(180.0f, transform.up) * transform.forward;
            timerMoveInbound = MAX_MOVE_INBOUND_TIME;
            exited = true;
            StartCoroutine("FallToDeath");
        }
    }


    public void TakeDamage(float amount)
    {
		if(!audioSource.isPlaying) {
			audioSource.Play();
		}

        hitPoints -= amount;
        if (hitPoints <= 0f)
        {
            deadPosition = transform.position - transform.parent.position;
            deadRotation = transform.rotation;
            enemyIsDead = true;
        }
    }

    public void Die()
    {
        // play sound effect
        //AudioController.instance.PlayGrandDaddySFX();
        if (!enemyIsFalling)
        {
            GetComponent<Animation>().wrapMode = WrapMode.ClampForever;
            GetComponent<Animation>().CrossFade("die");
			if(isNOTplayingDeathSFX) {
				audioSource.PlayOneShot(deathSFX);
				isNOTplayingDeathSFX = false;
			}
            StartCoroutine("DieAnimation");
        }
        else
        {
            Destroy(gameObject, 3);
        }
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

    public void SetHero(Transform player)
    {
        hero = player;
    }

    IEnumerator DieAnimation()
    {
        float timeLeft = 5f;
        float waitTime = 0.2f;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
        while (timeLeft > 0)
        {
            transform.position = deadPosition + transform.parent.position;
            transform.rotation = deadRotation;
            timeLeft -= waitTime;
            yield return new WaitForSeconds(waitTime);
        }
        Destroy(gameObject);
    }

    IEnumerator FallToDeath()
    {
        yield return new WaitForSeconds(2);
        if (exited)
        {
            enemyIsDead = true;
            enemyIsFalling = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Forward is in the positive z direction
 * 
 */

public class EnemyControl : MonoBehaviour {

	Rigidbody rb;

	float angleRotation = 10 * Mathf.Deg2Rad;
	const float MAX_ANGLE_ROTATION = 10 * Mathf.Deg2Rad;
	float probabilityOfRotation = 0.60f;
	float nextRotateTimer = 0.0f;
	float nextRotateTimeLimit;
	const float LOWER_ROTATE_TIME = 0.2f;
	const float UPPER_ROTATE_TIME = 0.5f;

	readonly Vector3 Y_AXIS = new Vector3(0.0f, 1.0f, 0.0f);

	float speed;
	const float MAX_SPEED = 0.001f;

	float probabiliyOfStandingStill = 0.3f;

	float moveTimer = 0.0f;
	float moveTimeLimit = 0.0f;
	const float LOWER_MOVE_TIME = 0.2f;
	const float UPPER_MOVE_TIME = 1.0f;


	void Start() {
		rb = GetComponent<Rigidbody>();
		moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
		speed = (Random.Range(0.0f, 1.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);

		nextRotateTimeLimit = Random.Range(LOWER_ROTATE_TIME, UPPER_ROTATE_TIME);
		angleRotation = MAX_ANGLE_ROTATION  * (Random.Range(0, 2) == 0 ? 1.0f : -1.0f);

	}

	// Update is called once per frame
	void Update () {
		
		// move forward
		Vector3 newPos = new Vector3(0.0f, 0.0f, speed);
		newPos = transform.rotation * newPos;
		transform.position = new Vector3(transform.position.x + newPos.x, transform.position.y + newPos.y, transform.position.z + newPos.z);

		// change the movement
		if((moveTimer += Time.deltaTime) >= moveTimeLimit) {			
			// get a new speed
			speed = (Random.Range(0.0f, 1.0f) <= probabiliyOfStandingStill ? 0.0f : MAX_SPEED);

			// reset timer
			moveTimer = 0.0f;
			moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
		}

		// change in rotation
		if( (nextRotateTimer += Time.deltaTime) >= nextRotateTimeLimit) {
			if(Random.Range(0.0f, 1.0f) <= probabilityOfRotation) {
				transform.Rotate(Y_AXIS, angleRotation);
			}

			nextRotateTimeLimit = Random.Range(LOWER_ROTATE_TIME, UPPER_ROTATE_TIME);
			angleRotation = MAX_ANGLE_ROTATION  * (Random.Range(0, 2) == 0 ? 1.0f : -1.0f);
		}
	}
}

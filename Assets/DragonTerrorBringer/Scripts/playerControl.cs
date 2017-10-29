using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour 
{

	// for player to shoot at the boss
	public GameObject IdleAttackCollider;
	public GameObject FlyForwardCollider;

	public GameObject avoidObsIdleAttack;
	public GameObject avoidObsFlyFoward;

	bool isInForceField = false;
	 
	readonly Vector2 X_BOUND = new Vector2(-778.0f, 778.0f);
	readonly Vector2 Y_BOUND = new Vector2(-320.0f, 380.0f);
	readonly Vector2 Z_BOUND = new Vector2(-848.0f, 848.0f);


	// movement
	float speed = 2.0f;
	float probabiliyOfStandingStill = 0.4f;

	float moveTimer = 0.0f;
    float moveTimeLimit = 0.0f;
    const float LOWER_MOVE_TIME = 0.2f;
    const float UPPER_MOVE_TIME = 1.0f;	

	// for turing
	float rotateTime = 0.0f;
	float rotateTimeLimit;
	const float LOWER_ROTATETIME = 2.0f;
	const float UPPER_ROTATETIME = 10.0f;

	bool isRotating = false;
	bool isRotateX = false;
	bool isRotateY = false;
	bool isRotateZ = false;
	
	const float ROTATE_ANGLE = 20.0f * Mathf.Deg2Rad;
	const float SHARP_ROTATE_ANGLE = 50.0f * Mathf.Deg2Rad;

	float rotateX = ROTATE_ANGLE;
	float rotateY = ROTATE_ANGLE;
	float rotateZ = ROTATE_ANGLE;

	readonly Vector3 xAxis = new Vector3(1.0f, 0.0f, 0.0f);
	readonly Vector3 yAxis = new Vector3(0.0f, 1.0f, 0.0f);
	readonly Vector3 zAxis = new Vector3(0.0f, 0.0f, 1.0f);


	Animator anim;
	int scream;
	int basicAttack;
	int clawAttack;
	int flameAttack;
	int defend;
	int getHit;
	int sleep;
	int walk;
	int run;
	int takeOff;
	int flyFlameAttack;
	int flyForward;
	int flyGlide;
	int land;
	int die;
	int idle02;

	void Awake () 
	{
		anim = GetComponent<Animator>();
		scream = Animator.StringToHash("Scream");
		basicAttack = Animator.StringToHash("Basic Attack");
		clawAttack = Animator.StringToHash("Claw Attack");
		flameAttack = Animator.StringToHash("Flame Attack");
		defend = Animator.StringToHash("Defend");
		getHit = Animator.StringToHash("Get Hit");
		sleep = Animator.StringToHash("Sleep");
		walk = Animator.StringToHash("Walk");
		run = Animator.StringToHash("Run");
		takeOff = Animator.StringToHash("Take Off");
		flyFlameAttack = Animator.StringToHash("Fly Flame Attack");
		flyForward = Animator.StringToHash("Fly Forward");
		flyGlide = Animator.StringToHash("Fly Glide");
		land = Animator.StringToHash("Land");
		die = Animator.StringToHash("Die");
		idle02 = Animator.StringToHash("Idle02");

		// remove after testing
		anim.SetTrigger(flyForward);	
		IdleAttackCollider.SetActive(false);
		FlyForwardCollider.SetActive(true);
		avoidObsIdleAttack.SetActive(false);
		avoidObsFlyFoward.SetActive(true);			
	}

	void Start() {
		moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);

		// remove after testing
		transform.parent = null;
	}

	void Update() {
		if(isInForceField) {
			if(GameObject.FindGameObjectsWithTag("Generator").Length <= 0) {
				isInForceField = false;
				
				// change animation and collider
				anim.SetTrigger(flyForward);	
				IdleAttackCollider.SetActive(false);
				FlyForwardCollider.SetActive(true);
				avoidObsIdleAttack.SetActive(false);
				avoidObsFlyFoward.SetActive(true);
				transform.parent = null;				
			}
		}

		// start moving and attacking

		float delta = Time.deltaTime;	

		// rotating
		if(isRotating) {
			if(isRotateX) {					
				transform.Rotate(xAxis, rotateX);
			}
			if(isRotateY) {
				transform.Rotate(yAxis, rotateY);
			}
			if(isRotateZ) {
				transform.Rotate(zAxis, rotateZ);
			}

			// rotate time is up so no more rotating
			if( (rotateTime += delta) >= rotateTimeLimit) {
				isRotating = false;
			}
		}
		else {
			// rolette
			isRotating = System.Convert.ToBoolean(Random.Range(0, 2));
			if(isRotating) {
				rotateTimeLimit = Random.Range(LOWER_ROTATETIME, UPPER_ROTATETIME);
				rotateTime = 0.0f;
				isRotateX = System.Convert.ToBoolean(Random.Range(0, 2));
				isRotateY = System.Convert.ToBoolean(Random.Range(0, 2));
				isRotateZ = System.Convert.ToBoolean(Random.Range(0, 2));

				rotateX *= (Random.Range(0, 2) == 0 ? 1 : -1);
				rotateY *= (Random.Range(0, 2) == 0 ? 1 : -1);
				rotateZ *= (Random.Range(0, 2) == 0 ? 1 : -1);
			}
		}

		transform.position = transform.position + (transform.rotation * (new Vector3(0.0f, 0.0f, speed)));

		// bound checking

	}


	public void Scream ()
	{
		anim.SetTrigger(scream);
	}

	public void BasicAttack ()
	{
		anim.SetTrigger(basicAttack);
	}

	public void ClawAttack ()
	{
		anim.SetTrigger(clawAttack);
	}

	public void FlameAttack ()
	{
		anim.SetTrigger(flameAttack);
	}

	public void Defend ()
	{
		anim.SetTrigger(defend);
	}

	public void GetHit ()
	{
		anim.SetTrigger(getHit);
	}

	public void Sleep ()
	{
		anim.SetTrigger(sleep);
	}

	public void Walk ()
	{
		anim.SetTrigger(walk);
	}

	public void Run ()
	{
		anim.SetTrigger(run);
	}

	public void TakeOff ()
	{
		anim.SetTrigger(takeOff);
	}

	public void FlyFlameAttack ()
	{
		anim.SetTrigger(flyFlameAttack);
	}

	public void FlyForward ()
	{
		anim.SetTrigger(flyForward);
	}

	public void FlyGlide ()
	{
		anim.SetTrigger(flyGlide);
	}

	public void Land ()
	{
		anim.SetTrigger(land);
	}

	public void Die ()
	{
		anim.SetTrigger(die);
	}

	public void Idle02 ()
	{
		anim.SetTrigger(idle02);
	}
	
}

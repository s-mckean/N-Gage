using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour 
{

	public int health = 200;
	bool isDead = false;

	public Transform playerTransform;

	// for player to shoot at the boss
	public GameObject IdleAttackCollider;
	public GameObject FlyForwardCollider;

	public GameObject avoidObsIdleAttack;
	public GameObject avoidObsFlyFoward;

	// help us orientate the boss during the firing
	public GameObject bellyObj;
	public GameObject backObj;

	bool isInForceField = false;
	 
	//readonly Vector2 X_BOUND = new Vector2(-778.0f, 778.0f);
	//readonly Vector2 Y_BOUND = new Vector2(-320.0f, 380.0f);
	//readonly Vector2 Z_BOUND = new Vector2(-848.0f, 848.0f);

	const float MAX_DISTANT_FROM_PLAYER = 800.0f;

	// movement
	float speed = 2.0f;
	//float probabiliyOfStandingStill = 0.4f;

	float moveTimer = 0.0f;
    float moveTimeLimit = 0.0f;
    const float LOWER_MOVE_TIME = 0.2f;
    const float UPPER_MOVE_TIME = 1.0f;	

	// for turing
	bool isRotatingTowardPlayer = false;
	float angleBetweenSelfAndPlayer = 0.0f;
	Vector3 currentAxisRotation;
	float tempRotateAngle;

	float rotateTime = 0.0f;
	float rotateTimeLimit;
	const float LOWER_ROTATETIME = 2.0f;
	const float UPPER_ROTATETIME = 10.0f;

	bool isRotating = false;
	bool isRotateX = false;
	bool isRotateY = false;
	bool isRotateZ = false;
	
	const float ROTATE_ANGLE = 20.0f * Mathf.Deg2Rad;
	const float SHARP_ROTATE_ANGLE = 40.0f * Mathf.Deg2Rad;

	float rotateX = ROTATE_ANGLE;
	float rotateY = ROTATE_ANGLE;
	float rotateZ = ROTATE_ANGLE;

	readonly Vector3 xAxis = new Vector3(1.0f, 0.0f, 0.0f);
	readonly Vector3 yAxis = new Vector3(0.0f, 1.0f, 0.0f);
	readonly Vector3 zAxis = new Vector3(0.0f, 0.0f, 1.0f);


	// attack
	float fireTimer = 0.0f;
	float fireTimeLimit;
	bool isFireMode = false;
	const float LOWER_FIRE_LIMIT = 1.0f;
	const float UPPER_FIRE_LIMIT = 10.0f;
	float chargeUPAnimationTimer = 3.26f;
	const float CHARGEUP_ANIMATION_LIMIT = 3.26f;	// the number is from the animation "Fly Flame Attack" of the dragon mesh

	float speedMultiplier = 2.0f;	// in firing mode .. make the dragon travel faster
	bool isInFiringPosition = false;
	bool isNOTup	= true;


	// plasma ball
	public GameObject plasmaFireBallFab;

	GameObject fireball;
	readonly Vector3 fireballOffsetVec = new Vector3(0.0f, 98.8f, 30.64f);
	
	
	// sound effects
	AudioSource audioSource;
	public AudioClip roarSFX;
	public AudioClip fireSFX;
	bool isNOTChargeUp = true;
	float roarTimer = 0.0f;
	float roarTimeLimit;
	const float LOWER_ROAR_TIMER = 1.0f;
	const float UPPER_ROAR_TIMER = 7.0f;


	#region Animator
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
	#endregion


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
		anim.SetTrigger(flyFlameAttack);	
		IdleAttackCollider.SetActive(false);
		FlyForwardCollider.SetActive(true);
		avoidObsIdleAttack.SetActive(false);
		avoidObsFlyFoward.SetActive(true);			
	}

	void Start() {
		moveTimeLimit = Random.Range(LOWER_MOVE_TIME, UPPER_MOVE_TIME);
		fireTimeLimit = Random.Range(LOWER_FIRE_LIMIT, UPPER_FIRE_LIMIT);
		audioSource = GetComponent<AudioSource>();
		roarTimeLimit = Random.Range(LOWER_ROAR_TIMER, UPPER_ROAR_TIMER);

		// remove after testing
		transform.parent = null;
	}



	void PlayFlyAnimation() {
		//anim.SetTrigger(flyForward);	
		IdleAttackCollider.SetActive(false);
		FlyForwardCollider.SetActive(true);
		avoidObsIdleAttack.SetActive(false);
		avoidObsFlyFoward.SetActive(true);
	}

	void PlayAttackAnimation() {
		audioSource.Play();
		anim.SetTrigger(flyFlameAttack);	
		IdleAttackCollider.SetActive(true);
		FlyForwardCollider.SetActive(false);
		avoidObsIdleAttack.SetActive(true);
		avoidObsFlyFoward.SetActive(false);
	}

	void Update() {
		//Debug.Log(transform.up);
		//return;
		if(isDead) return;

		if(isInForceField) {
			if(GameObject.FindGameObjectsWithTag("Generator").Length <= 0) {
				isInForceField = false;
				
				// change animation and collider
				anim.SetTrigger(flyForward);	
				PlayFlyAnimation();

				transform.parent = null;	
			}

			return;
		}

		// start moving and attacking

		float delta = Time.deltaTime;

		// play roar sound effect
		if(isNOTChargeUp) {
			if((roarTimer += delta) >= roarTimeLimit) {
				fireTimeLimit += 4.6f;	// make sure the charge up sound doesn't play while the boss is roaring
				roarTimer = 0.0f;
				roarTimeLimit = Random.Range(LOWER_ROAR_TIMER, UPPER_ROAR_TIMER) + 4.5f;
				
				audioSource.PlayOneShot(roarSFX);
			}
		}


		#region Fire
		if(!isFireMode && ( (fireTimer += delta) >= fireTimeLimit)) {
			isFireMode = true;
			isInFiringPosition = false;
			isNOTup = true; // a check to make sure it's right side up
		}

		if(isFireMode) {
			if(isInFiringPosition) {
				chargeUPAnimationTimer -= delta;
				if(chargeUPAnimationTimer <= 0.0f) {
					// fire projectile
					//Debug.Log("BOSS FIRE PROJECTILE");

					// check if player collided into the fireball before it has the chance of 
					// being fired by the boss
					if(fireball != null) {
						fireball.GetComponent<PlasmaFireball>().Fire(playerTransform);
						audioSource.PlayOneShot(fireSFX);
					}


					// change animation back into movement
					isNOTChargeUp = true;
					PlayFlyAnimation();
					chargeUPAnimationTimer = CHARGEUP_ANIMATION_LIMIT;
					isFireMode = false;
					fireTimer = 0.0f;
					fireTimeLimit = Random.Range(LOWER_FIRE_LIMIT, UPPER_FIRE_LIMIT);
					return;
				}
			}
			else {				
				// check if boss is right side up
				if(isNOTup) { 
					transform.LookAt(playerTransform);
					//if(transform.up.x < 0.0f || transform.up.y < 0.0f || transform.up.z < 0.0f) { // flip right side up
					//	transform.Rotate(zAxis, 160.0f * Mathf.Deg2Rad);
					//}

					//// check if right vector is pointing down, if yes rotate a bit more
					//if(transform.right.x < 0.0f || transform.right.y < 0.0f || transform.right.z < 0.0f) {
					//	transform.Rotate(zAxis, 40.0f * Mathf.Deg2Rad);
					//	//Debug.Log("Right: " + transform.right);
					//}

					isNOTup = false;
				}
				else {

					// is the boss below the player?
					if(transform.position.y < playerTransform.position.y) {	// make sure the dragon is really above the player
						// now move until boss is above the player
						transform.position = transform.position + (new Vector3(0.0f, speed * speedMultiplier, 0.0f));
					}
					else {

						//******************
						// maybe delete this chunk of code
						//*******************
						// do we need to flip the boss around so the belly face the player
						//float bellyDistance = (bellyObj.transform.position - playerTransform.position).magnitude;
						//float backDistrance = (backObj.transform.position - playerTransform.position).magnitude;
						//if(backDistrance < bellyDistance) {
						//	transform.Rotate(xAxis, 180.0f * Mathf.Deg2Rad);
						//}
						//**************************
						


						// boss is above the player
						fireball = Instantiate(plasmaFireBallFab, transform.position + (transform.rotation * fireballOffsetVec), Quaternion.identity);
						isNOTChargeUp = false;
						PlayAttackAnimation();
						isInFiringPosition = true;
					}
				}
			}

			return;
		}
		#endregion


		if(isRotatingTowardPlayer) {
			transform.Rotate(xAxis, tempRotateAngle);
			angleBetweenSelfAndPlayer -= tempRotateAngle;
			if(angleBetweenSelfAndPlayer <= 0.0f) {
				isRotatingTowardPlayer = false;
			}
		

			//transform.Rotate(currentAxisRotation, tempRotateAngle);
			//if(tempRotateAngle >= 0.0f) {
			//	angleBetweenSelfAndPlayer -= tempRotateAngle;
			//	if(angleBetweenSelfAndPlayer <= 0.0f) {
			//		isRotatingTowardPlayer = false;
			//	}
			//}
			//else {
			//	angleBetweenSelfAndPlayer += tempRotateAngle;
			//	if(angleBetweenSelfAndPlayer >= 0.0f) {
			//		isRotatingTowardPlayer = false;
			//	}
			//}
		}
		else { 
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
		}

		transform.position = transform.position + (transform.rotation * (new Vector3(0.0f, 0.0f, speed)));		

		Vector3 targetDir = playerTransform.position - transform.position;
		// if too far away from player turn back toward player.
		if(!isRotatingTowardPlayer && targetDir.sqrMagnitude >= (MAX_DISTANT_FROM_PLAYER * MAX_DISTANT_FROM_PLAYER)) {
			angleBetweenSelfAndPlayer = Vector3.Angle(transform.forward, targetDir) * Mathf.Deg2Rad;
			isRotatingTowardPlayer = true;
			isRotating = false;

			tempRotateAngle = ROTATE_ANGLE;
			//currentAxisRotation = xAxis;

			//Debug.Log(angleBetweenSelfAndPlayer);
			//// which axis to rotation from
			//float absX = Mathf.Abs(targetDir.x);
			//float absY = Mathf.Abs(targetDir.y);
			//float absZ = Mathf.Abs(targetDir.z);

			//tempRotateAngle = ROTATE_ANGLE;

			//// default to rotate via the x axis
			//if(absY > absX && absY > absZ) {
			//	if(Random.Range(0, 2) == 0) {
			//		currentAxisRotation = zAxis;
			//	}
			//	else {
			//		currentAxisRotation = xAxis;
			//	}
			//}
			//else if(absZ > absX && absZ > absY) {
			//	if(Random.Range(0, 2) == 0) {
			//		currentAxisRotation = yAxis;
			//	}
			//	else {
			//		currentAxisRotation = xAxis;
			//	}
				
			//	if(targetDir.z >= 0.0f) {
			//		tempRotateAngle = -ROTATE_ANGLE;
			//		angleBetweenSelfAndPlayer *= -1;
			//	}			
			//}
			//else {
			//	// default to rotate via the x axis
			//	currentAxisRotation = xAxis;
			//	if(targetDir.x >= 0.0f) {
			//		tempRotateAngle = -ROTATE_ANGLE;
			//		angleBetweenSelfAndPlayer *= -1;
			//	}
			//}


		}
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


	/// <summary>
	/// Hit has an optional paramater. If no arugment is supplied to Hit function,
	/// damage default to a value of 1
	/// </summary>
	/// <param name="damage"></param>
	public void Hit(int damage = 1) {
		if(isDead) return;

		health -= damage;
		if(health <= 0) {
			isDead = true;
			Die();
			Destroy(gameObject, 1.6f);
		}
	}



	
}

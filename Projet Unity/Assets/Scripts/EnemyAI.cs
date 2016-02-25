using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	//bool to check if AI is shot/dead
	public bool isShot = false;
	public bool isDead = false;
	public int HP = 100;

	//number of damage taken with one bullet
	public int resistance = 50;

	//ref to the player and the bullet of the enemy (for instantiation)
	public GameObject player;
	public Rigidbody bullet;

	public float bulletSpeed = 0.0f;

	//Ref to animator of AI + script to check if some animation are over
	Animator anim;
	EnemyAnimationCheckScript enemyCheckScript;

	public float shootingFrequency = 5.0f;
	float initialShootingFrequency = 0.0f;

	//audiosource et clip
	AudioSource[] myAudio;
	public AudioClip attackSound;
	public AudioClip injuredSound;
	public AudioClip deathSound;

	//ref to final door to open it when there are no enemies left
	public EndDoorScript endDoorScript;

	// Use this for initialization
	void Start () {
		initialShootingFrequency = shootingFrequency;
		player = GameObject.FindGameObjectWithTag ("Player");

		endDoorScript = GameObject.FindGameObjectWithTag ("endDoor").GetComponent<EndDoorScript>();

		anim = this.GetComponent<Animator> ();
		enemyCheckScript = this.GetComponent<EnemyAnimationCheckScript> ();

		myAudio = this.GetComponents<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		this.transform.LookAt (player.transform.position);

		shootingFrequency -= Time.deltaTime;

		if (isShot && !isDead) 
			applyShotEffect ();
			
		isShot = false;

		if (shootingFrequency <= 0) 
		{
			shootPlayer ();
		}

		if (isDead)
			applyDeadEffect ();

		resetAnimation ();

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			Debug.Log ("isDead = " + isDead);
			Debug.Log ("isShot = " + isShot);
		}
	}

	void shootPlayer()
	{
			RaycastHit hit;
			if (Physics.Raycast (this.transform.position, transform.TransformDirection(Vector3.forward),out hit, Mathf.Infinity)) 
			{
				if (hit.transform.gameObject == player) 
				{
					//play anmiation
					anim.SetBool ("isShooting", true);

					if (enemyCheckScript.isFiring == true) 
					{
						Rigidbody bulletClone = (Rigidbody)Instantiate (bullet, this.transform.position + this.transform.forward, this.transform.rotation);
						bulletClone.velocity = transform.TransformDirection (Vector3.forward) * bulletSpeed;

						//play sound of attack
						myAudio [1].clip = attackSound;
						myAudio [1].Play ();
						
						//reaload timer before shooting again
						shootingFrequency = initialShootingFrequency;
					}
				}
			}
	}

	void applyShotEffect()
	{
		//if enemy was shot while loading a shot itself, stop the shot and its animation
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Firing_Cacodemon")) 
		{	
			anim.SetBool ("isShooting", false);

			//reaload timer before shooting again
			shootingFrequency = initialShootingFrequency;
		}

		//Reduce enemy hp
		HP -= resistance;

		//Animate enemy
		anim.SetBool("isShot", true);

		//if hp <= 0, enemy is dead ==> play animation
		if (HP <= 0) 
		{			
			isDead = true;
			anim.SetBool ("isDead", true);
			Debug.Log (HP);
		}

		//play injured sound if not dead
		if (!isDead) 
		{
			myAudio[0].clip = injuredSound;
			myAudio[0].Play ();
		} 

		else if(isDead) 
		{
			myAudio[0].clip = deathSound;
			myAudio[0].Play ();
		}
	}

	void applyDeadEffect()
	{
		anim.SetBool("isShot", true);

		if (enemyCheckScript.isDeadAnimationOver == true) 
		{
			endDoorScript.enemyArray--;
			Destroy (this.gameObject);
		}
	}

	void resetAnimation()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("ShotCacodemon")) 
		{	
			anim.SetBool ("isShot", false);
		}

		if (enemyCheckScript.isShotAnimationOver == true) 
		{
			enemyCheckScript.isShotAnimationOver = false;
		} 

		else if (enemyCheckScript.isFiringAnimationOver == true) 
		{
			enemyCheckScript.isFiringAnimationOver = false;
			enemyCheckScript.isFiring = false;
			anim.SetBool ("isShooting", false);
		}
	}

}

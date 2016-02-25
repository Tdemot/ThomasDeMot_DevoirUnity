using UnityEngine;
using System.Collections;

public class CharacterSecondaryScript : MonoBehaviour {

	//animator for the shotgun
	public	Animator anim;

	//animator for HUD mariners'head
	public Animator headanim;

	//acces to a script on the sprite to check if shooting animation is over
	public shotgunAnimationCheckScript shotgunAnimationCheck;

	//audiosource for shotgun firing sound
	AudioSource[] audioShotgun;
	public AudioClip shotgunFiringSound;
	public AudioClip itemPickUpSound;

	//character's hp
	public int healthPoint = 100;
	public int armorPoint = 100;

	public bool isShooting = false;

	public LayerMask maskToShoot;

	// Use this for initialization
	void Start () {
		audioShotgun = this.GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButton(0) && isShooting == false)
		{
			//play the sound of the shot
			shootingSound();

			//play the gunshoot animation
			shootingAnimation();

			//Deal with the physics of shooting
			shooting();	
		}

		resetShooting ();
	}

	void shooting()
	{
		//Throw raycast and check if it touches an ennemy.  If it does, set enemy isShot boolean = true;
		RaycastHit hit;
		Ray rayFromCameraToMousePosition = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (rayFromCameraToMousePosition.origin, rayFromCameraToMousePosition.direction, out hit,  Mathf.Infinity, maskToShoot)) 
		{
			if (hit.transform.gameObject.tag == "Enemy") 
			{
				hit.transform.gameObject.GetComponent<EnemyAI>().isShot = true;

			}
		}

		isShooting = true;
	}

	void shootingSound()
	{
		//if shooting animation isn't already playing, you can play the firing sound
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("ShotgunAnimator")) 
		{
			audioShotgun[1].clip = shotgunFiringSound;
			audioShotgun[1].Play ();
		}
	}

	void shootingAnimation()
	{
		anim.SetBool ("isShooting", true);
	}

	void resetShooting()
	{
		//check if animation is currently playing, set boolean to go back to idle after animation is over
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("ShotgunAnimator")) 
		{	
			anim.SetBool ("isShooting", false);
		}

		if (shotgunAnimationCheck.isAnimationOver == true) 
		{
			isShooting = false;
			shotgunAnimationCheck.isAnimationOver = false;
		}


	}

	void OnTriggerEnter(Collider other)
	{
		//if collision with healthbox, add 50 hp
		if (other.transform.gameObject.tag == "HealthBox" && healthPoint <= 150)
		{
			healthPoint += 20;
			headanim.SetInteger ("HP", healthPoint);

			Destroy (other.transform.gameObject);

			audioShotgun[2].clip = itemPickUpSound;
			audioShotgun[2].Play ();
		}

		if (other.transform.gameObject.tag == "ArmorBox" && armorPoint <= 150)
		{
			armorPoint += 20;
			Destroy (other.transform.gameObject);

			audioShotgun[2].clip = itemPickUpSound;
			audioShotgun[2].Play ();
		}

		if (healthPoint > 150)
			healthPoint = 150;

		if (armorPoint > 150)
			armorPoint = 150;

		if (other.tag == "bullet") 
		{
			if (armorPoint >= 20) {
				armorPoint -= 20;
				headanim.SetBool ("isHit", true);
			} 

			else if (armorPoint <= 0) 
			{
				healthPoint -= 20;
				headanim.SetBool ("isHit", true);
				headanim.SetInteger ("HP", healthPoint);
			}
		}
	}
}

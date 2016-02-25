using UnityEngine;
using System.Collections;

public class SlidingDoor : MonoBehaviour {

	//link to the door the switch will activate
	public GameObject linkedDoor;

	public bool isNearTheButton = false;

	public bool movementAllowed = false;
	public bool isClosed = true;
	public bool isOpen = false;

	//float to stock open/close door position
	public float sidePosition = 0.0f;
	float originalPosition = 0.0f;

	//audiosources and multiple clip for sound
	AudioSource buttonAudioSource;
	AudioSource linkedDoorAudiosource;

	public AudioClip doorOpening;
	public AudioClip doorClosing;

	public AudioClip buttonSwitchOn;
	public AudioClip buttonSwitchOff;

	//animator to change switch state and appearance
	public Animator myAnim;

	//vector for smootDamping.
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		originalPosition = linkedDoor.transform.position.x;
		sidePosition = linkedDoor.transform.position.x - 2.5f;

		buttonAudioSource = this.GetComponent<AudioSource> ();
		linkedDoorAudiosource = linkedDoor.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		checkInput ();

		if (movementAllowed) 
		{
			if (isOpen == true) 
			{
				close ();
			}

			else if (isClosed == true) 
			{
				openUp ();
			}
		}
			
	}

	void openUp()
	{
		//if door is under upPosition, lerp to it
		if (linkedDoor.transform.position.x >= sidePosition) 
		{			
			linkedDoor.transform.position = Vector3.SmoothDamp (linkedDoor.transform.position,
				new Vector3(sidePosition, linkedDoor.transform.position.y, linkedDoor.transform.position.z), ref velocity, 15.0f*Time.deltaTime);
		}

		//if final position is reached, door is opened and movement isn't allowed anymore
		if (linkedDoor.transform.position.x <= sidePosition + 0.1f) 
		{
			isClosed = false;
			isOpen = true;
			movementAllowed = false;
		}
	}

	void close()
	{
		//if door is over originalPosition, smoothdamp back to it
		if (linkedDoor.transform.position.x <= originalPosition) 
		{
			linkedDoor.transform.position = Vector3.SmoothDamp (linkedDoor.transform.position,
				new Vector3(originalPosition, linkedDoor.transform.position.y, linkedDoor.transform.position.z), ref velocity, 15.0f*Time.deltaTime);
		}

		//if final position is reached, door is closed and movement isn't allowed anymore
		if (linkedDoor.transform.position.x >= originalPosition - 0.1f) 
		{
			isClosed = true;
			isOpen = false;
			movementAllowed = false;
		}
	}


	void checkInput()
	{
		//if player is in trigger zone and press E, movement of the door is allowed
		if (isNearTheButton == true) 
		{
			if (Input.GetKeyDown(KeyCode.E)) 
			{
				movementAllowed = true;

				//if door is closed, play door opening sound on door audiosource and switch opening sound on this audiosource
				if (isClosed) 
				{
					linkedDoorAudiosource.clip = doorOpening;
					linkedDoorAudiosource.Play ();

					//change switch state and play the activation sound
					myAnim.SetBool ("isActive", true);

					buttonAudioSource.clip = buttonSwitchOn;
					buttonAudioSource.Play ();
				}

				//if door is open, play door closing sound on door audiosource and switch closing sound on this audiosource
				else if (isOpen)
				{
					linkedDoorAudiosource.clip = doorClosing;
					linkedDoorAudiosource.Play ();

					//change switch state and play the desactivation sound
					myAnim.SetBool("isActive", false);

					buttonAudioSource.clip = buttonSwitchOff;
					buttonAudioSource.Play ();
				}	
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
			isNearTheButton = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
			isNearTheButton = false;
	}
}

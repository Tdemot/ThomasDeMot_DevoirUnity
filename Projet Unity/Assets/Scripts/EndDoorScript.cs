using UnityEngine;
using System.Collections;

public class EndDoorScript : MonoBehaviour {

	public float enemyArray;
	bool isOpen = false;
	int audioAllowed = 0;

	//float to stock open/close door position
	public float sidePosition = 0.0f;

	AudioSource doorAudiosource;
	public AudioClip doorOpening;

	//vector for smootDamping.
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		sidePosition = this.transform.position.z + 2.5f;

		doorAudiosource = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (enemyArray == 0 && isOpen == false)
			openUp ();

		if (audioAllowed == 1)
			playAudio ();
	}

	void openUp()
	{
		audioAllowed++;

		if (transform.position.x <= sidePosition) 
		{			
			transform.position = Vector3.SmoothDamp (transform.position,
				new Vector3(transform.position.x, transform.position.y, sidePosition), ref velocity, 15.0f*Time.deltaTime);
		}

		//if final position is reached, door is opened and movement isn't allowed anymore
		if (this.transform.position.z >= sidePosition - 0.1f) 
			isOpen = true;
	}

	void playAudio()
	{
		doorAudiosource.clip = doorOpening;
		doorAudiosource.Play ();
	}

}

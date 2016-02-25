using UnityEngine;
using System.Collections;

public class BulletBehaviorScript : MonoBehaviour {

	//time before the bullet disappears naturally
	public float aliveTimer = 10.0f; 

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {

		transform.LookAt (player.transform.position);

		aliveTimer -= Time.deltaTime ;

		if (aliveTimer <= 0.0f)
			Destroy (this.transform.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		Destroy (this.transform.gameObject);
	}
}

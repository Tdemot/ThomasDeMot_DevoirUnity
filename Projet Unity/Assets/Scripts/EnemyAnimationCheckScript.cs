using UnityEngine;
using System.Collections;

public class EnemyAnimationCheckScript : MonoBehaviour {

	//boolean to check if some animation are over
	public bool isFiring = false;
	public bool isFiringAnimationOver = false;
	public bool isShotAnimationOver = false;
	public bool isDeadAnimationOver = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void setIsFiring()
	{
		isFiring = true;
	}

	void setIsFiringAnimationOver()
	{
		isFiringAnimationOver = true;
	}

	void setIsShotAnimationOver()
	{
		isShotAnimationOver = true;
	}

	void setIsDeadAnimationOver()
	{
		isDeadAnimationOver = true;
	}
}

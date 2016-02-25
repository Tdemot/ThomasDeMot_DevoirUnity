using UnityEngine;
using System.Collections;

public class HUDHeadAnimationCheckScript : MonoBehaviour {

	public Animator headAnim;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void resetIsHit()
	{
		headAnim.SetBool ("isHit", false);
	}
}

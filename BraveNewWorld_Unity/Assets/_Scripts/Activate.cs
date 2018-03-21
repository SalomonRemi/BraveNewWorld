using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour {
	public bool activate = false;
	public bool Puzzle1;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update () {

		if (activate == true) {
			Puzzle1 = true;
			anim.SetBool("isPushed", true);
			anim.SetBool("isNotPushed", false);
		}
			else {
			//anim.SetTrigger ("toActivate");
			anim.SetBool("isPushed", false);
			anim.SetBool("isNotPushed", true);
	}
}
}
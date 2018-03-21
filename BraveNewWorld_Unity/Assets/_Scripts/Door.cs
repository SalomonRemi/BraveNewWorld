using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public GameObject Button1;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (Button1.GetComponent<Activate> ().activate) {
			gameObject.SetActive (false);
			//print ("tchu" + anim.GetBool ("isPushed"));
			/*if (!anim.GetBool ("isPushed")) {
				//anim.SetBool ("isPushed", true);
				//anim.SetBool ("isNotPushed", false);
			} else {
				//anim.SetTrigger ("toActivate");
				//anim.SetBool("isPushed", false);
				//anim.SetBool("isNotPushed", true);
			}*/
		} else {
			gameObject.SetActive (true);
		}
		}
	}

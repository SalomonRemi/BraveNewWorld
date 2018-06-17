using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawerOpen : MonoBehaviour {

	public bool isOpen;
	bool moving;
	public GameObject destination;

	Vector3 originalPos;

	// Use this for initialization
	void Start () {
		isOpen = false;
		moving = false;
		originalPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void open(){
		if (!isOpen && !moving) {
			StopAllCoroutines ();
			StartCoroutine (moveDrawer (destination.transform.position, 0.3f));
            AudioManager.instance.PlaySound("openDesks");
            isOpen = true;
		} else if(!moving) {
			StopAllCoroutines ();
			StartCoroutine (moveDrawer (originalPos, 0.3f));
            AudioManager.instance.PlaySound("closeDesks");
            isOpen = false;
		}
	}

	IEnumerator moveDrawer(Vector3 dir, float timeToMove){

		float t = 0;
		var pos = transform.position;
		moving = true;
		while (t < 1) {
			t += Time.deltaTime/timeToMove;
			transform.position = Vector3.Lerp (pos, dir, t);
			yield return null;
		}
		moving = false;
		yield return null;
	}
}

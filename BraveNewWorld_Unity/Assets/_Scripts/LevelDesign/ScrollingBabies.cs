using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBabies : MonoBehaviour {

	private Vector3 pos;
    private float scrollingSpeed = 3f;

	private void Start()
	{
		pos = transform.position;
	}

	private void Update ()
	{
		pos.y -= Time.deltaTime * scrollingSpeed;
		transform.position = pos;
	}
		
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("babyKiller"))
		{
			Destroy(gameObject);
		}
	}
}
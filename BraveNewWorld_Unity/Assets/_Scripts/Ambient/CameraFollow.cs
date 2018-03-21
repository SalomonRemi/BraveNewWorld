using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;

	private float speed = 1f;

	void Update()
    {
		if (GameObject.FindGameObjectWithTag ("Player"))
        {
			Vector3 targetDir = target.position - transform.position;
			float step = speed * Time.deltaTime;
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			Debug.DrawRay (transform.position, newDir, Color.red);
			//newDir.y = 0f;
			transform.rotation = Quaternion.LookRotation (newDir);
		}
	}
}

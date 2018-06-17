using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby_BillBoard : MonoBehaviour {
    
	private GameObject camera;
	private Camera cam;

	void Start()
	{
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
		cam = camera.GetComponent<Camera>();
	}

    void Update()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward,
			cam.transform.rotation * Vector3.up);
    }
}
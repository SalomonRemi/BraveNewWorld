using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkSound : MonoBehaviour {

    // Use this for initialization
    CharacterController cc;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		if ((cc.isGrounded && cc.velocity.magnitude > 2f && GetComponent<AudioSource>().isPlaying == false)
			&& !GameManager.instance.documentOpen && !GameManager.instance.manualVisible)
        {
			GetComponent<AudioSource> ().volume = Random.Range (0.3f, 0.4f);
			GetComponent<AudioSource> ().pitch = Random.Range (0.95f, 1f);
            GetComponent<AudioSource> ().Play();
        }
    }
}
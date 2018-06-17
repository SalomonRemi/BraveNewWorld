using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBabies : MonoBehaviour {

	private Vector3 pos;
    private float scrollingSpeed = 2f;

    private Animator usineBlockAnim;
    private Animation currentAnim;

	private void Start()
	{
		pos = transform.position;

        usineBlockAnim = GetComponent<Animator>();

        int pool = Random.Range(0, 3);

        if (pool < 1)
        {
            usineBlockAnim.SetTrigger("Usine2");
        }
        else if (pool > 1 && pool <= 2)
        {
            usineBlockAnim.SetTrigger("Usine3");
        }

        AnimatorStateInfo state = usineBlockAnim.GetCurrentAnimatorStateInfo(0); //JOUE ANIM A RANDOM ENTRE 0 ET 1 NORMALIZED
        usineBlockAnim.Play(state.fullPathHash, 0, Random.Range(.1f, .5f));
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
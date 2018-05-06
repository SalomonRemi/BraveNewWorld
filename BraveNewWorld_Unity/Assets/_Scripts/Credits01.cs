using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits01 : MonoBehaviour {

	private Animator usineBlockAnim;
	private Animation currentAnim;

	private void Start()
	{
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
}
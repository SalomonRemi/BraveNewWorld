﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour {

	public List<GameObject> lockerNum;

	public string objectAnimClipName;

	private Animator objectAnim;

    [HideInInspector] public bool codeOk;


	private void Start()
	{
		objectAnim = GetComponentInParent<Animator>();
	}


    private void Update()
    {
        CheckIfGoodCode();
    }


    private void CheckIfGoodCode()
    {
        int validateNumber = 0;

        for (int i = 0; i < lockerNum.Count; i++)
        {
            LockerNum ln = lockerNum[i].GetComponent<LockerNum>();
            if (ln.isGoodNumber) validateNumber++;
        }

        if(validateNumber == lockerNum.Count)
        {
            Debug.Log("open");

			objectAnim.SetBool (objectAnimClipName, true);

            codeOk = true;

			for (int i = 0; i < lockerNum.Count; i++)
			{
				Collider[] col = lockerNum [i].GetComponentsInChildren<Collider>(); 

				foreach (Collider c in col)
				{
					c.enabled = false;
				}
			}
        }
    }
}
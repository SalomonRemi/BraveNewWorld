using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour {

	public List<GameObject> lockerNum;

	public string objectAnimClipName;
    public string soundPlay;

	private Animator objectAnim;

    [HideInInspector] public bool codeOk;

    private bool feedbackDone;

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
            codeOk = true;

            if(!feedbackDone) StartCoroutine(Feedback());

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

    public IEnumerator Feedback()
    {
        objectAnim.SetBool(objectAnimClipName, true);
        AudioManager.instance.PlaySound(soundPlay);

        yield return null;

        feedbackDone = true;
    }
}
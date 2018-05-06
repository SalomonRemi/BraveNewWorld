using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locker : MonoBehaviour {

	public List<GameObject> lockerNum;

	public string objectAnimClipName;
    public string soundPlay;

    public bool isVent;
    public bool isOscarLocker;

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

        if (isOscarLocker && !MissionManager.instance.isInLastPuzzle)
        {
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayMusic("escapeWarn01");

            Dialogue dialogue = new Dialogue();
            dialogue.sentences.Add("Qu'est ce que vous faites ?! Je vous ai dit de ne pas toucher aux affaires d'Oscar. Laissez ce casier et reprenez le travail.");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue);
        }
        else if(isVent && !MissionManager.instance.isInLastPuzzle)
        {
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayMusic("escapeWarn02");

            Dialogue dialogue1 = new Dialogue();
            dialogue1.sentences.Add("Bon sang Wilson laissez la ventilation ! Vous ne voudirez pas finir en salle de tri ?!");
            FindObjectOfType<DialogSystem>().StartDialogue(dialogue1);
        }

        yield return null;

        feedbackDone = true;
    }
}
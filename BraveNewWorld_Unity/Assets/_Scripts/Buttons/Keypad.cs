using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour {

	public GameObject[] keyButtons;
	public GameObject validate;
    public int enabledAmmount;
    public float keycode;
    public ExePuzzle ep;
    [HideInInspector] public bool canUse;

	public List<int> keyPressed = new List<int>();

	void Start ()
    {
        enabledAmmount = 0;
        canUse = true;
	}
	
	void Update ()
    {
        if(keycode == 0)
        {
            enabledAmmount = 0;
        }

		foreach(GameObject btn in keyButtons)
        {
			Vector3 newScale = new Vector3(btn.transform.localScale.x,btn.transform.localScale.y,btn.transform.localScale.z);

			if (btn.GetComponent<keyBtn> ().clicked)
			{
				newScale.y = 1.3f;
				btn.GetComponent<keyBtn>().support.GetComponent<MeshRenderer> ().material.color = Color.green;
			} 
			else
			{
				newScale.y = 3.3f;
				btn.GetComponent<keyBtn>().support.GetComponent<MeshRenderer> ().material.color = Color.black;
			}

			btn.transform.localScale = newScale;

            if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInteract>().isSitting)
            {
                btn.GetComponent<highlightSelf>().enabled = true;
                validate.GetComponent<highlightSelf>().enabled = true;
            }
            else
            {
                btn.GetComponent<highlightSelf>().enabled = false;
                validate.GetComponent<highlightSelf>().enabled = false;
            }
        }
	}


	public void ComfirmInput()
	{
        if (!MissionManager.instance.inExePuzzle)
        {
            if (MissionManager.instance.ValidateKeypadCode(keyPressed) == true)
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<keyBtn>().clicked = false;
                }
                if (GameManager.instance.flashKeypad)
                {
                    StartCoroutine(flashKeys(Color.green, true));
                }
                else
                {
                    StartCoroutine(flashKeys(Color.green, false));
                }
                AudioManager.instance.PlaySound("digiOkSound");
                MissionManager.instance.keyPadCorrect = true;
            }
            else
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<keyBtn>().clicked = false;
                }
                AudioManager.instance.PlaySound("digiError");
                StartCoroutine(flashKeys(Color.red, true));
            }
        }
        else if(MissionManager.instance.inExePuzzle)
        {
            if(ep.puzzleOk)
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<keyBtn>().clicked = false;
                }
                if (GameManager.instance.flashKeypad)
                {
                    StartCoroutine(lightFlashKeys(Color.green, true));
                }
                else
                {
                    StartCoroutine(flashKeys(Color.green, false));
                }

                ep.nextStep = true;
            }
            else if(ep.puzzleDone)
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<keyBtn>().clicked = false;
                }
                if (GameManager.instance.flashKeypad)
                {
                    StartCoroutine(flashKeys(Color.green, true));
                }
                else
                {
                    StartCoroutine(flashKeys(Color.green, false));
                }
                AudioManager.instance.PlaySound("digiOkSound");
            }
            else
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<keyBtn>().clicked = false;
                }
                ep.StopPuzzle();
            }
        }
	}


    public void resetKeypad()
    {
        foreach (GameObject btn in keyButtons)
        {
            if (btn.GetComponent<keyBtn>().clicked)
            {
                btn.GetComponent<Renderer>().material.color = Color.grey;
                btn.GetComponent<keyBtn>().clicked = false;
            }
        }
        enabledAmmount = 0;
		keyPressed.Clear ();
    }

    public IEnumerator flashKeys(Color col, bool flash)
    {
        if (flash)
        {
            canUse = false;

            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = col;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = Color.grey;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = col;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = Color.grey;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = col;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = Color.grey;
            }

            canUse = true;
        }
        foreach (GameObject btn in keyButtons)
        {
            btn.GetComponent<Renderer>().material.color = Color.grey;
        }
        resetKeypad();
        yield return null;
    }

    public IEnumerator lightFlashKeys(Color col, bool flash)
    {
        if (flash)
        {
            canUse = false;

            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = col;
            }
            yield return new WaitForSeconds(0.4f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = Color.grey;
            }
            yield return new WaitForSeconds(0.2f);

            canUse = true;
        }
        foreach (GameObject btn in keyButtons)
        {
            btn.GetComponent<Renderer>().material.color = Color.grey;
        }
        resetKeypad();
        yield return null;
    }
}
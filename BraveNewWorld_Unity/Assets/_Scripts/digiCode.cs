using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class digiCode : MonoBehaviour {

    public GameObject[] keyButtons;
    public int enabledAmmount;
    public float keycode;

    bool code1Used = false;
    public Animator tiroir;
    public Animator door;

    public ExePuzzle ep;

	private Animator levierAnim;


    void Start()
    {
		levierAnim = MissionManager.instance.levier;

        enabledAmmount = 0;

		keycode = 0f;
    }


    void Update()
    {
        MissionManager.instance.digiTxt.text = "" + keycode;

        if (keycode == 0)
        {
            enabledAmmount = 0;
            MissionManager.instance.digiTxt.text = "";
        }
        else MissionManager.instance.digiTxt.text = "" + keycode;

        foreach (GameObject btn in keyButtons)
        {
            if (btn.GetComponent<digicodeBtn>().clicked)
            {
                btn.GetComponent<Renderer>().material.color = Color.green;
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerInteract>().isSitting)
            {
                btn.GetComponent<highlightSelf>().enabled = true;
            }
            else
            {
                btn.GetComponent<highlightSelf>().enabled = false;
            }
        }
    }

    public void validateInput()
    {
        if (MissionManager.instance.inExePuzzle)
        {
            if (keycode == 95 && ep.stepID == 1)
            {
                ep.nextStep = true;
                StartCoroutine(lightFlashKeys(Color.green, true));
            }
            else if (keycode == 18 && ep.stepID == 2)
            {
                ep.nextStep = true;
                StartCoroutine(lightFlashKeys(Color.green, true));
            }
            else if (keycode == 19 && ep.stepID == 3)
            {
                ep.nextStep = true;
                StartCoroutine(lightFlashKeys(Color.green, true));
            }
            else
            {
                ep.StopPuzzle();
            }
        }
        else
        {
            if (keyPadValidation(keycode))
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<digicodeBtn>().clicked = false;
                }
            }
            else
            {
                foreach (GameObject btn in keyButtons)
                {
                    btn.GetComponent<digicodeBtn>().clicked = false;
                }
                StartCoroutine(flashKeys(Color.red, true));
            }
        }
    }

    public bool keyPadValidation(float keyCode)
    { 
        if (keycode == 5213 && MissionManager.instance.inLastPuzzle)
        {
            MissionManager.instance.digiFinishPuzzle = true;

            //AudioManager.instance.PlaySound("digiOkSound");

            MissionManager.instance.inLastPuzzle = false;

			foreach (GameObject btn in keyButtons)
			{
				btn.GetComponent<Renderer>().material.color = Color.green;
			}

            return true;
        }
		else if (keycode == 1211 && MissionManager.instance.hideDigicode && MissionManager.instance.searchJack)
		{
			MissionManager.instance.digiFinishPuzzle = true;
			MissionManager.instance.searchJack = false;

			GameManager.instance.flashKeypad = true;

			foreach (GameObject btn in keyButtons)
			{
				btn.GetComponent<Renderer>().material.color = Color.green;
			}
			//AudioManager.instance.PlaySound("digiOkSound");
			return true;
		}
        else 
		{
            AudioManager.instance.PlaySound("digiError");
            return false;
        }
    }

    public void resetKeypad()
    {
        foreach (GameObject btn in keyButtons)
        {
            if (btn.GetComponent<digicodeBtn>().clicked)
            {
                btn.GetComponent<digicodeBtn>().clicked = false;
                btn.GetComponent<Renderer>().material.color = Color.grey;
            }
        }
        enabledAmmount = 0;
        keycode = 0;
    }

    public IEnumerator flashKeys(Color col, bool flash)
    {
        resetKeypad();
        if (flash)
        {
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
        }
        foreach (GameObject btn in keyButtons)
        {
            btn.GetComponent<Renderer>().material.color = Color.grey;
        }
        
        yield return null;
    }

    public IEnumerator lightFlashKeys(Color col, bool flash)
    {
        resetKeypad();
        if (flash)
        {
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = col;
            }
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<Renderer>().material.color = Color.grey;
            }
        }
        foreach (GameObject btn in keyButtons)
        {
            btn.GetComponent<Renderer>().material.color = Color.grey;
        }

        yield return null;
    }
}

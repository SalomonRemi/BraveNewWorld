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

	private Animator levierAnim;

    // Use this for initialization
    void Start()
    {
		levierAnim = MissionManager.instance.levier;

        enabledAmmount = 0;

		keycode = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        MissionManager.instance.digiTxt.text = "" + keycode;

        if (keycode == 0)
        {
            enabledAmmount = 0;
            MissionManager.instance.digiTxt.text = "";
        }
        else
			MissionManager.instance.digiTxt.text = "" + keycode;


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
        if (keyPadValidation(keycode))
        {
            foreach (GameObject btn in keyButtons)
            {
                btn.GetComponent<digicodeBtn>().clicked = false;
            }
            if (GameManager.instance.flashKeypad)
            {
                StartCoroutine(flashKeys(Color.green, true));
            }
            else
            {
                StartCoroutine(flashKeys(Color.green, false));
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

    public bool keyPadValidation(float keyCode)
    { 
        if (keycode == 5213 && MissionManager.instance.inLastPuzzle)
        {
            MissionManager.instance.digiFinishPuzzle = true;

            //AudioManager.instance.PlaySound("digiOkSound");

            MissionManager.instance.inLastPuzzle = false;

            return true;
        }
        else if (keycode == 5811 && !code1Used)
        {
            code1Used = true;
            tiroir.SetTrigger("open");
            AudioManager.instance.PlaySound("openDesks");
            AudioManager.instance.PlaySound("digiOkSound");
            return true;
        }
        else if (keycode == 2526 && code1Used)
        {
            door.SetTrigger("open");
            AudioManager.instance.PlaySound("digiOkSound");
            GameManager.instance.doorOpen = true;
            return true;
        }
		else if (keycode == 1211 && MissionManager.instance.hideDigicode && MissionManager.instance.searchJack)
		{
			MissionManager.instance.digiFinishPuzzle = true;
			MissionManager.instance.searchJack = false;

			GameManager.instance.flashKeypad = true;
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
                btn.GetComponent<Renderer>().material.color = Color.grey;
                btn.GetComponent<digicodeBtn>().clicked = false;
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
}

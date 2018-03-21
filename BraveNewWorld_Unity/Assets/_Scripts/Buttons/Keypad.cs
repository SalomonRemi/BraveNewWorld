using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour {

	public GameObject[] keyButtons;
	public GameObject validate;
    public int enabledAmmount;
    public float keycode;

	public List<int> keyPressed = new List<int>();

	void Start ()
    {
        enabledAmmount = 0;
	}
	
	void Update ()
    {
        if(keycode == 0)
        {
            enabledAmmount = 0;
        }

		foreach(GameObject btn in keyButtons)
        {
            if (btn.GetComponent<keyBtn>().clicked)
            {
                btn.GetComponent<Renderer>().material.color = Color.green;
            }

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
		if (MissionManager.instance.ValidateKeypadCode (keyPressed) == true) 
		{
			Debug.Log ("validate");

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
			Debug.Log ("not validate");

			foreach (GameObject btn in keyButtons)
			{
				btn.GetComponent<keyBtn>().clicked = false;
			}
			StartCoroutine(flashKeys(Color.red, true));
			AudioManager.instance.PlaySound("digiError");
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
        resetKeypad();
        yield return null;
    }
}
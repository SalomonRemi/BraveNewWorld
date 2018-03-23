using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyBtn : MonoBehaviour {

    public bool clicked;
    public float btnValue;
	public int buttonIntValue;
    public bool validate;
    public Keypad parent;
	public GameObject doorRelation;

    private Animator doorAnimator;
    private Material originalMat;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<Keypad>();
        originalMat = gameObject.GetComponent<MeshRenderer>().material;

        if(doorRelation != null) doorAnimator = doorRelation.GetComponent<Animator>();
    }


    private void Update()
    {
        if(doorRelation != null)
        {
            if (clicked)
            {
                doorAnimator.SetBool("Open", true);
            }
            else
            {
                doorAnimator.SetBool("Open", false);
            }
        }
    }


    public void enableButton() //APPELLER QUAND ON CLICK, DANS PLAYER INTERACT
    {
		if (validate && parent.keyPressed.Count > 0)
		{ 
			parent.ComfirmInput ();
		} 
		else if(validate && parent.keyPressed.Count == 0) AudioManager.instance.PlaySound("buttonFalse");

		if (parent.enabledAmmount < MissionManager.instance.doorAmmount && !clicked && !validate) 
		{ 
			parent.keyPressed.Add(buttonIntValue);
			parent.enabledAmmount++;
			clicked = true;

			AudioManager.instance.PlaySound ("clickBtn");
		}
		else if (clicked) 
		{
			for (int i = 0; i < parent.keyPressed.Count; i++) 
			{
				if (buttonIntValue == parent.keyPressed[i])
				{
					gameObject.GetComponent<Renderer>().material.color = Color.grey;
					gameObject.GetComponent<keyBtn>().clicked = false;
					parent.keyPressed.RemoveAt(i);

					AudioManager.instance.PlaySound ("clickBtn");
				}
			}
		}
    }
}
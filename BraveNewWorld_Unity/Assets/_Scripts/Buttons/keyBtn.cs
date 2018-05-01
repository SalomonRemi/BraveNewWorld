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

    private Material originalMat;
    private MapDoor likedDoor;

	[HideInInspector] public Animator doorAnimator;
	[HideInInspector] public GameObject support;

    private void Start()
    {
        parent = gameObject.GetComponentInParent<Keypad>();
        originalMat = gameObject.GetComponent<MeshRenderer>().material;

		if (doorRelation != null)
		{
			likedDoor = doorRelation.GetComponent<MapDoor>();
			doorAnimator = doorRelation.GetComponent<Animator>();
		}

		if (!validate) support = transform.GetChild(0).gameObject;
    }


    private void Update()
    {
        if(doorRelation != null)
        {
            if (clicked)
            {
                likedDoor.activateFeedback = true;
            }
            else
            {
                likedDoor.activateFeedback = false;
            }
        }
    }


    public void enableButton() //APPELLER QUAND ON CLICK, DANS PLAYER INTERACT
    {
		if (validate && parent.keyPressed.Count > 0) // VALIDER
		{ 
			parent.ComfirmInput();
		} 
		else if(validate && parent.keyPressed.Count == 0) AudioManager.instance.PlaySound("buttonFalse");

		if (parent.enabledAmmount < MissionManager.instance.doorAmmount && !clicked && !validate)// ACTIVER UN BOUTON
		{ 
			parent.keyPressed.Add(buttonIntValue);
			parent.enabledAmmount++;
			clicked = true;

			AudioManager.instance.PlaySound("clickBtn");
		}
		else if (clicked) // DESACTIVER UN BOUTON
		{
			for (int i = 0; i < parent.keyPressed.Count; i++) 
			{
				if (buttonIntValue == parent.keyPressed[i])
				{
					gameObject.GetComponent<Renderer>().material.color = Color.grey;
					gameObject.GetComponent<keyBtn>().clicked = false;
					parent.keyPressed.RemoveAt(i);

					AudioManager.instance.PlaySound("clickBtn");
				}
			}
		}
    }

	public void SetSupportColor()
	{
		if (clicked) support.GetComponent<MeshRenderer> ().material.color = Color.green;
		else support.GetComponent<MeshRenderer> ().material.color = Color.black;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Fade : MonoBehaviour {

	public float fadeSpeed;

	private Color actualColor;
	private bool doFadingOut = false;
	private Image fadeImage;

	void Start ()
	{
		fadeImage = GetComponent<Image> ();

		actualColor = new Color(0,0,0,1);

		doFadingOut = true;
	}

	void Update () 
	{
		fadeImage.color = actualColor;

		if (doFadingOut) 
		{
			actualColor.a -= fadeSpeed;
		}
	}
}
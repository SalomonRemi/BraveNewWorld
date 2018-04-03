using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElevatorFloorDisplay : MonoBehaviour {

	private int floor;
	private float time;
	private float counter;
	private bool doDisplay;

	private TextMeshPro text;

	void Start ()
	{
		floor = 7;

		time = 5f + MissionManager.instance.timeInElevator;

		text = GetComponent<TextMeshPro>();

		StartCoroutine(displayCoroutine());
	}
	
	void Update () 
	{
		if (doDisplay) 
		{
			counter += Time.deltaTime;
			if (counter >= 2f) 
			{	
				floor++;
				counter = 0;
			}
		}

		if (floor > 0 && floor < 10) {
			text.text = "0" + floor.ToString ();
		} else
			text.text = floor.ToString ();
	}

	private IEnumerator displayCoroutine()
	{
		doDisplay = true;

		yield return new WaitForSeconds (time);

		doDisplay = false;
	}
}

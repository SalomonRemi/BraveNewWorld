using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HorlogeChecker : MonoBehaviour {

    public TextMeshPro displayText;

    public int hourValue;
    public int minuteValue;

    private float timeCountMinute = 0;
	
	void Update ()
    {
        if(minuteValue < 10)
            displayText.text = hourValue.ToString() + " H 0" + minuteValue.ToString();
        else
            displayText.text = hourValue.ToString() + " H " + minuteValue.ToString();

        timeCountMinute += Time.deltaTime;

        if (timeCountMinute >= 60)
        {
            minuteValue++;
            timeCountMinute = 1;
        }

        if (minuteValue == 60)
        {
            minuteValue = 1;
            hourValue++;
        }
	}
}

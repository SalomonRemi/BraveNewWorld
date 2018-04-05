using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingScreen : MonoBehaviour {

    private TextMeshPro displayText;
    private float counter;
    private int state;

	void Start ()
    {
        displayText = GetComponent<TextMeshPro>();

        state = 0;
    }
	

	void Update ()
    {
        counter += Time.deltaTime;

        if(counter >= 1f)
        {
            if (state != 4) state++;
            else state = 0;

            counter = 0;
        }

        if (state == 0) displayText.text = "CHARGEMENT";
        else if (state == 1) displayText.text = "CHARGEMENT .";
        else if (state == 2) displayText.text = "CHARGEMENT ..";
        else if (state == 3) displayText.text = "CHARGEMENT ...";
    }
}
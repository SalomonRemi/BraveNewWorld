﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockerNum : MonoBehaviour {

    public int goodNumber;

    [HideInInspector] public bool isGoodNumber;

    private int actualNumber;
    private TextMeshPro displayText;


    private void Start()
    {
        displayText = gameObject.GetComponentInChildren<TextMeshPro>();
    }


    private void Update()
    {
        displayText.text = actualNumber.ToString();

        if (goodNumber == actualNumber) isGoodNumber = true;
        else isGoodNumber = false;
    }


    public void IncreaseNumber()
    {
        if (actualNumber == 9)
        {
            actualNumber = 0;
        }
        else actualNumber++;
    }

    public void DecreaseNumber()
    {
        if (actualNumber == 0)
        {
            actualNumber = 9;
        }
        else actualNumber--;
    }
}
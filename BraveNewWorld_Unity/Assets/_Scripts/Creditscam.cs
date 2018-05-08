using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creditscam : MonoBehaviour {

	public bool isEnding;

    private void Update()
    {
        if (isEnding) FindObjectOfType<MenuManager>().LoadSmallLevel("Menu");
    }
}

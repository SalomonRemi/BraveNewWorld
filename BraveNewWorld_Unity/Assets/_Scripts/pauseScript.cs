using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseScript : MonoBehaviour {

	public void exitGame(){
		Application.Quit ();
	}

	public void gotoMenu(){
		Application.LoadLevel ("Menu_Immersif");
	}

	public void continuetoPlay(){
		GameManager.instance.quitPause = true;
	}
}

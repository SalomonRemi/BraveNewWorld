using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	public Image btn;
	public Image play_btn;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseOver (){
		btn.enabled = false;
		play_btn.enabled = true;

		if (Input.GetMouseButtonDown(0)){
			Application.LoadLevel("MoodRoom");
		}
	}

	void OnMouseExit (){
		if (play_btn.enabled == true){
			btn.enabled = true;
			play_btn.enabled = false;
		}
	}
}

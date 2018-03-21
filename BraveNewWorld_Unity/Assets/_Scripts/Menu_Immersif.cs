using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class Menu_Immersif :  EventTrigger {

	void Update(){
		if (GameManager.instance) {
			Destroy (GameManager.instance);
		}
	}
	public void launchGame ()
	{
		Application.LoadLevel("MainRoomScene");
		//SceneManager.LoadScene("MoodRoom", LoadSceneMode.Single);
	}

	public void exitGame (){
		Application.Quit();
	}

	public void changeText(){
		gameObject.GetComponentInChildren<TextMeshProUGUI> ().color = Color.yellow;
	}

	public void originalColor(){
		gameObject.GetComponentInChildren<TextMeshProUGUI> ().color = Color.white;
	}

	/*public override void OnPointerEnter(PointerEventData data)
	{
		btn1.GetComponent<TextMeshProUGUI> ().color = Color.green;
		btn2.GetComponent<TextMeshProUGUI> ().color = Color.green;
	}
*/
}
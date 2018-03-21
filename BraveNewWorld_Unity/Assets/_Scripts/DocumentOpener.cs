using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentOpener : MonoBehaviour {

	public Sprite doc;
	public GameObject docObj;
    public string displayName;

	private Vector2 size;
	private Vector2 screenSize;


	void Start ()
    {
		screenSize = new Vector2 (Screen.currentResolution.width, Screen.currentResolution.height);
	}
	

	void Update ()
    {
		if (!GameManager.instance.documentOpen)
        {
			docObj.SetActive (false);
		}
        else
        {
			docObj.SetActive (true);
		}
	}

	public void CreateDoc()
    {
		Sprite img = doc;
		size = new Vector2 (img.border.x, img.border.y);

			//maxX(1800)   // maxY(900)
		/*docObj.GetComponent<Image> ().sprite = img;
		docObj.GetComponent<Image> ().SetNativeSize ();
		 * if (size.x > 1800 || size.y > 900) {
			while (size.x > 1800 || size.y > 900) {
				size.x--;
				size.y--;
			}
		} else {
			while (size.x < 1800 || size.y < 900) {
				size.x++;
				size.y++;
				if (size.y == 900)
					break;
			}
		}
		print (size);
		docObj.GetComponent<RectTransform>().sizeDelta = size;*/

		docObj.GetComponent<Image> ().sprite = img;
		docObj.GetComponent<Image> ().preserveAspect = true;
		GameManager.instance.documentOpen = true;
	}
}

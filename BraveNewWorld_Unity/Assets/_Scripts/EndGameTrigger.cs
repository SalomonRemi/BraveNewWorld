using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour {

    public Image fadeImage;

    private Fade fade;

	private MenuManager mm;

    private void Start()
    {
        fade = fadeImage.gameObject.GetComponent<Fade>();

		mm = FindObjectOfType<MenuManager> ();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            fade.doFadingOut = false;
			StartCoroutine (EndCoroutine ());
        }
    }

	IEnumerator EndCoroutine()
	{
		yield return new WaitForSeconds (2f);

		mm.LoadSmallLevel ("EndSceneInProgress");

		yield return null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameTrigger : MonoBehaviour {

    public Image fadeImage;

    private Fade fade;

    private void Start()
    {
        fade = fadeImage.gameObject.GetComponent<Fade>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            fade.doFadingOut = false;
        }
    }
}

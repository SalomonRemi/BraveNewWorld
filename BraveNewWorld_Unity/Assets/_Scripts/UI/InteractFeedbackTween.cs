using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class InteractFeedbackTween : MonoBehaviour {

    private bool isAnImage;
    private bool doTween;

    Color startCol = new Color(1, 1, 1, 0.2f);

    Image img;


    private void Update()
    {
        if(doTween == true)
        {
            //StartCoroutine(TweeningColor(img.color));
            DoTween(img.color);
        }
    }


    private void DoTween(Color tweeningColor)
    {
        img.color = startCol;

        LeanTween.value(tweeningColor.a, 1, .2f)
        .setOnUpdate((float val) =>
        {
            tweeningColor.a = val;
        });
    }


    private void OnEnable()
    {
        if (gameObject.GetComponent<Image>()) isAnImage = true;

        if (isAnImage)
        {
            img = GetComponent<Image>();

            doTween = true;
        }
        else
        {

        }
    }


    IEnumerator TweeningColor(Color tweeningColor)
    {
        Color col = startCol;

        LeanTween.value(tweeningColor.a, 1, .2f)
        .setOnUpdate((float val) =>
        {
            tweeningColor.a = val;
        });

        Debug.Log(tweeningColor.a);

        yield return new WaitForSeconds(.3f);

        doTween = false;
    }
}

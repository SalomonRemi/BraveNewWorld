using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightSelf : MonoBehaviour {

	public bool isSelected = false;
	public GameObject camObject;
	public bool activateChild = true;

	private bool enabled = false;

	private HighlightsFX highlight;


	void Start ()
    {
		highlight = camObject.GetComponentInChildren<HighlightsFX> ();
	}
	

	void Update ()
    {
		if (isSelected)
        {
			if (!enabled)
            {
				StartCoroutine(setHightlight ());
			}
		}
        else
        {
			if (enabled)
            {
				removeHighlight ();
			}
		}
		
	}


	IEnumerator setHightlight()
    {
		enabled = true;
		List<Renderer> children = new List<Renderer> ();
		Renderer[] childrensR;
		childrensR = gameObject.GetComponentsInChildren<Renderer> ();

		if (childrensR[0] != null && activateChild)
        {
			foreach (Renderer r in childrensR)
            {
				children.Add (r);
			}
			if (gameObject.GetComponent<Renderer> ())
            {
				children.Add (gameObject.GetComponent<Renderer> ());
			}
		}
        else
        {
			children.Add (gameObject.GetComponent<Renderer> ());
		}
		yield return 0;
		highlight.AddRenderers (children, Color.white, HighlightsFX.SortingType.DepthFiltered);
	}

	public void removeHighlight ()
    {
		enabled = false;
		highlight.ClearOutlineData ();
	}
}
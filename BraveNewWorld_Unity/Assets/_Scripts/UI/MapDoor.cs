using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoor : MonoBehaviour {

    [HideInInspector] public bool activateFeedback;

    public GameObject likedWave;

    private List<GameObject> childObj = new List<GameObject>();

	void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childObj.Add(transform.GetChild(i).gameObject);
        }
	}

    private void Update()
    {
        if (activateFeedback) OpenFeedBacks();
        if (!activateFeedback) CloseFeedBacks();
    }

    private void OpenFeedBacks()
    {
        foreach (GameObject door in childObj)
        {
            Renderer renderer = door.GetComponent<Renderer>();
            Material mat = renderer.material;

            mat.color = Color.green;
            mat.SetColor("_EmissionColor", Color.green);

            float emission = Mathf.PingPong(Time.time * 2, 1.0f);
            Color baseColor = Color.green;

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);

            likedWave.SetActive(true);
        }
    }

    private void CloseFeedBacks()
    {
        foreach (GameObject door in childObj)
        {
            Renderer renderer = door.GetComponent<Renderer>();
            Material mat = renderer.material;

            mat.color = Color.red;
            mat.SetColor("_EmissionColor", Color.red);

            float floor = 0.7f;
            float ceiling = 1.0f;
            float emission = floor + Mathf.PingPong(Time.time / 2, ceiling - floor);

            Color baseColor = Color.red;

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);

            likedWave.SetActive(false);
        }
    }
}
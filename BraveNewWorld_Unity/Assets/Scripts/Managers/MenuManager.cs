using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	[Header("Menu and loading")]
    public GameObject loadingScreen;
    public GameObject uiMenu;
    public Slider slider;

	public void LoadSmallLevel(string name)
	{
		SceneManager.LoadScene(name);
	}

	public void LoadLevel(string name)
    {
        StartCoroutine(LoadSceneAsynchro(name));

        uiMenu.SetActive(false);
        loadingScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsynchro(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress);

            slider.value = progress;

            yield return null;
        }
    }
}
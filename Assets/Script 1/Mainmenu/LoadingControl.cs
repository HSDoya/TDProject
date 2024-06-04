using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingControl : MonoBehaviour
{
    public GameObject LoadingScreen;
    public Image LoadingBarFill;



  

    public void LoadScene(int sceneId)
    {
    
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        // Activate the loading screen
        LoadingScreen.SetActive(true);

        // Fake loading time
        float fakeLoadingTime = 3f; // 3 seconds
        float elapsedFakeTime = 0f;

        // Update the loading bar fill amount during the fake loading time
        while (elapsedFakeTime < fakeLoadingTime)
        {
            elapsedFakeTime += Time.deltaTime;
            float fakeProgressValue = Mathf.Clamp01(elapsedFakeTime / fakeLoadingTime);
            LoadingBarFill.fillAmount = fakeProgressValue;
            yield return null;
        }

        // Start the actual loading operation
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        // Continue updating the loading bar fill amount based on the actual load progress
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}



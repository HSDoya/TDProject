using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField]
    private GameObject PauseUI;

    public string thisScene;


    private void Awake()
    {
        thisScene = SceneManager.GetActiveScene().name;
        PauseUI.SetActive(false);
    }
    // 게임을 멈추는 함수
    public void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    // 게임을 다시 시작하는 함수
   public void ResumeGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(thisScene);
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }
    public void stagemenu()
    {
        SceneManager.LoadSceneAsync(1);
        PauseUI.SetActive(false);
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField]
    private GameObject PauseUI;


    private void Awake()
    {
        PauseUI.SetActive(false);
    }
    // 게임을 멈추는 함수
    void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    // 게임을 다시 시작하는 함수
   void ResumeGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}

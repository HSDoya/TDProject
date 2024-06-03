using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;


    // 게임을 멈추는 함수
    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    // 게임을 다시 시작하는 함수
    private void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}

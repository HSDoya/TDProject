using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;


    // ������ ���ߴ� �Լ�
    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;
    }

    // ������ �ٽ� �����ϴ� �Լ�
    private void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;
    }
}

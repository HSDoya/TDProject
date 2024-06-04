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
    // ������ ���ߴ� �Լ�
    void PauseGame()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }

    // ������ �ٽ� �����ϴ� �Լ�
   void ResumeGame()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}

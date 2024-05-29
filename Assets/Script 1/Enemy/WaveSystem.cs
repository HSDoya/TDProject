using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; // 현재 스테이지의 모든 웨이브 정보 
    [SerializeField]
    private EnemySpawn enemyspawner;
    [SerializeField]
    private GameObject gameClearUI; // 게임 클리어 UI
    [SerializeField]
    private GameObject gameOverUI;
    [SerializeField]
    private PlayerHP playerHp;

    private int currentWaveIndex = -1; // 현재 웨이브 인덱스 
    public int CurrentWave => currentWaveIndex + 1; // 시작이 0이기 때문에 +1
    public int MaxWave => waves.Length;

    private void Update()
    {
        // 게임 클리어를 확인하는 로직을 매 프레임마다 체크
        CheckGameClear();
    }

    public void StartWave()
    {
        // 현재 적이 없고 wave가 남아 있으면
        if (enemyspawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            // 인덱스의 시작이 -1 때문에 웨이브 인덱스 증가를 제일 먼저 함 
            currentWaveIndex++;
            // 현재 웨이브 정보 제공
            enemyspawner.StartWave(waves[currentWaveIndex]);
        }
    }

    private void CheckGameClear()
    {
        // 모든 웨이브가 끝났고, 현재 적이 없다면 게임 클리어
        if (currentWaveIndex >= waves.Length - 1 && enemyspawner.EnemyList.Count == 0)
        {
           
           if(playerHp.CurrentHP <= 0)
            {
                ActivateGameOverUI();
            }
            else
            {
                ActivateGameClearUI();
            }
           
        }
    }

    private void ActivateGameClearUI()
    {
        // 게임 클리어 UI 활성화
        if (gameClearUI != null)
        {
            gameClearUI.SetActive(true);
        }
    }
    private void ActivateGameOverUI()
    {
        if(gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }
    }

}
[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int MaxEnemyCount;
    public GameObject[] enemyPrefabs;
}

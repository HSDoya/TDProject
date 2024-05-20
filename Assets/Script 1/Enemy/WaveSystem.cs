using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves; //현재 스테이지의 모든 웨이브 정보 
    [SerializeField]
    private EnemySpawn enemyspawner;
    private int currentWaveIndex = -1; //현재 웨이브 인덱스 
    public int CurrentWave => currentWaveIndex + 1; //시작이 0이기 때문에 +1
    public int MaxWave => waves.Length;


    public void StartWave()
    {
        //현재 적이 없고 wave가 남아 있으면
        if (enemyspawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            //인덱스의 시작이 -1 때문에 웨이브 인덱스 증가를 제일 먼저 함 
            currentWaveIndex++;
            //현재 웨이브 정부 제공
            enemyspawner.StartWave(waves[currentWaveIndex]);
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

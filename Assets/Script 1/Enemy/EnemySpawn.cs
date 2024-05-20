using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private int currentEnemyCount;
    public int CurrentEnemyCount => currentEnemyCount;
    public int MaxEnemyCount => currentWave.MaxEnemyCount;

    private Wave currentWave;

    [SerializeField]
    private Transform[] wayPoints; // 현재 스테이지의 이동경로
    private List<Enemy> enemyList;//현재 맵에 존재하는 모든 적의 정보 
    public List<Enemy> EnemyList => enemyList; //적의 생성과 삭제는 EnemySpawner에서 하기 때문에 Set은 필요 없음 
    [SerializeField]
    private GameObject enemyHPSlider;
    [SerializeField]
    private Transform canvasTransform;
    [SerializeField]
    private PlayerHP playerHP;
    [SerializeField]
    private PlayerGold playerGold;

    private void Awake()
    {
        enemyList = new List<Enemy>();
            
    }
    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;
        while (spawnEnemyCount < currentWave.MaxEnemyCount)
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();
            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);
            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;

            yield return new WaitForSeconds(currentWave.spawnTime);
        }


    }
    public void DestoroyEnemy(EnemyDestoryType type, Enemy enemy, int Gold)
    {
        if (type == EnemyDestoryType.Arrive)
        {
            playerHP.TakeDamage(1);
        }
        else if (type == EnemyDestoryType.kill)
        {
            playerGold.CurrentGold += Gold;
        }
        enemyList.Remove(enemy);
        currentEnemyCount--;

        //리스트에서 사망하는 적 정보 삭제 
        enemyList.Remove(enemy);
        //적 오브젝트 삭제 
        Destroy(enemy.gameObject);
    }
    public void SpawnEnemyHPSlider(GameObject enemy)
    {
        //적 체력을 나타내는 ui 생성
        GameObject SliderClone = Instantiate(enemyHPSlider);
        //Slider 오브젝트를 parent("canves"오브젝트)의 자식으로 설정 
        //캔버스 자식으로 설정해야 화면에 보임 
        SliderClone.transform.SetParent(canvasTransform);
        //계층 설정으로 바뀐 크기를 다시 (1,1,1)로 설정
        SliderClone.transform.localPosition = Vector3.one;
        //slider가 쫒아다닐 대상을 본인으로 설정
        SliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        //체력 정보 표시하도록 설정
        SliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }

    public void StartWave(Wave wave)
    {
        //현재 웨이브에서 생성한 적 숫자
        currentWave = wave;
        currentEnemyCount = currentWave.MaxEnemyCount;
        StartCoroutine("SpawnEnemy");
    }

}

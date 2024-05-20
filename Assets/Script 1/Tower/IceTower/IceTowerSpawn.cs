using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTowerSpawn : MonoBehaviour
{
    [SerializeField]
    private IceTowerTemplete towerTemplate;
    [SerializeField]
    private EnemySpawn enemySpawn;

    [SerializeField]
    private PlayerGold playerGold;
    [SerializeField]
    private SystemTextViewer systemTextviewer;
    private bool isOnTowerButton = false;
    private GameObject followTowerClone = null;

    public bool click_icetower;
  

    private void Awake()
    {
        click_icetower = false;
    }
    public void ReadyToSpawnTower()
    {
        if (isOnTowerButton == true)
        {
            return;
        }
        if (towerTemplate.wapons[0].cost_ice > playerGold.CurrentGold)
        {
            systemTextviewer.PrintText(SystemType.Money);
            return;
        }
        isOnTowerButton = true;
        click_icetower = true;
        
        followTowerClone = Instantiate(towerTemplate.followTowerPrefab);

        StartCoroutine("OnTowerCancelSystem");
    }
    public void SpawnTower(Transform tileTransform)
    {

        if (isOnTowerButton == false) //타워 건설 ui사용 전까지 사용 
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            systemTextviewer.PrintText(SystemType.Build); // 시스템 뷰어 추가시 추가 
            return;
        }

        //다시 타워 건설 버튼을 눌러서 타워를 건설하도록 변수 설정-> 타워 건설 ui추가시 추가 
        isOnTowerButton = false;
        //타워가 건설되어 있음으로 설정
        tile.IsBuildTower = true;
        click_icetower = false;


        //선택한 타일의 위치에 타워건설(타일보다 Z축 -1위치에 배치)-> 타워 패널 ui 추가시 추가 
        Vector3 position = tileTransform.position + Vector3.back;

        playerGold.CurrentGold -= towerTemplate.wapons[0].cost_ice; // 타워 템플릿 추가시 사용 



        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity); // 타워 템플릿 추가시 추가 

        //타워 무기에 enemyspwer정보 전달 
        clone.GetComponent<IceTowerWeapon>().Setup(enemySpawn, playerGold, tile);

        Destroy(followTowerClone);
        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {

        while (true)
        {
            //ESC가 또는 마우스 오른쪽 버튼을 눌렀을 떄 타워 건설 취소
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isOnTowerButton = false;
                Destroy(followTowerClone);
                click_icetower = false;
                break;
            }
            yield return null;
        }

    }
}

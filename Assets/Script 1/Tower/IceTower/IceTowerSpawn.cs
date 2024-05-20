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

        if (isOnTowerButton == false) //Ÿ�� �Ǽ� ui��� ������ ��� 
        {
            return;
        }
        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            systemTextviewer.PrintText(SystemType.Build); // �ý��� ��� �߰��� �߰� 
            return;
        }

        //�ٽ� Ÿ�� �Ǽ� ��ư�� ������ Ÿ���� �Ǽ��ϵ��� ���� ����-> Ÿ�� �Ǽ� ui�߰��� �߰� 
        isOnTowerButton = false;
        //Ÿ���� �Ǽ��Ǿ� �������� ����
        tile.IsBuildTower = true;
        click_icetower = false;


        //������ Ÿ���� ��ġ�� Ÿ���Ǽ�(Ÿ�Ϻ��� Z�� -1��ġ�� ��ġ)-> Ÿ�� �г� ui �߰��� �߰� 
        Vector3 position = tileTransform.position + Vector3.back;

        playerGold.CurrentGold -= towerTemplate.wapons[0].cost_ice; // Ÿ�� ���ø� �߰��� ��� 



        GameObject clone = Instantiate(towerTemplate.towerPrefab, position, Quaternion.identity); // Ÿ�� ���ø� �߰��� �߰� 

        //Ÿ�� ���⿡ enemyspwer���� ���� 
        clone.GetComponent<IceTowerWeapon>().Setup(enemySpawn, playerGold, tile);

        Destroy(followTowerClone);
        StopCoroutine("OnTowerCancelSystem");
    }

    private IEnumerator OnTowerCancelSystem()
    {

        while (true)
        {
            //ESC�� �Ǵ� ���콺 ������ ��ư�� ������ �� Ÿ�� �Ǽ� ���
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

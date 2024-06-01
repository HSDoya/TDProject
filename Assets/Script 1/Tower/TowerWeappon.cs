using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum WeaponState { SearchTarget = 0, AttackToTarget } // 열거형 
public class TowerWeappon : MonoBehaviour
{
    [SerializeField]
    private TowerTemplater towerTemplate;
    [SerializeField]
    private GameObject projectileprefab;
    [SerializeField]
    private Transform spawnPoint;

    private WeaponState weaponState = WeaponState.SearchTarget; //타워 무기의 상태 
    private Transform attackTarget = null;
    private EnemySpawn enemySpawner;
    private int level = 0;

    private SpriteRenderer spriteRenderer;
    private PlayerGold playerGold;
    private Tile ownertile;
    
    public Sprite TowerSprite => towerTemplate.wapons[level].sprit;
    public float Damage => towerTemplate.wapons[level].damage;
    public float Rate => towerTemplate.wapons[level].rate;
    public float Range => towerTemplate.wapons[level].range;
    public int Level => level + 1;

    public int MaxLevel => towerTemplate.wapons.Length;

    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip fireSound;



    public void Setup(EnemySpawn enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownertile = ownerTile;
        ChangeState(WeaponState.SearchTarget);

    }
    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());
        weaponState = newState;
        StartCoroutine(weaponState.ToString());
    }
    private void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }
    private void RotateToTarget() // 타워가 적을 바라보게 하는 함수 
    {
        //원점으로부터의 거리와 수평축으로부터의 각도를 이용해 위치를 구하는 극 좌표계 이용 
        //각도 = arctan(y/x)
        //x,y 변위값 구하기 
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;
        //x,y 변위값을 바탕으로 구하는 각도 
        //각도가 radian단위이기 떄문에 Mathf.Rad2deg를 곱해 도 단위를 구함 
        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degree);
    }
    private IEnumerator SearchTarget()
    {
        while (true)
        {
            //제일 가까운 적을 찾기 위해 최초 거리를 최대한 크게 설정 
            float closetDistSqr = Mathf.Infinity;
            for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
            {
                float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

                if (distance <= towerTemplate.wapons[level].range && distance <= closetDistSqr)
                {
                    closetDistSqr = distance;
                    attackTarget = enemySpawner.EnemyList[i].transform;
                }
            }

            if (attackTarget != null)
            {
                ChangeState(WeaponState.AttackToTarget);
            }
            yield return null;
        }
    }
    private IEnumerator AttackToTarget()
    {
        while (true)
        {
            //1.타겟이 있는지 검사 
            if (attackTarget == null)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            //2. 공격 범위 안에 있는지 검사 
            float distance = Vector3.Distance(attackTarget.position, transform.position);
            if (distance > towerTemplate.wapons[level].range)
            {
                attackTarget = null;
                ChangeState(WeaponState.SearchTarget);
                break;
            }
            yield return new WaitForSeconds(towerTemplate.wapons[level].rate);
            //공격
            spawnProjectile();
        }
    }
    private void spawnProjectile()
    {
        audioSource.PlayOneShot(fireSound);
        GameObject clone = Instantiate(projectileprefab, spawnPoint.position, Quaternion.identity);
        clone.GetComponent<Bullet>().Setup(attackTarget, towerTemplate.wapons[level].damage);
    }
    public bool Upgrade() // 타워 템플릿 추가시 추가 
    {
        // 타워 업그레이드에 필요한 골드가 충분한지 검사 
        if (playerGold.CurrentGold < towerTemplate.wapons[level + 1].cost)
        {
            return false;
        }
        // 타워 레벨 증가
        level++;
        //타워 외형 변경 
        spriteRenderer.sprite = towerTemplate.wapons[level].sprit;
        //골드 차감
        playerGold.CurrentGold -= towerTemplate.wapons[level].cost;

        return true;

    }
    public void Sell() // 판매 시스템 추가시 추가  
    {
        playerGold.CurrentGold += towerTemplate.wapons[level].sell;

        ownertile.IsBuildTower = false;

        Destroy(gameObject);
    }


}

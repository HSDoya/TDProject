using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public enum EnemyDestoryType { kill = 0, Arrive }


public class Enemy : MonoBehaviour
{
    private int wayPointCount;  //이동 경로 개수
    private Transform[] wayPoint; // 이동 경로 정보 
    private int currentIndex = 0; // 현재 목표지점 인덱스
    private Movement movement2D; // 오브젝트 이동 제어 
    private EnemySpawn enemySpawner; //적의 삭제를 본인이 하지 않고 EnemySpawner에 알려서 삭제 

    private SpriteRenderer render;
    [SerializeField]
    private int Gold = 10;
    private float slowbullet;

    private IceBullet iceBullet;

    private EnemyHP enemyhp;

    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        anim.SetBool("isDie", false);
        enemyhp = GetComponent<EnemyHP>();
    }

    public void Update()
    {
        if (movement2D.isSlow)
        {
            render.color = Color.blue;
            if(enemyhp.CurrentHP <= 0)
            {
                movement2D.isSlow = false;
            }
           
        }
        else 
        {
            render.color = Color.white;
        }
    }
    public void Setup(EnemySpawn enemySpawner, Transform[] wayPoint)
    {
        this.enemySpawner = enemySpawner;

        movement2D = GetComponent<Movement>();

        //적 이동 경로 설정 
        wayPointCount = wayPoint.Length;
        this.wayPoint = new Transform[wayPointCount];
        this.wayPoint = wayPoint;
        //적 위치를 첫번째 위치로 설정 
        transform.position = wayPoint[currentIndex].position;
        //적 이동/ 목표지점 설정 코루틴 함수 시작 
        StartCoroutine("OnMove");

    }
 
    private IEnumerator OnMove()
    {
        NextMoveTo();
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoint[currentIndex].position) < 0.2f * movement2D.MoveSpeed)
            {

                NextMoveTo();

            }
            yield return null;
        }
    }


    private void NextMoveTo()
    {
        //아직 waypoint가 남아있다면 
        if (currentIndex < wayPointCount - 1)
        {
            //적의 위치를 정확하게 목표 위치로 설정
            transform.position = wayPoint[currentIndex].position;
            //이동 방향 설정 => 다음 목표지점 
            currentIndex++;
            Vector3 direction = (wayPoint[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            Gold = 0;
            OnDie(EnemyDestoryType.Arrive);
        }
    }
    public void OnDie(EnemyDestoryType type)
    {

        StartCoroutine(DieAnmation(type));
      
    }

    private IEnumerator DieAnmation(EnemyDestoryType type)
    {
        movement2D.MoveSpeed = 0f;
        anim.SetBool("isDie", true);
        render.color = Color.white;
        yield return new WaitForSeconds(0.5f);
        enemySpawner.DestoroyEnemy(type, this, Gold);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerTemplater : ScriptableObject
{
    public GameObject towerPrefab; // 타워 생성을 위한 프리펩 

    public Weapon[] wapons; //레벨별 타워 정보 
    public GameObject followTowerPrefab;


    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprit; // 보여지는 타워 이미지UI
        public float damage; // 공격력 
        public float rate; // 공격속도
        public float range; // 공격 범위 
        public int cost; // 필요 골드 
        public int sell;


    }


}

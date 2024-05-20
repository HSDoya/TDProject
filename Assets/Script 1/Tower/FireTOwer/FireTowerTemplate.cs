using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class FireTowerTemplate : ScriptableObject
{
    public GameObject towerPrefab; // 타워 생성을 위한 프리펩 
    public fireWeapon[] wapons; //레벨별 타워 정보 
    public GameObject followTowerPrefab;

    [System.Serializable]
    public struct fireWeapon
    {
        public Sprite sprit; // 보여지는 타워 이미지UI
        public float damage_fire; // 공격력 
        public float rate_fire; // 공격속도
        public float range_fire; // 공격 범위 
        public float burn;// 
        public int cost_fire; // 필요 골드 
        public int sell;


    }
}

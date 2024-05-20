using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class IceTowerTemplete : ScriptableObject
{
    public GameObject towerPrefab; // 타워 생성을 위한 프리펩 
    public IceWeapon[] wapons; //레벨별 타워 정보 
    public GameObject followTowerPrefab;
  
    [System.Serializable]
    public struct IceWeapon
    {
        public Sprite sprit; // 보여지는 타워 이미지UI
        public float damage_ice; // 공격력 
        public float rate_Ice; // 공격속도
        public float range_Ice; // 공격 범위 
        public float slowdown;// 
        public int cost_ice; // 필요 골드 
        public int sell;


    }

}

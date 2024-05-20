using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireDataView : MonoBehaviour
{
    [SerializeField]
    private Image imageTower;
    [SerializeField]
    private TextMeshProUGUI textDamage;
    [SerializeField]
    private TextMeshProUGUI textRate;
    [SerializeField]
    private TextMeshProUGUI textRange;
    [SerializeField]
    private TextMeshProUGUI textLevel;

    private FireTowerWeapon currentTower;

    [SerializeField]
    private TowerAttackRange TowerAttackRange;

    [SerializeField]
    private Button buttonUpgrade;
    [SerializeField]
    private SystemTextViewer systemTextViewer;
    [SerializeField]
    private IceTowerDataView IceTowerDataView;
    [SerializeField]
    private TowerDataViewer towerDataViewer;


    public void OnPanel(Transform towerWeapon)
    {
        IceTowerDataView.OffPanel_ice();
        towerDataViewer.OffPanel();

        //출력해야하는 타워 정보를 받아와서 저장 -> UI연동때 추가 
        currentTower = towerWeapon.GetComponent<FireTowerWeapon>();
        //타워 정보 패널 키기
        gameObject.SetActive(true);
        UpdatetowerData();// ui연동때 추가  

        TowerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);

    }
    private void UpdatetowerData() // ui 연동떄 추가 
    {
        imageTower.sprite = currentTower.TowerSprite;
        textDamage.text = "Damage :" + currentTower.Damage;
        textRate.text = "Rate :" + currentTower.Rate;
        textRange.text = "Range :" + currentTower.Range;
        textLevel.text = "Level :" + currentTower.Level;

        buttonUpgrade.interactable = currentTower.Level < currentTower.MaxLevel ? true : false;

    }


    private void Awake()
    {
        OffPanel();

    }
    public void OnPanel()
    {
        gameObject.SetActive(true);
    }
    public void OffPanel()
    {

        gameObject.SetActive(false);
        TowerAttackRange.OffAttackRange();

    }
    public void OnClickEventTowerUpgrade() //타워 업그레이드 시스템 추가때 추가 
    {
        //타워 업그레이드 시도( 성공: true 실패: flase)
        bool isSuccess = currentTower.Upgrade();
        if (isSuccess == true)
        {
            //타워가 업그레이드 되었기 때문에 정보갱신
            UpdatetowerData();
            // 타워 주변에 보이는 공격범위도 갱신 
            TowerAttackRange.OnAttackRange(currentTower.transform.position, currentTower.Range);
        }
        else
        {
            systemTextViewer.PrintText(SystemType.Money);
        }
    }
    public void OnClickEventTowerSell()
    {
        currentTower.Sell();
        OffPanel();
    }
}

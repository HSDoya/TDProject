using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP; // 플레이어 체력 ui
    [SerializeField]
    private PlayerHP playerHp; // 플레이어 체력정보 


    [SerializeField]
    private TextMeshProUGUI textPlayerGold;
    [SerializeField]
    private PlayerGold PlayerGold;
    [SerializeField]
    private TextMeshProUGUI textWave;
    [SerializeField]
    private WaveSystem wavesystem;
    [SerializeField]
    private TextMeshProUGUI textEnemyCount;
    [SerializeField]
    private EnemySpawn enemyspawner;



    private void Update()
    {
        textPlayerHP.text = playerHp.CurrentHP + "/" + playerHp.MaxHP;
        textPlayerGold.text = PlayerGold.CurrentGold.ToString();
        textWave.text = wavesystem.CurrentWave + "/" + wavesystem.MaxWave;
        textEnemyCount.text = enemyspawner.CurrentEnemyCount + "/" + enemyspawner.MaxEnemyCount;

    }

}

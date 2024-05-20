using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPViewer : MonoBehaviour
{
    private EnemyHP enemyhp;
    private Slider hpSlider;
    public void Setup(EnemyHP enemyhp)
    {
        this.enemyhp = enemyhp;
        hpSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        hpSlider.value = enemyhp.CurrentHP / enemyhp.MaxHP;
    }

}

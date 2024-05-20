using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 20; //최대 체력
    private float currentHP; // 현재 체력 
    [SerializeField]
    private Image imageScreen;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;
    private void Awake()
    {
        currentHP = maxHP;
    }
    public void TakeDamage(float damage)
    {
        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        //체력 감수
        currentHP -= damage;
        //체력이 0이 되면 게임오버 
        if (currentHP <= 0)
        {
        }
    }
    private IEnumerator HitAlphaAnimation()
    {
        //전체화면 크기로 배치된 image의 색상을 color변수에 저장 
        // 투명도 40%설정
        Color color = imageScreen.color;
        color.a = 0.4f;
        imageScreen.color = color;
        //투명도가 0%가 될떄까지
        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            imageScreen.color = color;
            yield return null;
        }
    }

}

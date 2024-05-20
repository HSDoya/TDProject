using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowMousePosition : MonoBehaviour
{
    private Camera maincamera;
    private void Awake()
    {
        maincamera = Camera.main;
    }
    private void Update()
    {
        //화면의 마우스 좌표를 기준으로 게임 월드 상의 좌표를 구함
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.position = maincamera.ScreenToWorldPoint(position);
        //z위치를 0으로 설정
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

}

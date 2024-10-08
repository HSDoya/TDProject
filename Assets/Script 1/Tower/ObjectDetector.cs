﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField]
    private TowerSpawn towerspawner;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hit;
    [SerializeField]
    private TowerDataViewer towerDateViewer;
    [SerializeField]
    private IceTowerSpawn icetowerspawn;
    [SerializeField]
    private IceTowerDataView icetowerdataview;
    [SerializeField]
    private FireTowerSpawn fireTowerSpawn;
    [SerializeField]
    private FireDataView fireDataView;




    private void Awake()
    {
        mainCamera = Camera.main;
        
    }
   
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == true)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                HandleInput(touch.position);
            }
        }
       

    }
    private void HandleInput(Vector3 inputPosition)
    {
        ray = mainCamera.ScreenPointToRay(inputPosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //광성에 부딪히는 객체의 tag가 tile면 
            if (hit.transform.CompareTag("Tile"))
            {
                //타워를 생성
                if (towerspawner.click_towerspawn)
                {
                    towerspawner.SpawnTower(hit.transform);
                }
                else if (icetowerspawn.click_icetower)
                {
                    icetowerspawn.SpawnTower(hit.transform);
                }
                else if (fireTowerSpawn.click_firetowerspawn)
                {
                    fireTowerSpawn.SpawnTower(hit.transform);
                }
            }
            else if (hit.transform.CompareTag("Tower"))
            {
                towerDateViewer.OnPanel(hit.transform);
            }
            else if (hit.transform.CompareTag("IceTower"))
            {
                icetowerdataview.OnPanel(hit.transform);
            }
            else if (hit.transform.CompareTag("FireTower"))
            {
                fireDataView.OnPanel(hit.transform);
            }
        }
    }

}

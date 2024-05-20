using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SystemType { Money = 0, Build }

public class SystemTextViewer : MonoBehaviour
{
    private TextMeshProUGUI textSystem;
    private TMPAlpha tMPAlpha;
    private void Awake()
    {
        textSystem = GetComponent<TextMeshProUGUI>();
        tMPAlpha = GetComponent<TMPAlpha>();
    }
    public void PrintText(SystemType type)
    {
        switch (type)
        {
            case SystemType.Money:
                textSystem.text = "돈이 부족합니다...";
                break;
            case SystemType.Build:
                textSystem.text = "유효하지 않은 빌드 타워...";
                break;
        }
        tMPAlpha.FadeOut();
    }

}

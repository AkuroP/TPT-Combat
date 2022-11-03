using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;

    public void SetHUD(Shadow data)
    {
        nameText.text = data._Name;
        hpSlider.maxValue = data.maxHP;
        hpSlider.value = data.currentHP;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}

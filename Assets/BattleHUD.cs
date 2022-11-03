using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;
    public String name;

    public void SetHUD(Shadow data)
    {
        name = data._Name;
        nameText.text = data._Name;
        // hpSlider.maxValue = data.maxHp;
        // hpSlider.value = data.currentHp;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}

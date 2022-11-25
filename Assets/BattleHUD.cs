using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public Slider hpSlider;
    public Slider manaSlider;
    public Slider xpSlider;

    public void SetHUD(Shadow data)
    {
        nameText.text = data._Name;
        hpSlider.maxValue = data.maxHP;
        hpSlider.value = data.currentHP;
        if (data.anEnemy) return;
        manaSlider.maxValue = data.manaMax;
        manaSlider.value = data.mana;
        xpSlider.maxValue = data.maxExp;
        xpSlider.value = data.exp;
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetMana(int mana)
    {
        manaSlider.value = mana;
    }

    public void SetXP(int xp)
    {
        xpSlider.value = xp;
    }
}

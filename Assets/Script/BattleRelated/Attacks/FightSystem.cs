using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FightSystem : MonoBehaviour
{
    public TMP_Text atkName;
    public TMP_Text description;
    public TMP_Text power;
    public TMP_Text manaCost;

    public SpellsData spellData;

    void Start()
    {
        atkName.text = spellData._name;
        description.text = spellData._description;
        power.text = spellData._power.ToString();
        manaCost.text = spellData._manaCost.ToString();

    }

    void Update()
    {
        
    }
}

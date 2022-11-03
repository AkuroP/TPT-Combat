using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class Shadow : MonoBehaviour
{
    public SpellsData spellSelected;

    public List<SpellsData> ListOfSpells;

    public BattleOrderManager BO;

    [Header("Type de personnage")]
    public bool anEnemy;

    [Header("Statistiques")] 
    public String _Name;
    public int maxHP;
    public int currentHP;
    public int atk;
    public int sAtk;
    public int def;
    public int sDef;
    public int speed;
    public int mana;

    public float damageFormule;
    public float damages;

    [Header("Status")]
    public bool burned;
    public bool frozen;
    public bool paralyzed;

    [Header("Debug")]
    public EntityData MyEntity;
    public Shadow EntitySelected;
    // Start is called before the first frame update
    void Start()
    {
        _Name = MyEntity._Name;
        maxHP = MyEntity._hp;
        atk = MyEntity._atk;
        sAtk = MyEntity._sAtk;
        def = MyEntity._def;
        sDef = MyEntity._sDef;
        speed = MyEntity._speed;

        currentHP = maxHP;

        burned = MyEntity._burned;
        frozen = MyEntity._frozen;
        paralyzed = MyEntity._paralyzed;

        anEnemy = MyEntity._anEnemy;
        ListOfSpells = MyEntity._ListOfSpells;
    
        BO = FindObjectOfType<BattleOrderManager>();
        BO.Shadows.Add(this);

        IsAnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        if(EntitySelected != null)
        damageFormule = spellSelected._power * atk / EntitySelected.def;

        if (burned)
        {
            atk /= 2;
        }
    }

    public void IsAnEnemy()
    {
        if (anEnemy)
        {
            EntitySelected = GameObject.FindGameObjectWithTag("PlayerStation").GetComponent<Shadow>();

            int randAtk = Random.Range(0, ListOfSpells.Count);
            spellSelected = ListOfSpells[randAtk];
        }
    }
    
    public void Attack()
    {
        if(currentHP <= 0)
        {
            Debug.Log("T'es mort " + MyEntity._Name);
        }
        else
        {
            if (mana >= spellSelected._manaCost)
            {
                if (paralyzed)
                {
                    int cucked = Random.Range(0, 100);

                    if (cucked >= 25)
                        SuccessfulAttack();

                    else
                    {
                        Debug.Log("T'es Paralysé , pas de chance mon boug ! " + MyEntity._Name);
                    }

                }
                else if (frozen)
                {
                    Debug.Log("T'es gelé zebi Kekw " + MyEntity._Name);
                }
                else
                {
                    SuccessfulAttack();
                }

            }
            else
            {
                Debug.Log("T'as plus de mana " + MyEntity._Name);
            }
        }
        
    }

    public void SuccessfulAttack()
    {
        Debug.Log("Attaque réussie !, " + MyEntity._Name + " a infligé " + damageFormule + " dégâts avec " + spellSelected._name );
        mana -= spellSelected._manaCost;
        EntitySelected.currentHP -= (int)damageFormule;

        if(spellSelected.currentStatus == SpellsData.Status.Paralyze)
        {
            EntitySelected.paralyzed = true;
        }
        if (spellSelected.currentStatus == SpellsData.Status.Frozen)
        {
            EntitySelected.frozen = true;
        }
    }
}

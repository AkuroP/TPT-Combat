using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Shadow : MonoBehaviour
{
    public SpellsData spellSelected;

    public List<SpellsData> ListOfSpells;

    public BattleOrderManager BO;
    public BattleHUD healthHUD;
    public Habillage habillage;
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

    
    void OnEnable()
    {
        StartCoroutine(InitEntity());
    }

    void OnDisable()
    {
        if (anEnemy)
            InitVar(MyEntity, _Name, maxHP, atk, sAtk, def, sDef, speed, burned, frozen, paralyzed, ListOfSpells);
    }

    IEnumerator InitEntity()
    {
        yield return new WaitForSeconds(1f);
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

    public void InitVar(EntityData entityData,String _Name, int CurrentHp, int atk, int sAtk, int def, int sDef, int speed, 
        bool burned, bool frozen, bool paralyzed, List<SpellsData> ListOfSpells)
    {
        entityData = null;
        _Name = null;
        
        CurrentHp = 0;        atk =  0;        sAtk = 0;
        def =  0;        sDef =  0;        speed =  0;
        
        burned = false;
        frozen = false;
        paralyzed = false;
        ListOfSpells = null;
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

        if (currentHP <= 0 && !anEnemy)
        {
            // Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
        else if (currentHP <= -0.1f && anEnemy)
        {
            StartCoroutine(Victory());
        }
        
    }

    IEnumerator Victory()
    {
        
        Destroy(Habillage.instance.Getmob.gameObject);
        yield return new WaitForSeconds(1f);
        GameManager.instance.ShowMM();

        Habillage.instance.Getmob = null;
        print("mob null normalement");
        GameManager.instance.gameState = GameManager.GameState.Adventure;
        
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

    public void SuccessfulAttack()
    {
        Debug.Log("Attaque réussie !, " + MyEntity._Name + " a infligé " + damageFormule + " dégâts avec " + spellSelected._name );
        mana -= spellSelected._manaCost;
        EntitySelected.currentHP -= (int)damageFormule;
        healthHUD.SetHP(EntitySelected.currentHP);        

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shadow : MonoBehaviour
{
    
    public SpellsData spellSelected;

    public SpellsData[] spells;

    [Header("Statistiques")]
    public int hp;
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
    void Awake()
    {       

        hp = MyEntity._hp;
        atk = MyEntity._atk;
        sAtk = MyEntity._sAtk;
        def = MyEntity._def;
        sDef = MyEntity._sDef;
        speed = MyEntity._speed;

        burned = MyEntity._burned;
        frozen = MyEntity._frozen;
        paralyzed = MyEntity._paralyzed;

    }

    private void Start()
    {
        spells = FindObjectsOfType<SpellsData>();
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

    public void Attack()
    {
        if(mana >= spellSelected._manaCost)
        {
            if (paralyzed)
            {
                int cucked = Random.Range(0, 100);

                if(cucked >= 25)
                    SuccessfulAttack();

                else
                {
                    Debug.Log("T'es Paralysé , pas de chance mon boug !");
                }
                    
            }
            else if (frozen)
            {
                Debug.Log("T'es gelé zebi Kekw");
            }
            else
            {
                SuccessfulAttack();
            }
            
        }
        else
        {
            Debug.Log("T'as plus de mana zebi");
        }
    }

    public void SuccessfulAttack()
    {
        Debug.Log("Attaque réussie !, vous avez infligé " + damageFormule + " dégâts");
        mana -= spellSelected._manaCost;
        EntitySelected.hp -= (int)damageFormule;

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

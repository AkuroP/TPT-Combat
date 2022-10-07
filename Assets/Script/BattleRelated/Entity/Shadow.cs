using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Shadow : MonoBehaviour
{
    public EntityData MyEntity;
    public EntityData EntitySelected;
    public SpellsData spells;

    [Header("Statistiques")]
    public int hp;
    public int atk;
    public int sAtk;
    public int def;
    public int sDef;
    public int speed;
    public int mana;
    public float damageFormule;

    [Header("Status")]
    public bool burned;
    public bool frozen;
    public bool paralyzed;
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

    // Update is called once per frame
    void Update()
    {
        damageFormule = spells._power * atk / def;

        if (burned)
        {
            atk /= 2;
        }
    }

    public void Attack()
    {
        if(mana >= spells._manaCost)
        {
            Debug.Log("Attaque réussie !, vous avez infligé " + damageFormule + " dégâts");
            mana -= spells._manaCost;
        }
        else
        {
            Debug.Log("T'as plus de mana zebi");
        }
    }
}

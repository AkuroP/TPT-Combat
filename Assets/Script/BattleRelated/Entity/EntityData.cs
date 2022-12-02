using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "My game/EntityData")]
public class EntityData : ScriptableObject
{
    public Sprite sprite;
    public string _Name;

    [Header("Attaques")]
    public List<SpellsData> _ListOfSpells;

    [Header("Type d'entite")]
    public bool _anEnemy;
    public int _expValue;

    [Header("Stats de l'entite")]
    public int _hp;
    public int _atk;
    public int _sAtk;
    public int _def;
    public int _sDef;
    public int _speed;
    public int _lvl;
    public int _exp;

    [Header("StatusAppliques")]
    public bool _burned;
    public bool _frozen;
    public bool _paralyzed;

    public void AffectedByStatus()
    {
        if (_burned)
            _atk /= 2;

        if (_frozen)
        {
            Debug.Log("T'es Freeze zbi");
        }

    }
}

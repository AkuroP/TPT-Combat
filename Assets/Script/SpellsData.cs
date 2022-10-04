using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SpellsData", menuName = "My game/SpellsData")]
public class SpellsData : ScriptableObject
{
    public string _name;
    public string _description;
    public string _power;
    public string _manaCost;
    public bool _special;

    [Header("Status différents applicables")]
    public bool _Status;
    public bool _BurnStatus;
    public bool _FreezeStatus;
    public bool _ParalyzeStatus;

    [Header("Chance d'appliquer un status")]
    public int _chanceToApplyStatus;

    [Header("Liste des status")]
    public Status currentStatus;

    public enum Status
    {
        Burn,
        Frozen,
        Paralyze
    }
    public Status ApplyStatus()
    {     
        int calculationToApplyStatus;
        if (_Status)
        {
            calculationToApplyStatus = Random.Range(0, 100);
            if (_BurnStatus)
                if (calculationToApplyStatus <= _chanceToApplyStatus)
                    currentStatus = Status.Burn;
            if (_FreezeStatus)
                if (calculationToApplyStatus <= _chanceToApplyStatus)
                    currentStatus = Status.Frozen;
            if (_ParalyzeStatus)
                if (calculationToApplyStatus <= _chanceToApplyStatus)
                    currentStatus = Status.Paralyze;


        }
        return currentStatus;
    }
}

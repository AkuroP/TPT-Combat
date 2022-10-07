using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Character")]
public class Character : ScriptableObject
{
    [Header("References")]
    public Sprite sprite;

    public SpellsData spell;

    [Space]
    [Header("Parameters")]

    public bool isPlayerTeam;

    public float maxHealth;
    public float attack;
    public float defense;
    public float speed;
}

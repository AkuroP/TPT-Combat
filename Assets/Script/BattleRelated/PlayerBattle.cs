using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    void Awake()
    {

    }

    public void Setup (bool isPlayerTeam)
    {
        spriteRenderer.sprite = isPlayerTeam ? BattleSystem.GetInstance().playerSprite : BattleSystem.GetInstance().ennemySprite;
    }
}

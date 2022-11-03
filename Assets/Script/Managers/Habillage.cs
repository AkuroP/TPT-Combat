using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Habillage : MonoBehaviour
{
    public GameObject ennemy;
    public GameObject player;
    public GameObject Getmob;
    public GameObject GetPlayer;

    public BattleHUD playerHUD;
    public BattleHUD mobHUD;

    private void Start()
    {
        SetupBattle();
    }

    private void SetupBattle()
    {
        
        ennemy.GetComponent<Shadow>().MyEntity = Getmob.GetComponent<MobBehaviour>().mobData;
        ennemy.GetComponent<Image>().sprite = Getmob.GetComponent<SpriteRenderer>().sprite;
        
        player.GetComponent<Image>().sprite = GetPlayer.GetComponent<SpriteRenderer>().sprite;
        
        playerHUD.SetHUD(player.GetComponent<Shadow>());
        mobHUD.SetHUD(ennemy.GetComponent<Shadow>());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Habillage : MonoBehaviour
{
    public Transform playerStation;
    public Transform mobStation;

    private GameObject player;
    [HideInInspector]public GameObject mob;

    public BattleHUD playerHUD;
    public BattleHUD mobHUD;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetupBattle();
    }

    private void SetupBattle()
    {
        player.transform.position = playerStation.position; 
        mob.transform.position = mobStation.position; 
        
        playerHUD.SetHUD(player.GetComponent<PlayerBehaviour>().playerData);
        mobHUD.SetHUD(mob.GetComponent<MobBehaviour>().mobData);
    }
}

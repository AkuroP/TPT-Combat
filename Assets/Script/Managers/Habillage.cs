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
    public GameObject figthScene;

    public BattleHUD playerHUD;
    public BattleHUD mobHUD;
    
    public static Habillage instance;
    private void Awake()
    {
        if(instance != null)Destroy(gameObject);
        instance = this;
        
    }
    private void OnEnable()
    {
        StartCoroutine(SetupBattle());
    }

    private void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Adventure)
        {
            figthScene.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    IEnumerator SetupBattle()
    {
        yield return new WaitForSeconds(0.1f);
        ennemy.GetComponent<Shadow>().MyEntity = Getmob.GetComponent<MobBehaviour>().mobData;
        ennemy.GetComponent<Image>().sprite = Getmob.GetComponent<SpriteRenderer>().sprite;
        
        player.GetComponent<Image>().sprite = GetPlayer.GetComponent<SpriteRenderer>().sprite;

        yield return new WaitForSeconds(1f);
        
        playerHUD.SetHUD(player.GetComponent<Shadow>()); playerHUD.SetHP(player.GetComponent<Shadow>().currentHP);
        mobHUD.SetHUD(ennemy.GetComponent<Shadow>()); mobHUD.SetHP(ennemy.GetComponent<Shadow>().currentHP);
    }
}

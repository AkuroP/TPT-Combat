using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Habillage : MonoBehaviour
{
    [Header("Station")]
    public List<GameObject> ennemys = new List<GameObject>();
    public GameObject player;
    
    [Header("Get Player and mob")]
    public GameObject Getmob;
    public GameObject GetPlayer;
    
    [Header("L'UI")]
    public GameObject figthScene;
    public BattleHUD playerHUD;
    public List<BattleHUD> mobsHUD = new List<BattleHUD>();
    
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
        for (int i = 0; i < ennemys.Count; i++)
        {
            ennemys[i].GetComponent<Shadow>().MyEntity = Getmob.GetComponent<MobBehaviour>().mobData;
            ennemys[i].GetComponent<Image>().sprite = Getmob.GetComponent<SpriteRenderer>().sprite;
        }
        
        
        player.GetComponent<Image>().sprite = GetPlayer.GetComponent<SpriteRenderer>().sprite;

        yield return new WaitForSeconds(1f);
        
        playerHUD.SetHUD(player.GetComponent<Shadow>()); playerHUD.SetHP(player.GetComponent<Shadow>().currentHP);
        
        for (int i = 0; i < mobsHUD.Count; i++)
        {
            mobsHUD[i].SetHUD(ennemys[i].GetComponent<Shadow>()); 
            mobsHUD[i].SetHP(ennemys[i].GetComponent<Shadow>().currentHP);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Obstacle : MonoBehaviour, IInteract
{
    [SerializeField] private int levelNeeded;
    private Collider2D obstacleColl;
    [SerializeField] private EntityData entity;
    private Shadow playerStation;
    public PlayerBehaviour player;

    private void OnEnable()
    {
        obstacleColl = this.GetComponent<Collider2D>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        //playerStation = GameManager.instance.GetComponentInChildren<Shadow>();
        
        //if(playerStation.lvl >= levelNeeded)OpenLimit(player.GetComponent<Collider2D>());
        Shadow[] stations = GameManager.instance.GetComponentsInChildren<Shadow>(true);
        foreach(Shadow shadow in stations)
        {
            if(shadow.MyEntity == entity)
            {
                playerStation = shadow;
                break;
            }
        }
        if(playerStation.lvl >= levelNeeded)OpenLimit(player.GetComponent<Collider2D>());
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(!coll.collider.CompareTag("Player"))return;
        player = coll.gameObject.GetComponent<PlayerBehaviour>();
        player.canTalk = true;
        player.triggeredGO = this.gameObject;
    }

    private void OnCollisionExit2D(Collision2D coll)
    {
        if(!coll.collider.CompareTag("Player"))return;
        player.canTalk = false;
        player.triggeredGO = null;
    }

    private void OpenLimit(Collider2D playerColl)
    {
        Physics2D.IgnoreCollision(playerColl, obstacleColl, true);
        obstacleColl.GetComponent<TilemapRenderer>().enabled = false;
    }

    public void Interact()
    {
        if(playerStation.lvl < levelNeeded)return;
        Collider2D playerColl = player.GetComponent<Collider2D>();
        OpenLimit(playerColl);
    }
}

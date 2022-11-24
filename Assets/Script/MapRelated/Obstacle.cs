using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Obstacle : MonoBehaviour, IInteract
{
    [SerializeField] private int levelNeeded;
    private Collider2D obstacleColl;
    private PlayerBehaviour player;

    private void OnEnable()
    {
        obstacleColl = this.GetComponent<Collider2D>();
        PlayerBehaviour player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        if(player.playerLvl >= levelNeeded)OpenLimit(player.GetComponent<Collider2D>());
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
        if(player.playerLvl < levelNeeded)return;
        Collider2D playerColl = player.GetComponent<Collider2D>();
        OpenLimit(playerColl);
    }
}

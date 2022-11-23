using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private int levelNeeded;
    private Collider2D obstacleColl;

    private void OnEnable()
    {
        obstacleColl = this.GetComponent<Collider2D>();
        PlayerBehaviour player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        if(player.playerLvl > levelNeeded)OpenLimit(player.GetComponent<Collider2D>());
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(!coll.collider.CompareTag("Player") || coll.collider.GetComponent<PlayerBehaviour>().playerLvl < levelNeeded)return;
        else OpenLimit(coll.collider);
    }

    private void OpenLimit(Collider2D playerColl)
    {
        Physics2D.IgnoreCollision(playerColl, obstacleColl, true);
        obstacleColl.GetComponent<TilemapRenderer>().enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdetection : MonoBehaviour
{
    PlayerBehaviour player;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (player == null)
                player = col.GetComponent<PlayerBehaviour>();

            player.canTalk = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.canTalk = false;
        }
    }
}

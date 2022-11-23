using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdetection : MonoBehaviour
{
    PlayerBehaviour player;

    public List<string> Dialogue;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (player == null)
                player = col.GetComponent<PlayerBehaviour>();

            player.canTalk = true;

            player.currentDialog.Clear();
            foreach(string line in Dialogue)
            {
                player.currentDialog.Add(line);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.canTalk = false;
            //player.currentDialog = Dialogue;
            
        }
    }
}

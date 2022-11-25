using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdetection : MonoBehaviour, IInteract
{
    public PlayerBehaviour player;

    public List<string> Dialogue;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (player == null)
                player = col.GetComponent<PlayerBehaviour>();
            player.canTalk = true;
            player.triggeredGO = this.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.canTalk = false;
            player.triggeredGO = null;
        }
    }

    public void Interact()
    {
        Talk();
    }

    private void Talk()
    {
        player.PlayerInput.SwitchCurrentActionMap("UI");
        player.dialogueUI.GetComponent<Dialog>().lines.Clear();


        player.currentDialog.Clear();
        foreach(string line in Dialogue)
        {
            player.currentDialog.Add(line);
        }
        foreach(string line in player.currentDialog)
            player.dialogueUI.GetComponent<Dialog>().lines.Add(line);
        player.dialogueUI.SetActive(true);
    }
}

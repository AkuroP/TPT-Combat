using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCdetection : MonoBehaviour, IInteract
{
    public PlayerBehaviour player;

    public List<string> Dialogue;

    public GameObject UItalk;

    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
        UItalk.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (player == null)
                player = col.GetComponent<PlayerBehaviour>();
            player.canTalk = true;
            player.triggeredGO = this.gameObject;
            UItalk.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.canTalk = false;
            player.triggeredGO = null;
            UItalk.SetActive(false);
        }
    }

    public void Interact()
    {
        Talk();
        UItalk.SetActive(false);
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

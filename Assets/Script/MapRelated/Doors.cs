using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour, IInteract
{
    private PlayerBehaviour player;

    [SerializeField] private string[] dialogue;
       
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(!coll.collider.CompareTag("Player"))return;
        player = coll.gameObject.GetComponent<PlayerBehaviour>();
    }

    public void Interact()
    {
        player.PlayerInput.SwitchCurrentActionMap("UI");
        player.dialogueUI.GetComponent<Dialog>().lines.Clear();


        player.currentDialog.Clear();
        foreach(string line in dialogue)
        {
            player.currentDialog.Add(line);
        }
        foreach(string line in player.currentDialog)
            player.dialogueUI.GetComponent<Dialog>().lines.Add(line);
        player.dialogueUI.SetActive(true);
    }
}

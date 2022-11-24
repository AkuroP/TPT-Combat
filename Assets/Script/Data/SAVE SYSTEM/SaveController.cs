using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveController : MonoBehaviour
{

    public PlayerBehaviour player;
    public Shadow playerStation;

    private void Awake()
    {
        SaveSystem.InitSaveData();
    }
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    //public Text txtGameName;

    public void OnSave()
    {
        SaveSystem.SaveGameData();
    }

    public void OnLoad()
    {
        SaveData.GameData gameData = SaveSystem.LoadGameData();
        playerStation.currentHP = gameData.playerHP;
        playerStation.mana = gameData.playerMana;
        playerStation.lvl = gameData.playerLVL;
        playerStation.exp = gameData.playerEXP;

        //txtGameName.text = gameData.gameName;
    }
}

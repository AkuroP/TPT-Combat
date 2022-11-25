using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveController : MonoBehaviour
{

    public PlayerBehaviour player;
    public Shadow playerStation;
    public static bool menuLoading;

    private void Awake()
    {
        SaveSystem.InitSaveData();
    }
    private void OnEnable()
    {
        if(GameObject.FindWithTag("Player") == null)return;
        player = GameObject.FindWithTag("Player").GetComponent<PlayerBehaviour>();
    }
    //public Text txtGameName;

    public void OnSave()
    {
        if(playerStation._Name == "")
        {
            SavePlayerStats();
        }
        SaveSystem.SaveGameData(playerStation.lvl, playerStation.exp, player.transform.position.x, player.transform.position.y, playerStation.currentHP, playerStation.mana, GameManager.instance.playerSavedStats.playerActualZone);
    }

    public void OnLoad()
    {
        SaveData.GameData gameData = SaveSystem.LoadGameData();
        Time.timeScale = 1f;
        playerStation.currentHP = gameData.playerHP;
        GameManager.instance.playerSavedStats.mana = gameData.playerMana;
        playerStation.lvl = gameData.playerLVL;
        playerStation.exp = gameData.playerEXP;
        
        GameManager.instance.playerSavedStats.playerSavedPos = new Vector2(gameData.playerX, gameData.playerY);
        GameManager.instance.playerSavedStats.playerActualZone = gameData.whatZone;
        GameManager.instance.RemoveMode();
        GameManager.instance.pauseMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //txtGameName.text = gameData.gameName;
    }

    private void SavePlayerStats()
    {
        playerStation._Name = playerStation.MyEntity._Name;
        playerStation.maxHP = playerStation.MyEntity._hp;
        playerStation.atk = playerStation.MyEntity._atk;
        playerStation.sAtk = playerStation.MyEntity._sAtk;
        playerStation.def = playerStation.MyEntity._def;
        playerStation.sDef = playerStation.MyEntity._sDef;
        //playerStation.lvl = playerStation.MyEntity._lvl;
        //playerStation.exp = playerStation.MyEntity._exp;
        

        playerStation.currentHP = playerStation.maxHP;
        
        playerStation.burned = playerStation.MyEntity._burned;
        playerStation.frozen = playerStation.MyEntity._frozen;
        playerStation.paralyzed = playerStation.MyEntity._paralyzed;

        playerStation.ListOfSpells = playerStation.MyEntity._ListOfSpells;
    }
}

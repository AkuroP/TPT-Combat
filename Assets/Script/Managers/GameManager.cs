using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Adventure,
        Combat,
        Pause
    }

    public delegate void OnAdventureMode(Collider2D col, SpriteRenderer sprite);
    public delegate void OnCombatMode(Collider2D col, SpriteRenderer sprite);
    
    public OnAdventureMode adventure;
    public OnCombatMode combat;
    public GameState gameState = GameState.Adventure;
    public static GameManager instance;

    public GameObject mmUI;
    
    
    [System.Serializable]
    public class SpellsAnim
    {
        public string spellName;
        public GameObject spell;
    }
    
    [Header("All Spells Anim")]
    public List<SpellsAnim> allSpells = new List<SpellsAnim>();
    public Dictionary<string, GameObject> spells = new Dictionary<string, GameObject>();

    [Header("Save & Load")]
    public GameObject pauseMenu;
    
    [System.Serializable]
    public class PlayerSavedStats
    {
        public Vector2 playerSavedPos;
        public int playerActualZone;
        public int hp;
        public int mana;
        public int lvl;
        public int exp;
        public PlayerSavedStats(Vector2 savedPos)
        {
            playerSavedPos = savedPos;
        }

    }
    public PlayerSavedStats playerSavedStats = new PlayerSavedStats(new Vector2(-0.6f,-23.5f));
    public List<GameObject> allZone;

    
    
    private void AdventureMode(Collider2D col, SpriteRenderer sprite)
    {
        
        col.enabled = true;    
        sprite.enabled = true;
    }
    private void CombatMode(Collider2D col, SpriteRenderer sprite)
    {
       
        col.enabled = false;
        sprite.enabled = false;
    }
    private void Awake()
    {
        if(instance != null)Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        
        foreach(var ui in allSpells)
        {
            spells[ui.spellName] = ui.spell;
        }
    }

    private void Start()
    {
        SetPlayerMap();
    }
    public void HideMM()
    {
        mmUI.SetActive(false);
    }

    public void ShowMM()
    {
        mmUI.SetActive(true);
    }

    

    // public void OnEnableCamFollow()
    // {
    //     cameraFollow.enabled = true;
    // }
    //
    // public void OnDisableCamFollow()
    // {
    //     cameraFollow.enabled = false;
    // }

    public void SetPlayerMap()
    {
        //Debug.Log(playerSavedStats.playerActualZone);
        for(int i = 0; i < allZone.Count; i++)
        {
            if(!allZone[i].activeSelf)continue;
            allZone[i].SetActive(false);
        }
        allZone[playerSavedStats.playerActualZone].SetActive(true);
        CinemachineConfiner2D newConfiner = GameObject.FindObjectOfType<CinemachineConfiner2D>();
        PolygonCollider2D collForConfiner = allZone[playerSavedStats.playerActualZone].GetComponentInChildren<PolygonCollider2D>();
        newConfiner.m_BoundingShape2D = collForConfiner;
    }

    public void AddMode()
    {
        adventure += (col, sprite) => AdventureMode(col, sprite);
        combat += (col, sprite) => CombatMode(col, sprite);
        
    }

    public void RemoveMode()
    {
        adventure -= (col, sprite) => AdventureMode(col, sprite);
        combat -= (col, sprite) => CombatMode(col, sprite);
    }
}

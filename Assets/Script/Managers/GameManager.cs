using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject mmUI;
    // [SerializeField] private Camera cameraFollow;

    
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
        if(instance != null)Destroy(gameObject);
        instance = this;
        
    }

    private void Start()
    {
        adventure += (col, sprite) => AdventureMode(col, sprite);
        combat += (col, sprite) => CombatMode(col, sprite);
    }

    private void Update()
    {

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
}

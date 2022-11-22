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

    public delegate void OnAdventureMode(float saveSpeed,float speed, Collider2D col, SpriteRenderer sprite);
    public delegate void OnCombatMode(float speed, Collider2D col, SpriteRenderer sprite);

    public OnAdventureMode adventure;
    public OnCombatMode combat;
    public GameState gameState = GameState.Adventure;
    public static GameManager instance;

    [SerializeField] private GameObject mmUI;
    // [SerializeField] private Camera cameraFollow;

    
    private void AdventureMode(float saveSpeed, float speed, Collider2D col, SpriteRenderer sprite)
    {
        speed = saveSpeed;
        col.enabled = true;    
        sprite.enabled = true;
    }
    private void CombatMode(float speed, Collider2D col, SpriteRenderer sprite)
    {
        speed = 0;
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
        adventure += (saveSpeed,speed, col, sprite) => AdventureMode(saveSpeed, speed, col, sprite);
        combat += (rb, col, sprite) => CombatMode(rb, col, sprite);
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

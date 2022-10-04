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

    public GameState gameState = GameState.Adventure;
    public static GameManager instance;
    public GameObject UIFight;

    private void Awake()
    {
        if(instance != null)Destroy(gameObject);
        instance = this;
        
    }

    private void Update()
    {
        if (gameState == GameState.Adventure)
        {
            UIFight.SetActive(false);
        }
        else if (gameState == GameState.Combat)
        {
            UIFight.SetActive(true);
        }
        
    }
    
}

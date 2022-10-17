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
    

    private void Awake()
    {
        if(instance != null)Destroy(gameObject);
        instance = this;
        
    }

    private void Update()
    {
        
        
    }
    
}

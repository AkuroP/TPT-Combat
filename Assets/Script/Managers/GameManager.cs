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

    [SerializeField] private GameObject mmUI;
    // [SerializeField] private Camera cameraFollow;
    

    private void Awake()
    {
        if(instance != null)Destroy(gameObject);
        instance = this;
        
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

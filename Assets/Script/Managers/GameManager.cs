using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null)Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(instance);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //remplacer string par script des entity
    public void StartBattle(string player, string enemy)
    {
        Debug.Log("STARTING BATTLE !");
        Debug.Log(player + " VS " + enemy);
    }
}
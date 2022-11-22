using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    START, PLAYERTURN, ENEMYTURN, WIN, LOST 
}

public class BattleSystem : MonoBehaviour
{
    static BattleSystem instance;
    public static BattleSystem GetInstance()
    {
        return instance;
    }

    [SerializeField] Transform characterBattle;
    public Sprite playerSprite;
    public Sprite ennemySprite;         //changer ça en fonction de l'ennemi

    PlayerBattle playerBattle;
    PlayerBattle enemyBattle;
    State state;

    

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        playerBattle = SpawnCharacter(true);
        enemyBattle = SpawnCharacter(false);
        
        state = State.START; 
    }

    void Update()
    {
        // if (state == State.WaitingForPlayer)
        //     if (Input.GetKeyDown(KeyCode.Space))
        //     {
        //         state = State.Busy;
        //         Debug.Log("Attack");
        //     }
    }

    PlayerBattle SpawnCharacter(bool isPlayerTeam)
    {
        Vector3 position;
        position = isPlayerTeam ? new Vector3(-5,0) : new Vector3(5, 0);

        Transform characterTransform = Instantiate(characterBattle, position, Quaternion.identity);
        PlayerBattle playerBattle = characterTransform.GetComponent<PlayerBattle>();
        playerBattle.Setup(isPlayerTeam);

        return playerBattle;
    }
}

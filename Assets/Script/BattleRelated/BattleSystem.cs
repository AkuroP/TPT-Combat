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
    public Sprite ennemySprite;

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

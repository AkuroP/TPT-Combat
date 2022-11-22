using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] mobToSpawn;
    [SerializeField] private List<GameObject> mobSpawned;
    
    [SerializeField] private GameObject[] mobFightUI = new GameObject[3];
    private void OnEnable()
    {
        //dispawn spawned mob if exist
        foreach(GameObject mob in mobSpawned)
        {
            Destroy(mob);
        }
        mobSpawned.Clear();

        //spawn mob
        for(int i = 0; i < mobToSpawn.Length; i++)
        {
            GameObject tempMob = Instantiate(mobToSpawn[i], this.transform);
            MobBehaviour mobBehaviour = tempMob.GetComponent<MobBehaviour>();
            mobBehaviour.habillage = mobFightUI[0];
            mobBehaviour.fightScene = mobFightUI[1];
            mobBehaviour.transitionObject = mobFightUI[2];
            mobBehaviour.animator = mobFightUI[2].GetComponent<Animator>();
            mobSpawned.Add(tempMob);
        }
        
    }
}

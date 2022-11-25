using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject[] mobToSpawn;
    [SerializeField] private List<GameObject> mobSpawned;

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
            mobBehaviour.habillage = GameManager.instance.habillage;
            mobBehaviour.fightScene = GameManager.instance.fightScene;
            mobBehaviour.transitionObject = GameManager.instance.transition;
            mobBehaviour.transitionAnim = GameManager.instance.transition.GetComponent<Animator>();
            mobSpawned.Add(tempMob);
        }
        
    }
}

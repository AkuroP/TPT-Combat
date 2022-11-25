using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMapToGM : MonoBehaviour
{
    [SerializeField] private List<GameObject> allMaps;
    private void Start()
    {
        if(GameManager.instance.allZone == null)return;
        GameManager.instance.allZone = allMaps;
        GameManager.instance.SetPlayerMap();
    }
}

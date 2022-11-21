using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerWalkOn : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("ChangeZone"))
        {
            coll.GetComponent<ChangeZone>().GoToZone();
        }
    }
}

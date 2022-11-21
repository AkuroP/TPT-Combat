using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeZone : MonoBehaviour
{

    public GameObject actualZone;
    public GameObject nextZone;


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Player"))
        {
            GoToZone();
        }
    }

    public void GoToZone()
    {
        if(actualZone == null || nextZone == null)return;
        actualZone.SetActive(false);
        nextZone.SetActive(true);
    }
}

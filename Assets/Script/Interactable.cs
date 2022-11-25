using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Interactable : Collidable
{
    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == "Player")
        {
            print(col.name);
        }
        
    }
}

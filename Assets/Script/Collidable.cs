using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter;
    public BoxCollider2D _boxCol2D;
    public _collider2d col2d = new _collider2d();
    public class _collider2d
    {
        
        public T GetCollider<T>(T col) where T : class
        {
            return col;
        }
        
        public T SetCollider<T>(T newCollider, string typeCollider) where T : class
        {
            if (typeCollider == "TileMapCollider2D")
            {
                return (T) Convert.ChangeType(newCollider, typeof(TilemapCollider2D));
            }

            switch (typeCollider)
            {
                case "TileMapCollider2D":
                    return (T) Convert.ChangeType(newCollider, typeof(TilemapCollider2D));
                break;
                
                case "BoxCollider2D":
                    return (T) Convert.ChangeType(newCollider, typeof(BoxCollider2D));
                    break;
                
                case "CapsuleCollider2D":
                    return (T) Convert.ChangeType(newCollider, typeof(CapsuleCollider2D));
                    break;
            }

            return null as T;
        }
    }
    

    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        _boxCol2D = GetComponent<BoxCollider2D>();
    }
    
    
    protected virtual void Update()
    {
        //Collision work
        _boxCol2D.OverlapCollider(filter, hits);
        
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null) continue;
            
            OnCollide(hits[i]);
            
            //clean le tableau manuellement
            hits[i] = null;

        }
    }
    
    protected virtual void OnCollide(Collider2D col)
    {
        Debug.Log(col.name);
    }
    
}

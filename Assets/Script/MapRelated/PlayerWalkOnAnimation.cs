using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class PlayerWalkOnAnimation : MonoBehaviour
{
    public AnimatedTile yee;
    public Tile yeet;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Platform"))
        {
            yee = coll.GetComponent<AnimatedTile>();
            Debug.Log(yee.m_AnimatedSprites[0]);
            yee.m_AnimationStartFrame += 1;
            yee.RefreshTile(Vector3Int.CeilToInt(coll.transform.position), coll.GetComponent<ITilemap>());
        }
    }
}

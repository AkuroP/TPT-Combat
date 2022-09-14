using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{

    [SerializeField] private float mobRadius;
    [SerializeField] private float mobDistance;
    [SerializeField] private LayerMask targetLayer;
    private Vector2 mobDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D chaseCircle = Physics2D.CircleCast(this.transform.position, mobRadius, mobDir, mobDistance, targetLayer);
        if(chaseCircle.collider != null)
        {
            Debug.Log("PLAYER DETECT"); 
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, mobDistance);
    }
}

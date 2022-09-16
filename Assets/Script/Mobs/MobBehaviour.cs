using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{

    [SerializeField] private float mobRadius;
    [SerializeField]private float mobDistance;
    [SerializeField] private LayerMask targetLayer;
    public float speed;
    private Vector2 mobDir;
    public GameObject player;
    private Rigidbody2D rb;

    private bool isHunting;
    private Vector2 mov;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        RaycastHit2D chaseCircle = Physics2D.CircleCast(this.transform.position, mobRadius, mobDir, mobDistance, targetLayer);
        if (chaseCircle.collider == null)
            return;
        
        if(chaseCircle.collider !=null)
        {
            Debug.Log("PLAYER DETECT");
            isHunting = true;
        }
    }

    private void FixedUpdate()
    {
        if (isHunting)
        {
            Vector3 MPdir = (player.transform.position - transform.position).normalized;
            mov = MPdir;
            rb.velocity = new Vector2(mov.x, mov.y) * speed;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            print("DUEL");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, mobRadius);
    }
}

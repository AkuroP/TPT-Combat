using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")] public float speed = 200.0f;
    public float limiter = 0.7f;

    private Vector2 movement;

    [Header("Component")]
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float range;
    private Vector2 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x != 0 && movement.y != 0)
        {
            movement.x *= limiter;
            movement.y *= limiter;
        }

        bool isIdle = movement.x == 0 && movement.y == 0;
        if (isIdle)
        {
            //Idle
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving", false);
        }
        else
        {
            //Moving
            rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, movement.y * speed * Time.deltaTime);
            anim.SetFloat("Xinput", movement.x);
            anim.SetFloat("Yinput", movement.y);
            anim.SetBool("isMoving", true);
            dir = new Vector2(movement.x, movement.y);
        }

        RaycastHit2D yes = Physics2D.Raycast((Vector2)this.transform.position, dir, range, wallLayer);
        if (yes.collider != null)
        {
            rb.velocity = Vector2.zero;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay((Vector2)this.transform.position, dir * range);
    }
}
    

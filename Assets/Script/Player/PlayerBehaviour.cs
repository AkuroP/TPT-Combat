using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 200.0f;
    public float limiter = 0.7f;
    private bool flip = false;
 
    private Vector2 movement;
    
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;
    private SpriteRenderer sprite;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    
    private void FixedUpdate()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        if (movement.x != 0 && movement.y != 0)
        {
            movement.x *= limiter;
            movement.y *= limiter;
        }
        
        
        rb.velocity = new Vector2(movement.x * speed * Time.deltaTime, movement.y* speed * Time.deltaTime);

        Flip(rb.velocity.x);
    }

    private void Flip(float _velocity)
    {
        if (_velocity > 0.2f)
        {
            sprite.flipX = false;
            if (flip == false)
            {
                flip = true;
            }
            
        }
        else if (_velocity < -0.2f)
        {
            sprite.flipX = true;
            if (flip == true)
            {
                flip = false;
            }
            
            
        }
    }
}

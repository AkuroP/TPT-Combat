using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed = 200.0f;
    private float speedSave;
    public float limiter = 0.7f;

    private Vector2 movement;

    [Header("Component")]
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;
    private SpriteRenderer sprite;
    private Animator anim;
    private CapsuleCollider2D col;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float range;
    private Vector2 dir;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();

        speedSave = moveSpeed;

        AudioManager.instance.PlayClipAt(AudioManager.instance.allAudio["exploration"], this.transform.position, AudioManager.instance.ostMixer);
    }


    private void FixedUpdate()
    {
        // movement.x = Input.GetAxisRaw("Horizontal");
        // movement.y = Input.GetAxisRaw("Vertical");

        // if (movement.x != 0 && movement.y != 0)
        // {
        //     movement.x *= limiter;
        //     movement.y *= limiter;
        // }

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
            rb.velocity = new Vector2(movement.x * moveSpeed * Time.deltaTime, movement.y * moveSpeed * Time.deltaTime);
            anim.SetFloat("Xinput", movement.x);
            anim.SetFloat("Yinput", movement.y);
            anim.SetBool("isMoving", true);
            dir = new Vector2(movement.x, movement.y);
        }

        RaycastHit2D yes = Physics2D.Raycast((Vector2)this.transform.position, dir, range, wallLayer);
        if (yes.collider != null)rb.velocity = Vector2.zero;

    }

    private void Update()
    {

        switch (GameManager.instance.gameState)
        {
            case GameManager.GameState.Combat :
                // rb.bodyType = RigidbodyType2D.Static;
                // rb.velocity = Vector2.zero;
                // sprite.enabled = false;
                
                GameManager.instance.combat.Invoke(moveSpeed, col,sprite);
                break;
            
            case GameManager.GameState.Adventure :
                // rb.bodyType = RigidbodyType2D.Dynamic;
                // sprite.enabled = true;
                
                GameManager.instance.adventure.Invoke(speedSave,moveSpeed, col,sprite);
                break;
        }
    }

    void OnMove(InputValue v)
    {
        movement = v.Get<Vector2>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay((Vector2)this.transform.position, dir * range);
    }
}
    

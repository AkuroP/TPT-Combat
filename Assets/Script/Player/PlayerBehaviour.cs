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

    [SerializeField] private GameObject runParticles;
    [SerializeField] private float particlesTimerMax = 0.6f;
    private float particlesTimer;
    bool particleStop;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();

        speedSave = moveSpeed;

        runParticles.SetActive(false);
        particlesTimer = 0;
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
            
            
            if (particlesTimer > 0)particlesTimer = Mathf.Clamp(particlesTimer - Time.fixedDeltaTime, 0, particlesTimerMax);
            else runParticles.SetActive(false);

        }
        else
        {
            //Moving
            rb.velocity = new Vector2(movement.x * moveSpeed * Time.deltaTime, movement.y * moveSpeed * Time.deltaTime);
            anim.SetFloat("Xinput", movement.x);
            anim.SetFloat("Yinput", movement.y);
            anim.SetBool("isMoving", true);
            dir = new Vector2(movement.x, movement.y);
            runParticles.SetActive(true);
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

    public void OnMove(InputAction.CallbackContext ctx)
    {
        movement = ctx.ReadValue<Vector2>();

        if (ctx.canceled)
        {
            particlesTimer = particlesTimerMax;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay((Vector2)this.transform.position, dir * range);
    }
}
    

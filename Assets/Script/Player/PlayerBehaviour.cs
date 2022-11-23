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
    private PlayerInput playerInput;

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float range;
    private Vector2 dir;

    [SerializeField] private GameObject runParticles;
    [SerializeField] private float particlesTimerMax = 0.6f;
    private float particlesTimer;
    bool particleStop;
    


    public bool canTalk;
    public GameObject dialogueUI;
    public List<string> currentDialog;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        playerInput = GetComponent<PlayerInput>();

        speedSave = moveSpeed;

        AudioManager.instance.PlayClipAt(AudioManager.instance.allAudio["exploration"], this.transform.position, AudioManager.instance.ostMixer, false);
        runParticles.SetActive(false);
        particlesTimer = 0;

        dialogueUI.SetActive(false);
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
                moveSpeed = 0;
                GameManager.instance.combat.Invoke(col,sprite);
                break;
            
            case GameManager.GameState.Adventure :
                moveSpeed = 200f;
                GameManager.instance.adventure.Invoke(col,sprite);
                break;
        }
    }

    #region Inputs

        public void OnMove(InputAction.CallbackContext ctx)
        {
            movement = ctx.ReadValue<Vector2>();

            if (ctx.canceled)
            {
                particlesTimer = particlesTimerMax;
            }
        }

        public void OnUse(InputAction.CallbackContext ctx)
        {
            if (ctx.performed && canTalk)
            {
                playerInput.SwitchCurrentActionMap("UI");

                dialogueUI.GetComponent<Dialog>().lines.Clear();
                foreach(string line in currentDialog)
                    dialogueUI.GetComponent<Dialog>().lines.Add(line);

                dialogueUI.SetActive(true);
                //dialogueUI.GetComponent<Dialog>().lines = currentDialog;
            }
        }


    #endregion

    public void SwitchActionMap(string am)
    {
        if (GameManager.instance.gameState == GameManager.GameState.Adventure)
        {
            playerInput.SwitchCurrentActionMap(am);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay((Vector2)this.transform.position, dir * range);
    }

    public void WalkSound()
    {
        //solution temporaire
        if((anim.GetFloat("Xinput") > 0 && anim.GetFloat("Yinput") > 0) ||
        (anim.GetFloat("Xinput") < 0 && anim.GetFloat("Yinput") < 0) ||
        (anim.GetFloat("Xinput") < 0 && anim.GetFloat("Yinput") > 0) ||
        (anim.GetFloat("Xinput") > 0 && anim.GetFloat("Yinput") < 0))return;
        AudioManager.instance.PlayClipAt(AudioManager.instance.allAudio["WalkOnGrass"], this.transform.position, AudioManager.instance.soundEffectMixer, true);
    }
}
    

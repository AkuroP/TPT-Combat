using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Movement")] 
    public float moveSpeed = 200.0f;
    private float speedSave;

    private Vector2 movement;

    [Header("Component")]
    private Rigidbody2D rb;
    private CapsuleCollider2D playerCollider;
    private SpriteRenderer sprite;
    private Animator anim;
    private CapsuleCollider2D col;
    private PlayerInput playerInput;
    public PlayerInput PlayerInput
    {
        get{return playerInput;}
        private set{}
    }

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float range;
    private Vector2 dir;

    [SerializeField] private GameObject runParticles;
    [SerializeField] private float particlesTimerMax = 0.6f;
    private float particlesTimer;
    


    public bool canTalk;
    public GameObject dialogueUI;
    public List<string> currentDialog;
    
    public GameObject triggeredGO;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider2D>();
        playerInput = GetComponent<PlayerInput>();
        dialogueUI = GameManager.instance.dialogueUI;
        speedSave = moveSpeed;

        AudioManager.instance.PlayClipAt(AudioManager.instance.allAudio["exploration"], this.transform.position, AudioManager.instance.ostMixer, false);
        runParticles.SetActive(false);
        particlesTimer = 0;

        dialogueUI.SetActive(false);

        //load
        this.transform.position = GameManager.instance.playerSavedStats.playerSavedPos;
        GameManager.instance.AddMode();
        
    }


    private void FixedUpdate()
    {

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
                if(triggeredGO == null) return;
                var interaction = triggeredGO.GetComponent<IInteract>();
                Debug.Log(interaction);
                if(interaction == null)
                {
                    Debug.Log(":'(");
                    return;
                }
                interaction.Interact();
            }
        }
        public void OnManageGame(InputAction.CallbackContext ctx)
        {
            if(ctx.performed)
            {
                if(GameManager.instance.pauseMenu.activeSelf)
                {
                    GameManager.instance.pauseMenu.SetActive(false);
                    Time.timeScale = 1;
                }
                else
                {
                    GameManager.instance.pauseMenu.SetActive(true);
                    Time.timeScale = 0;
                }
            }
        }


    #endregion

    public void SwitchActionMap(string am)
    {
        playerInput.SwitchCurrentActionMap(am);
        Debug.Log("switch to " + am);
        if (GameManager.instance.gameState == GameManager.GameState.Adventure)
        {
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
    

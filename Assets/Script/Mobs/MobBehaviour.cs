using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MobBehaviour : MonoBehaviour
{
    public GameObject habillage;
    [SerializeField] private float mobRadius;
    [SerializeField]private float mobDistance;
    [SerializeField] private LayerMask targetLayer;
    public float Huntspeed;
    public float speed = 6f;
    private Vector2 mobDir;
    public GameObject player;
    private Rigidbody2D rb;
    private BoxCollider2D col;

    private bool isHunting;
    private Vector2 mov;
    public Animator animator;
    public GameObject fightScene;
    public GameObject transitionObject;
    
    private Vector2 direction;
    
    public float radius  = 40.0f;

    // The point we are going around in circles
    private Vector2 basestartpoint;

    // Destination of our current move
    private Vector2 destination;

    // Start of our current move
    private Vector2 start;

    // Current move's progress
    private float progress = 0.0f;


    private Transform playerPos;

    public EntityData mobData;
    [Header("Mob Stats")] 
    [HideInInspector]public int _hp;
    [HideInInspector]public int _atk;
    [HideInInspector]public int _sAtk;
    [HideInInspector]public int _def;
    [HideInInspector]public int _sDef;
    [HideInInspector]public int _fightSpeed;
    

    private void Start()
    {
        if (mobData != null)
            LoadMobData(mobData);
        
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        start = transform.localPosition;
        basestartpoint = transform.localPosition;
        progress = 0.0f;

        PickNewRandomDestination();

    }
    
    private void LoadMobData(EntityData data)
    {
        _hp = data._hp;
        _atk = data._atk;
        _sAtk = data._sAtk;
        _def = data._def;
        _sDef = data._sDef;
        _fightSpeed = data._speed;
    }
    
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Combat)
        {
            rb.bodyType = RigidbodyType2D.Static;
            this.enabled = false;
        }
        
        RaycastHit2D chaseCircle = Physics2D.CircleCast(this.transform.position, mobRadius, mobDir, mobDistance, targetLayer);
        if (chaseCircle.collider == null)
            return;
        
        if(chaseCircle.collider !=null)
        {
            isHunting = true;
        }
    }

    private void FixedUpdate()
    {
        if (isHunting)
        {
            Vector3 MPdir = (player.transform.position - transform.position).normalized;
            mov = MPdir;
            rb.velocity = new Vector2(mov.x, mov.y) * Huntspeed;
        }
        else
        {
            bool reached = false;

            // Update our progress to our destination
            progress += speed * Time.deltaTime;

            // Check for the case when we overshoot or reach our destination
            if (progress >= 1.0f)
            {
                progress = 1.0f;
                reached = true;
            }

            // Update out position based on our start postion, destination and progress.
            transform.localPosition = (destination * progress) + start * (1 - progress);

            // If we have reached the destination, set it as the new start and pick a new random point. Reset the progress
            if (reached)
            {
                start = destination;
                PickNewRandomDestination();
                progress = 0.0f;
            }
            
        }
        
    }
    
    void PickNewRandomDestination()
    {
        // We add basestartpoint to the mix so that is doesn't go around a circle in the middle of the scene.
        destination = Random.insideUnitCircle * radius + basestartpoint;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            print("DUEL");
            col.enabled = false;
            GameManager.instance.gameState = GameManager.GameState.Combat;
            transitionObject.SetActive(true);
            GameManager.instance.OnDisableCamFollow();
            StartCoroutine(Transition());
            playerPos = other.collider.GetComponent<Transform>();
            

        }
    }

    IEnumerator Transition()
    {
        //Déplacer la caméra vers la scène de combat
        yield return new WaitForSeconds(1.2f);
        fightScene.SetActive(true);
        habillage.SetActive(true);
        habillage.GetComponent<Habillage>().mob = this.gameObject;
        GameManager.instance.HideMM();
        yield return new WaitForSeconds(0.5f);
        //End Animation transition
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(1.2f);
        transitionObject.SetActive(false);
        
    }
    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 0,1 ), mobRadius);
        
        UnityEditor.Handles.color = Color.yellow;
        if (!Application.isPlaying)
        {
            basestartpoint = transform.localPosition;
        }
        
        UnityEditor.Handles.DrawWireDisc(Random.insideUnitCircle + basestartpoint, new Vector3(0, 0,1), radius);
    }
    #endif
}

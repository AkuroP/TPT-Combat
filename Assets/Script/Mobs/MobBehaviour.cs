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
    private SpriteRenderer sprite;

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

    public EntityData mobData;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        

        start = transform.localPosition;
        basestartpoint = transform.localPosition;
        progress = 0.0f;

        PickNewRandomDestination();

    }

    void Update()
    {
        

        switch (GameManager.instance.gameState)
        {
            case GameManager.GameState.Combat :
                rb.bodyType = RigidbodyType2D.Static;
                rb.velocity = Vector2.zero;
                col.enabled = false;
                sprite.enabled = false;
                break;
            
            case GameManager.GameState.Adventure :
                rb.bodyType = RigidbodyType2D.Dynamic;
                col.enabled = true;
                sprite.enabled = true;
                break;
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
            // GameManager.instance.OnDisableCamFollow();
            StartCoroutine(Transition());
            
            

        }
    }

    IEnumerator Transition()
    {
        //Déplacer la caméra vers la scène de combat
        
        yield return new WaitForSeconds(1.2f);
        habillage.SetActive(true);
        habillage.GetComponent<Habillage>().Getmob = this.gameObject;
        fightScene.SetActive(true);
        
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

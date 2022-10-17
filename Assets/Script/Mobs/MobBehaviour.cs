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
    private BoxCollider2D col;

    private bool isHunting;
    private Vector2 mov;
    public Animator animator;
    public GameObject fightScene;
    public GameObject transitionObject;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (GameManager.instance.gameState == GameManager.GameState.Combat)
        {
            speed = 0;
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
            rb.velocity = new Vector2(mov.x, mov.y) * speed;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            print("DUEL");
            col.enabled = false;
            GameManager.instance.gameState = GameManager.GameState.Combat;
            transitionObject.SetActive(true);
            StartCoroutine(Transition());

        }
    }

    IEnumerator Transition()
    {
        //Déplacer la caméra vers la scène de combat
        
        yield return new WaitForSeconds(1.2f);
        fightScene.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        //End Animation transition
        animator.SetTrigger("Transition");
        yield return new WaitForSeconds(1.2f);
        transitionObject.SetActive(false);
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, mobRadius);
    }
}

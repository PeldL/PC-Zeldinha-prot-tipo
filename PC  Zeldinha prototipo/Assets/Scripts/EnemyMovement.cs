using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float playerDetecRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1;
    private EnemyState enemyState, newstate;

   

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);

    }


    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();


        if (attackCooldown > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }


        if (enemyState == EnemyState.Chasing)
        {
            if (enemyState == EnemyState.Chasing)
            {
                Chase();
            }
            else if (enemyState ==EnemyState.Attacking) 
            {
                rb.velocity = Vector2.zero;
            }
        }
       
    }


    void Chase()
    { 

       else if (player.position.x < transform.position.x && facingDirection == -1 ||
            player.position.x > transform.position.x && facingDirection == 1)
        {
            Flip();
        }
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }



    void Flip()
    {   
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }




    private void CheckForPlayer()
    {

        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetecRange, playerLayer);
        if (hits.Length > 0 )
        {
            player = hits[0].transform;
        

        
            ChangeState(EnemyState.Chasing);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newstate)
    {
        
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        enemyState = newstate;

        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", true);
    }




}


public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

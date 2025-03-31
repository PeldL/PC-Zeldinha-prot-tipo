using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float attackRange = 2;
    public float attackCooldown = 2;
    public float playerDetectRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1;
    private EnemyState enemyState, newState;

    
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

        if(attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero;
        }
    }

    void Chase()
    {
     

        if (player.position.x < transform.position.x && facingDirection == -1 ||
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectRange, playerLayer);
        if(hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.transform.position) <= attackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attacking);
            }


            else if(Vector2.Distance(transform.position, player.transform.position) > attackRange)
            {
                ChangeState(EnemyState.Chasing);

            }
 
        }
        else
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }



    }




    void ChangeState(EnemyState newState)
    {
        // Resetando os par�metros do Animator antes de mudar o estado
        anim.SetBool("isIdle", false);
        anim.SetBool("isChasing", false);
        anim.SetBool("isAttacking", false);

        // Atualizando o estado
        enemyState = newState;
        Debug.Log("Mudando estado para: " + enemyState); // Log para depura��o

        // Ativando a anima��o correspondente
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking) // Aqui estava errado
            anim.SetBool("isAttacking", true);
    }





}
public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float attackRange = 1.5f; // Aumentado para evitar que o inimigo perca o ataque
    public float attackCooldown = 2;
    public float playerDetecRange = 5;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private float attackCooldownTimer;
    private int facingDirection = -1;
    private EnemyState enemyState;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeState(EnemyState.Idle);
    }

    void Update()
    {
        CheckForPlayer();

        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if (enemyState == EnemyState.Chasing)
        {
            Chase();
        }
        else if (enemyState == EnemyState.Attacking)
        {
            rb.velocity = Vector2.zero; // Para o movimento enquanto ataca
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetecRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            Debug.Log("Distância do player: " + distanceToPlayer + " | attackRange: " + attackRange);

            if (distanceToPlayer <= attackRange && attackCooldownTimer <= 0)
            {
                Debug.Log("Atacando agora!");
                ChangeState(EnemyState.Attacking);
                attackCooldownTimer = attackCooldown; // Reseta cooldown do ataque
            }
            else
            {
                ChangeState(EnemyState.Chasing);
            }
        }
        else
        {
            ChangeState(EnemyState.Idle);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
            ChangeState(EnemyState.Idle);
        }
    }

    void ChangeState(EnemyState newState)
    {
        Debug.Log("Mudando estado para: " + newState);

        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", false);
        else if (enemyState == EnemyState.Attacking)
            anim.SetBool("isAttacking", false);

        enemyState = newState;

        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", true);
        else if (enemyState == EnemyState.Chasing)
            anim.SetBool("isChasing", true);
        else if (enemyState == EnemyState.Attacking)
        {
            anim.SetBool("isAttacking", true);
            Debug.Log("ATACANDO! isAttacking foi ativado.");
            StartCoroutine(AttackDuration());
        }
    }

    IEnumerator AttackDuration()
    {
        yield return new WaitForSeconds(1f); // Mantém o estado de ataque por 1 segundo
        ChangeState(EnemyState.Idle);
    }
}

public enum EnemyState
{
    Idle,
    Chasing,
    Attacking
}

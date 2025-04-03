// EnemyCombatTopDown.cs
using UnityEngine;

public class EnemyCombatTopDown : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 1.0f;
    public float attackCooldown = 1.5f;
    private float attackCooldownTimer;

    public Transform attackPoint;
    public LayerMask playerLayer;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (attackCooldownTimer > 0)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    private void TryAttack()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (playerHit != null && attackCooldownTimer <= 0)
        {
            anim.SetTrigger("isAttacking"); // Ativa a animação de ataque

            PlayerHealth playerHealth = playerHit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-attackDamage);
            }

            attackCooldownTimer = attackCooldown;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TryAttack();
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

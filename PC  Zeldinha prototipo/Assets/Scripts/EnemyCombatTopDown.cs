using UnityEngine;

public class EnemyCombatTopDown : MonoBehaviour
{
    public int attackDamage = 10;
    public float attackRange = 1.0f;
    public float attackCooldown = 1.5f;

    private float attackCooldownTimer;
    private Animator anim;
    public Transform attackPoint;
    public LayerMask playerLayer;

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
        if (attackCooldownTimer > 0) return; // Evita atacar antes do cooldown acabar

        anim.SetTrigger("isAttacking"); // Ativa a animação de ataque

        // Adiciona um pequeno atraso para dar o dano no momento certo da animação
        Invoke("DealDamage", 0.3f);

        attackCooldownTimer = attackCooldown;
    }

    private void DealDamage()
    {
        Collider2D playerHit = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayer);
        if (playerHit != null)
        {
            PlayerHealth playerHealth = playerHit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.ChangeHealth(-attackDamage);
            }
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

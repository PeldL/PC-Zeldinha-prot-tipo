using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Configuração do Ataque")]
    public Transform attackPoint;  // Ponto onde o ataque ocorre
    public float attackRange = 1f; // Raio do ataque
    public int damage = 1;         // Dano causado ao inimigo
    public float attackCooldown = 0.5f; // Tempo de recarga do ataque
    public LayerMask enemyLayer;   // Layer dos inimigos

    private float nextAttackTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime) // Clique esquerdo para atacar
        {
            Attack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    public void Attack()
    {
        Debug.Log("Ataque realizado!"); // Teste para ver se o ataque está funcionando

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Inimigo atingido: " + enemy.name);
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }
    }

    // Apenas para visualizar a área de ataque no Editor
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

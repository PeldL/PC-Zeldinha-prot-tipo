using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public int healAmount = 1; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

            if (playerHealth != null) 
            {
                playerHealth.ChangeHealth(healAmount);
                Debug.Log("Jogador curado! Vida atual: " + playerHealth.currentHealth);

                Destroy(gameObject); 
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Importar para usar SceneManager

public class PlayerHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;

    public TMP_Text healthText;
    public Animator healthTextAnim;

    private void Start()
    {
        healthText.text = "HP: " + currentHealth + " / " + maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        healthTextAnim.Play("TextUpdate");

        healthText.text = "HP: " + currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false); // Desativa o objeto jogador (opcional)
            LoadDeathScene(); // Chama a função para mudar de cena
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("heal"))
        {
            maxHealth += 1;
            Destroy(collision.gameObject);
        }
    }

    // Função para carregar a cena de "MORTE"
    private void LoadDeathScene()
    {
        SceneManager.LoadScene("MORTE"); // Troca para a cena "MORTE"
    }
}

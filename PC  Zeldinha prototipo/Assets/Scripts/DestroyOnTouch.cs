using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public GameObject objectToDestroy; // Objeto que será destruído

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o Player tocou
        {
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy); // Destroi o objeto atribuído
                Debug.Log(objectToDestroy.name + " foi destruído!");
            }
            else
            {
                Debug.LogWarning("Nenhum objeto atribuído para ser destruído!");
            }

            Destroy(gameObject); // Destroi o botão após ser ativado
            Debug.Log(gameObject.name + " (botão) foi destruído!");
        }
    }
}

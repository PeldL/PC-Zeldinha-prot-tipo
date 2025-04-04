using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    public GameObject objectToDestroy; // Objeto que ser� destru�do

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Verifica se o Player tocou
        {
            if (objectToDestroy != null)
            {
                Destroy(objectToDestroy); // Destroi o objeto atribu�do
                Debug.Log(objectToDestroy.name + " foi destru�do!");
            }
            else
            {
                Debug.LogWarning("Nenhum objeto atribu�do para ser destru�do!");
            }

            Destroy(gameObject); // Destroi o bot�o ap�s ser ativado
            Debug.Log(gameObject.name + " (bot�o) foi destru�do!");
        }
    }
}

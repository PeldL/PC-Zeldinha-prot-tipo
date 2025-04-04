using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Importa o sistema de gerenciamento de cenas

public class SceneChanger : MonoBehaviour
{
    public string sceneName; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene(sceneName); 
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int curentHealth;
    public int maxHealth;
    

    void Start()
    {
        curentHealth = maxHealth;
    }

    // Update is called once per frame
    public void ChangeHealth(int amount)
    {
        curentHealth += amount;
        if(curentHealth > maxHealth)
        {

            curentHealth = maxHealth;

        }

        else if (curentHealth <= 0)
        {
            Destroy(gameObject);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private bool isChansing;
    private int facingDirection = -1;
    private EnemyState enemyState;
    
    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        ChangeState(EnemyState.Idle);
        rb = GetComponent<Rigidbody2D>();   
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (isChansing == true) 
        {
            if(player.position.x > transform.position.x && facingDirection == -1|| 
                player.position.x > transform.position.x && facingDirection == 1) 
            {
                Flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
       
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(player == null) 
            { 
                player = collision.transform;
            }
            isChansing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.velocity = Vector2.zero;
            isChansing = false;
        }
    }

    void ChangeState(EnemyState newstate)
    {
        if (enemyState == EnemyState.Idle)
            anim.SetBool("isIdle", false);
    }




}


public enum EnemyState
{
    Idle,
    Chasing,
}

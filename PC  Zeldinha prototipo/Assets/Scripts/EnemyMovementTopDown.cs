using UnityEngine;

public class EnemyMovementTopDown : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 2f;
    public float playerDetectionRange = 5f;
    public Transform detectionPoint;
    public LayerMask playerLayer;

    private Rigidbody2D rb;
    private Transform player;
    private Animator anim;
    private bool isChasing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        isChasing = false;
    }

    void Update()
    {
        DetectPlayer();
        Move();
    }

    void Move()
    {
        if (isChasing && player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * speed;

            anim.SetBool("isIdle", false);
            anim.SetBool("isChasing", true);
        }
        else
        {
            rb.velocity = Vector2.zero;

            anim.SetBool("isIdle", true);
            anim.SetBool("isChasing", false);
        }
    }

    void DetectPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(detectionPoint.position, playerDetectionRange, playerLayer);
        isChasing = hits.Length > 0;
        if (isChasing)
        {
            player = hits[0].transform;
        }
    }
}

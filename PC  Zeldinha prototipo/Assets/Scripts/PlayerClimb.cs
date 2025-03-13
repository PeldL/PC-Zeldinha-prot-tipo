using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 3f;  // Velocidade de subida
    public LayerMask ladderLayer;  // Camada da escada
    private Rigidbody2D rb;
    private bool isClimbing = false;
    private float originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;  // Armazena o valor original da gravidade
    }

    void Update()
    {
        // Detecta se o jogador está em uma escada
        if (isClimbing)
        {
            // Faz o jogador subir ou descer com as teclas de movimento (W/S ou setas para cima/baixo)
            float verticalInput = Input.GetAxis("Vertical");
            rb.gravityScale = 0;  // Desativa a gravidade enquanto estiver subindo/descendo

            // Movimento de subida
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
        }
        else
        {
            rb.gravityScale = originalGravity;  // Restaura a gravidade normal quando não está subindo
        }
    }

    // Detecta colisão com a escada
    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & ladderLayer) != 0)  // Verifica se a escada foi tocada
        {
            isClimbing = true;
        }
    }

    // Sai da escada
    void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & ladderLayer) != 0)  // Verifica se saiu da escada
        {
            isClimbing = false;
        }
    }
}

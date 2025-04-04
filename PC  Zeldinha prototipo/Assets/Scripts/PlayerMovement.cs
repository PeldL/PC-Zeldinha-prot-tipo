using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento
    private Rigidbody2D rb;       // Para controlar a física do jogador
    private Vector2 movement;     // Para armazenar a direção do movimento
    private int facingDirection = 1;  // Variável para controlar a direção de virada do personagem

    // Start é chamado quando o script é inicializado
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obtém o componente Rigidbody2D do jogador
    }

    // Update é chamado uma vez por frame
    void Update()
    {
        ProcessInput();  // Processa a entrada do jogador
    }

    // FixedUpdate é chamado uma vez por frame, mas é usado para física, por isso é ideal para movimentação
    void FixedUpdate()
    {
        MoveCharacter();  // Aplica o movimento físico
    }

    // Função para processar a entrada do jogador
    void ProcessInput()
    {
        // Obtém a direção do movimento a partir das teclas W, A, S, D ou setas
        float moveX = Input.GetAxisRaw("Horizontal");  // Para a direção horizontal (esquerda/direita)
        float moveY = Input.GetAxisRaw("Vertical");    // Para a direção vertical (cima/baixo)

        movement = new Vector2(moveX, moveY).normalized;  // Normaliza para evitar movimento diagonal mais rápido

        // Chama a função de flip se necessário
        if (moveX != 0)
        {
            Flip(moveX);  // Se houver movimento horizontal, chama o Flip
        }
    }

    // Função para mover o personagem
    void MoveCharacter()
    {
        rb.velocity = movement * moveSpeed;  // Aplica a velocidade ao Rigidbody2D
    }

    // Função para virar o personagem dependendo da direção
    void Flip(float moveX)
    {
        // Se o movimento for para a esquerda, viramos o personagem
        if (moveX > 0 && facingDirection == -1 || moveX < 0 && facingDirection == 1)
        {
            facingDirection *= -1;  // Inverte a direção de virada
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);  // Faz o flip
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // Velocidade de movimento
    private Rigidbody2D rb;       // Para controlar a f�sica do jogador
    private Vector2 movement;     // Para armazenar a dire��o do movimento
    private int facingDirection = 1;  // Vari�vel para controlar a dire��o de virada do personagem

    // Start � chamado quando o script � inicializado
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // Obt�m o componente Rigidbody2D do jogador
    }

    // Update � chamado uma vez por frame
    void Update()
    {
        ProcessInput();  // Processa a entrada do jogador
    }

    // FixedUpdate � chamado uma vez por frame, mas � usado para f�sica, por isso � ideal para movimenta��o
    void FixedUpdate()
    {
        MoveCharacter();  // Aplica o movimento f�sico
    }

    // Fun��o para processar a entrada do jogador
    void ProcessInput()
    {
        // Obt�m a dire��o do movimento a partir das teclas W, A, S, D ou setas
        float moveX = Input.GetAxisRaw("Horizontal");  // Para a dire��o horizontal (esquerda/direita)
        float moveY = Input.GetAxisRaw("Vertical");    // Para a dire��o vertical (cima/baixo)

        movement = new Vector2(moveX, moveY).normalized;  // Normaliza para evitar movimento diagonal mais r�pido

        // Chama a fun��o de flip se necess�rio
        if (moveX != 0)
        {
            Flip(moveX);  // Se houver movimento horizontal, chama o Flip
        }
    }

    // Fun��o para mover o personagem
    void MoveCharacter()
    {
        rb.velocity = movement * moveSpeed;  // Aplica a velocidade ao Rigidbody2D
    }

    // Fun��o para virar o personagem dependendo da dire��o
    void Flip(float moveX)
    {
        // Se o movimento for para a esquerda, viramos o personagem
        if (moveX > 0 && facingDirection == -1 || moveX < 0 && facingDirection == 1)
        {
            facingDirection *= -1;  // Inverte a dire��o de virada
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);  // Faz o flip
        }
    }
}

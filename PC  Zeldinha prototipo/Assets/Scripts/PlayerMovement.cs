using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator anim; // Animator do personagem
    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (anim == null)
            anim = GetComponent<Animator>(); // Garantir que o Animator foi atribu�do
    }

    void Update()
    {
        // Processar a entrada do jogador (movimento e ataque)
        ProcessInput();

        // Atualizar a anima��o com base no movimento
        UpdateAnimations();
    }

    // Processar a entrada do jogador (movimento e ataque)
    private void ProcessInput()
    {
        // Coletar entradas de movimento (horizontal e vertical)
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // Criar vetor de movimento e normalizar
        movement = new Vector2(horizontal, vertical).normalized;

        // Se o personagem se mover, atualiza a anima��o
        if (movement.magnitude > 0)
        {
            anim.SetBool("isWalking", true);  // Ativa anima��o de Walking
        }
        else
        {
            anim.SetBool("isWalking", false); // Desativa anima��o de Walking
        }

        // L�gica para o ataque (pressione 'Space' para atacar)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("isAttacking"); // Ativar anima��o de ataque
        }
    }

    private void UpdateAnimations()
    {
        // A anima��o de Idle ser� ativada quando o personagem n�o estiver andando
        if (movement.magnitude == 0)
        {
            anim.SetBool("isIdle", true);  // Ativa anima��o de Idle
        }
        else
        {
            anim.SetBool("isIdle", false); // Desativa anima��o de Idle
        }
    }

    private void FixedUpdate()
    {
        // Movimento do personagem (fixo, para n�o ocorrer jitter)
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // Movimenta o personagem com base na entrada do usu�rio
        rb.velocity = movement * moveSpeed;
    }
}

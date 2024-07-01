using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   //Variaveis de movimento //
   public InputAction MoveAction;
   Rigidbody2D rigidbody2d;
   Vector2 move;
   public float speed = 3.5f;

   // Variavel de Pulo //
    public InputAction JumpAction;
    public float jumpForce = 5.0f;
    bool isGrounded;
   //Variaveis de Vida //
   public int maxHealth = 5;
   int currentHealth;
   public int health {get { return currentHealth; } }

   // Variaveis de tempo de Invencibilidade ao tomar dano //
   public float timeInvincible = 3.0f;
   bool isInvincible;
   float damageCooldown;

   // Variaveis de projetil //
   public GameObject projectilePrefab;
   public InputAction launchAction;

   // Variaveis de animação //
   Animator animator;
   Vector2 moveDirection = new Vector2(1,0);

   // Variaveis de dialogo e Música //
   public InputAction talkAction;
   AudioSource audioSource;


    // Start é chamado antes de começar a rodar
    void Start()
   {
      MoveAction.Enable();  

      JumpAction.Enable(); // Linha adicionada
      JumpAction.performed += Jump; // Linha adicionada

      talkAction.Enable();
      //talkAction.performed += FindFriend;

      launchAction.Enable();
      launchAction.performed += Launch;

      rigidbody2d = GetComponent<Rigidbody2D>();
      animator = GetComponent<Animator>();
      audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;
   }

    // Update atualiza a cada frame
    void Update()
    {
        move = MoveAction.ReadValue<Vector2>();

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            moveDirection.Set(move.x, move.y);
            moveDirection.Normalize();
        }

        //animator.SetFloat("Look X", moveDirection.x);
        //.SetFloat("Look Y", moveDirection.y);
        //animator.SetFloat("Speed", move.magnitude);

        // Checar se o jogador está no chão
        isGrounded = Physics2D.Raycast(rigidbody2d.position, Vector2.down, 0.1f, LayerMask.GetMask("Ground")); // Linha adicionada
        
        if (isInvincible)
        {
            damageCooldown -= Time.deltaTime;
            if (damageCooldown < 0)
            {
                isInvincible = false;
            }
        }
    }

    // Fixed update, vai ser atualizado com o sistema de física
    void FixedUpdate()
   {
       Vector2 position = (Vector2)rigidbody2d.position + move * speed * Time.deltaTime;
       rigidbody2d.MovePosition(position);
   }

   // ChangeHealth vai ser relacionado a qualquer mudança de vida do personagem
   public void ChangeHealth (int amount)
   {  
      if(amount < 0)
      {
          if(isInvincible) 
          { 
                return; 
          }
         
          isInvincible = true;
          damageCooldown = timeInvincible;
          animator.SetTrigger("Hit");
      }

      currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
      UIHandler.instance.SetHealthValue(currentHealth / (float)maxHealth);
   }

    //Função para lançamento de projéteis
   void Launch(InputAction.CallbackContext context)
   {
      GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
      Projectile projectile = projectileObject.GetComponent<Projectile>();
      projectile.Launch(moveDirection,300);

      animator.SetTrigger("Launch");
   }

    void Jump(InputAction.CallbackContext context) // Função adicionada
    {
        if (isGrounded)
        {
            rigidbody2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    //Função para realizar dialogo.
    //void FindFriend(InputAction.CallbackContext context)
    // {
    //     // Raycast seria uma espécie de "raio" que vai sair de um lugar, definido com um tamanho
    //     RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, moveDirection, 1.5f, LayerMask.GetMask("NPC"));
    //     if (hit.collider != null)
    //     {   // Se o raio colidir com algo e for um NPC, entao mostre o diálogo.
    //         NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
    //         if (character != null)
    //         {
    //             UIHandler.instance.DisplayDialogue();
    //         }
    //     }
    // }

    // //PlaySound se refere a sons que o personagem pode fazer.
    // public void PlaySound(AudioClip clip)
    // {
    //     audioSource.PlayOneShot(clip);
    // }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
   //Variaveis de movimento que podem ser ajustadas
	public float walkSpeed = 3.0f;
	public float walkStopRate = 0.07f;

   //Componentes a serem chamados
	Rigidbody2D rigidbody2d;
   TouchingDirections touchingDirections;
   public DetectionZone attackZone;
   public DetectionZone cliffDetectionZone;
   Animator animator;
   Damageable damageable;
	 
   // Variaveis de movimento para o codigo 
	public enum WalkableDirection { Right, Left }
	private WalkableDirection _walkDirection;
   private Vector2 walkDirectionVector = Vector2.right;
   private bool hasFlipped = false; // verificar se ele ja virou ou nao
   public WalkableDirection WalkDirection
    {
      get { return _walkDirection;}
      set
      {
         if(_walkDirection != value)
         {  //Trocar Direção
            gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

            if(value == WalkableDirection.Right)
            {
               walkDirectionVector = Vector2.right;
            }
            else if(value == WalkableDirection.Left)
            {
               walkDirectionVector = Vector2.left;
            }  
         }
         _walkDirection = value;
      }
    }

   //Variaveis para detectar um inimigo
   public bool _hasTarget = false;
   public bool HasTarget 
   { get {return _hasTarget; } 
     private set
     {
        _hasTarget = value;
        animator.SetBool("hasTarget",value);
     } 
   }

   //Se ele esta atacando nao pode se mover, variavel auziliar
   public bool CanMove
   {
      get{ return animator.GetBool(AnimationStrings.canMove); }
   }

   public float AttackCD 
   { 
     get {return animator.GetFloat(AnimationStrings.AttackCooldown);}
     private set { animator.SetFloat(AnimationStrings.AttackCooldown, Mathf.Max(value, 0)); }
   }

    void Awake()
   {
     rigidbody2d = GetComponent<Rigidbody2D>();  
     touchingDirections = GetComponent<TouchingDirections>();
     animator = GetComponent<Animator>();
     damageable = GetComponent<Damageable>();
   }

   void Update()
   {
      HasTarget = attackZone.detectedColliders.Count > 0;
      
      if(AttackCD > 0)
         AttackCD -= Time.deltaTime;
   }
   private void FixedUpdate()
   {
      if (!hasFlipped && touchingDirections.IsGrounded && touchingDirections.IsOnWall)
      { //Verificar se ele pode virar ou não
         FlipDirection();
         hasFlipped = true;
      }
      else if (!touchingDirections.IsOnWall || !touchingDirections.IsGrounded)
      {
         hasFlipped = false; // Reseta o auxiliar caso as condições nao forem cumpridas
      }

      if(!damageable.LockVelocity)
      {
         if(CanMove) 
            rigidbody2d.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rigidbody2d.velocity.y);
         else 
            rigidbody2d.velocity = new Vector2(Mathf.Lerp(rigidbody2d.velocity.x, 0, walkStopRate), rigidbody2d.velocity.y);

      }

   }

   private void FlipDirection()
    {
      if(WalkDirection == WalkableDirection.Left)
      {
         WalkDirection = WalkableDirection.Right;
      }
      else if(WalkDirection == WalkableDirection.Right)
      {
         WalkDirection = WalkableDirection.Left;
      }
      else { Debug.LogError("O valor de caminhada nao está setado como algo possivel como esquerda ou direita");   }
    }

   public void OnHit(int damage, Vector2 knockback)
   {
      rigidbody2d.velocity = new Vector2(knockback.x, rigidbody2d.velocity.y + knockback.y);
   }

   public void OnCliffDetected()
   {
      if(touchingDirections.IsGrounded)
      {
         FlipDirection();
      }
   }
}

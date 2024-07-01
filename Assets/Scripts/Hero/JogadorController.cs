using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections) )]
public class JogadorController : MonoBehaviour
{
    //Variaveis de movimento
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    public float jumpImpulse = 8f;
    public float walkSpeed = 7f;
    public float runSpeed = 10f;
    public float airWalkSpeed = 5f;
    public float CurrentMoveSpeed
    {
        get
        {
            if(CanMove)
            {
                if(IsMoving && !touchingDirections.IsOnWall)
                {
                    if(touchingDirections.IsGrounded)
                    {   //No chão
                        if(IsRunning) // Se está correndo
                        {
                            return runSpeed;
                        }
                        else         // Se está andando
                            return walkSpeed;
                    }
                    else
                    {   //No ar
                        return airWalkSpeed;
                    }
                }
                else  //Idle speed is 0  
                    return 0;
            }

            else // Movimento Bloqueado
                return 0;

        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving 
    { 
        get { return _isMoving; }
        private set 
        { 
            _isMoving = value; 
            animator.SetBool(AnimationStrings.isMoving,value);
        }
    }

    [SerializeField]
    private bool _isRunning = false; 
    public bool IsRunning
    {
        get { return _isRunning; }        
        private set 
        { 
            _isRunning = value; 
            animator.SetBool(AnimationStrings.isRunning,value);
        }
    }
    
    public bool _isFacingRight = true;
    public bool IsFacingRight 
    { 
        get {return _isFacingRight;} 
        private set
        {
            if(_isFacingRight != value)
            {   //Vai trocar a direção.
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        } 
    }
    
    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    //Variaveis para pegar componentes do personagem
    new Rigidbody2D rigidbody2D;
    Animator animator;
    

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    void Start()
    {
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rigidbody2D.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rigidbody2D.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity,rigidbody2D.velocity.y);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {   //Mover para direita
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {   //Mover para esquerda
            IsFacingRight = false;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }
    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            IsRunning = true;
        }
        else if(context.canceled)
        {
            IsRunning = false;
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }
}


internal class AnimationStrings
{
    internal static string isMoving = "isMoving";
    internal static string isRunning = "isRunning";
    internal static string yVelocity = "yVelocity";
    internal static string jumpTrigger = "jump";
    internal static string attackTrigger = "attack";
    internal static string canMove= "canMove";
}
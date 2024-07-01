using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class filhodaputaController : MonoBehaviour
{
    //Movimento do arrombado
    new Rigidbody2D rigidbody2D;
    public float speed;
    float input;

    //Pulo da Gazela
    public float jumpForce;
    public LayerMask groundLayer;
    private bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;
    public float jumpTime = 0.35f;
    public float jumpCounter;
    private bool isJump;

    //Sprite do bichinha
    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
       input = Input.GetAxisRaw("Horizontal"); 

        //Checa se esta no chao.           
       isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);   
       if( (isGrounded == true) && Input.GetButtonDown("Jump") )
       {
            isJump = true;
            jumpCounter = jumpTime;
            rigidbody2D.velocity = Vector2.up * jumpForce;
       }          
       if(Input.GetButton("Jump") && (isJump == true) )
       {
            if(jumpCounter > 0)
            {
                rigidbody2D.velocity = Vector2.up * jumpForce;
                jumpCounter -= Time.deltaTime;
            }
            else { isJump = false; }
                
       } 
       if(Input.GetButtonUp("Jump") ) { isJump = false; }
    }

    void FixedUpdate()
    {
       if(input < 0) // ta invertido por causa do sprite de merda
        spriteRenderer.flipX = false;
       else if(input > 0)
         spriteRenderer.flipX = true;
        rigidbody2D.velocity = new Vector2(input * speed, rigidbody2D.velocity.y);
    }
}

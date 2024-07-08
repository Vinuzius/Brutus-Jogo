using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
  //Variaveis para falar os limites das direções
    public ContactFilter2D castFilter;
    public float grounDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    
    RaycastHit2D[] groundhits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    
    //Variaveis de componentes
    CapsuleCollider2D touchingCol;
    Animator animator;

    //Variaveis para checkar se está no chão, na parede ou no teto
    [SerializeField]
    private bool _isGrounded = true;
    public bool IsGrounded 
    { get{return _isGrounded; } 
      private set
      {
        _isGrounded = value;
        animator.SetBool("isGrounded",value);
      }
    }
    
    [SerializeField]
    private bool _isOnWall = true;
    public bool IsOnWall 
    { get{return _isOnWall; } 
      private set
      {
        _isOnWall = value;
        animator.SetBool("isOnWall",value);
      }
    }    
    
    [SerializeField]
    private bool _isOnCeiling = true;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCeiling 
    { get{return _isOnCeiling; } 
      private set
      {
        _isOnCeiling = value;
        animator.SetBool("isOnCeiling",value);
      }
    }

    private void Awake()
    {   
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundhits, grounDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
    }

}

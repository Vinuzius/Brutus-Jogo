using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
	 public float walkSpeed = 3.0f;
	 
	 Rigidbody2D rigidbody2d;
	 
	 public enum WalkableDirection { Right, Left }
	 private WalkableDirection _walkDirection;
	 public WalkableDirection WalkDirection
	 {
	 	get { return _walkDirection }
	 	set 
	 	{
	 		if(_walkDirection  ! = value)
	 		{	//Trocar a direção
	 			gameObject.transform.localScale = new Vector2(gameoObject.tansform.localScale.x * -1, gameoObject.tansform.localScale.y);
	 			
	 			if(value = WalkableDirection.Right)
	 			{
	 			}
	 		}
	 	 _walkDirection = value; 
	 	}
	 	
	 } 
    // Start is called before the first frame update
    void Awake()
    {
       rigidbody2d = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
    		GetComponent<Rigidbody2D>().velocity = new Vector2(walkSpeed * Vector2.right.x, rigidbody2d.velocity.y);
    }    
    
    void Start()
    {
    
    }
    void Update()
    {  
    
    }
}

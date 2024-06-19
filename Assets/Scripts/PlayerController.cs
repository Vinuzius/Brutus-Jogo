using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   public InputAction MoveAction;
   Rigidbody2D rigidbody2d;
   Vector2 move;
   
   void Start()
   {
      MoveAction.Enable();  
      rigidbody2d = GetComponent<Rigidbody2D>();
   }


   void Update()
   {
     move = MoveAction.ReadValue<Vector2>();
     Debug.Log(move);
   }


   void FixedUpdate()
   {
      Vector2 position = (Vector2)rigidbody2d.position + move * 3.0f * Time.deltaTime;
      rigidbody2d.MovePosition(position);

   }
}

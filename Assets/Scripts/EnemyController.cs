using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
   //Movimento
    Rigidbody2D rigidbody2d;
    float speed = 2.5f; 
    public bool vertical;

    //Patrulha
    int direction = 1;
    float changeTime = 1.5f;
    float timer; 

    bool aggressive = true;
   void Start()
   {
       rigidbody2d = GetComponent<Rigidbody2D>();
       timer = changeTime;
   }

   void Update()
   {
       timer-= Time.deltaTime; // Diminui o timer

      if (timer < 0)
      { // Quando timer menor que 0 vai trocar a direção e reseta o timer
        direction = -direction;
        timer = changeTime;
        int randomNumber = Random.Range(0, 10);
        if(randomNumber % 2 == 0) { vertical = false; }
        else { vertical = true; }
      }
   }

  void FixedUpdate()
  {    
        if(!aggressive) { return; }
       Vector2 position = rigidbody2d.position;
       if (vertical)
       {
           position.y = position.y + speed * direction * Time.deltaTime;
       }
       else
       {
           position.x = position.x + speed * direction * Time.deltaTime;
       }
       rigidbody2d.MovePosition(position);


  }


   void OnCollisionEnter2D(Collision2D other)
   {
       PlayerController player = other.gameObject.GetComponent<PlayerController>();

       if (player != null)
       {
           player.ChangeHealth(-1);
           Debug.Log("Collision "+ other.gameObject);
       }
   }

    public void Fix()
    {
        aggressive = false;
        rigidbody2d.simulated = false;
        //Destroy(gameObject);
    }

}
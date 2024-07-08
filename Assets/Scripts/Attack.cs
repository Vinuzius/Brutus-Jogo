using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void OnTriggerEnter2D(Collider2D collision) 
    {   //Verificar se pode ser acertável.
        Damageable damagable= collision.GetComponent<Damageable>();

        if (damagable != null)
        {   //Se ele existe então pode acertar

            Vector2 acertouKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damagable.Hit(attackDamage, acertouKnockback);

            if (gotHit)
                Debug.Log(collision.name + "hit for " + attackDamage);
        }
        
    }
}

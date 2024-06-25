using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {   // Quando os 2 objetos se encontrarem
        PlayerController controller = other.GetComponent<PlayerController>();

        if( (controller != null) && (controller.health < controller.maxHealth) )
        {   // Se for um player, e ele tiver com a vida não cheia
            controller.ChangeHealth(1);
            controller.PlaySound(collectedClip);
            Destroy(gameObject);
        }
    }
}

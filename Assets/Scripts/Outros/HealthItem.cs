using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthRecupera = 10;
    public Vector3 spinVelocidade = new Vector3(0,180,0);

    private void Update() 
    {
        transform.eulerAngles += spinVelocidade * Time.deltaTime;    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable)
        {   //Se o personagem pode sofrer dano
            bool Cura = damageable.Heal(healthRecupera);
            if(Cura) // Se curou, então destroi
                Destroy(gameObject);
        }
    }

}

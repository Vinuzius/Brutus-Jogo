using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   //Detectar os novos colisores dentro da area
        detectedColliders.Add(collision);
    }   
    private void OnTriggerExit2D(Collider2D collision)
    {   //Remover os colisores que sairam
        detectedColliders.Remove(collision);

        if(detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }

}

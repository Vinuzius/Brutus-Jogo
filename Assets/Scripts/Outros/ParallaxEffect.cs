using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    //StartPosition do objeto do Parallax
    Vector2 startingPosition;

    //O valor inicial de Z do parallax
    float startingZ;

    //Distancia da camera se moveu da posição inicial do objeto parallax
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // Quanto mais distante se move do jogador, mais rapido o efeito vai ser. Pegando o valor de Z mais perto do alvo para parecer mais devagar
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;
    
    void Start()
    {
        startingPosition = transform.position;
        startingZ= transform.position.z;
    }

    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x,newPosition.y, startingZ);
    }
}

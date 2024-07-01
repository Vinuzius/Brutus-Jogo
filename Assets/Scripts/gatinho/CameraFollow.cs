using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Referência ao Transform do personagem
    public float smoothSpeed = 0.125f; // Velocidade de suavização da câmera
    public Vector3 offset; // Offset da câmera em relação ao personagem

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // Posição desejada da câmera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime); // Suavização da posição da câmera

        transform.position = smoothedPosition; // Atualiza a posição da câmera suavizada
    }
}

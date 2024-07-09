using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    public Damageable damageable;
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Scene cenaAtual = SceneManager.GetActiveScene();
            SceneManager.LoadScene(cenaAtual.name);
        }
    }

    public void Death()
    {
        Damageable personagem = damageable;
        if(personagem.Health <= 0)
        {
            Debug.Log("voce morreu");
        }
    }
}

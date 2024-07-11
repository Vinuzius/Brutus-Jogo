using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    //[SerializeField] private JogadorController player;
    [SerializeField] private Damageable damage;

    private void Start() {
        damage = GetComponent<Damageable>();
    }
    private void Update() 
    {
       if(damage.Health <=0)
        Death();
    }
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
        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }
}

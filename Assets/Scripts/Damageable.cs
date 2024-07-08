using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;

    //Variaveis de vida
    [SerializeField]    // Vida máxima
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]    // Vida Atual
    private int _health = 100;
    public int Health
    {
        get{ return _health; }
        set
        {
            _health = value;
             
            // Caso a vida esteja abaixo de 0, então o personagem está morto
            if(_health <= 0)
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]    // Se está vivo
    private bool _isAlive = true;

    [SerializeField]    // Invencibilidade
    private bool isInvincible = false;

    private float timeSinceHit = 0;
    public float invencibleTimer = 0.25f;

    public bool IsAlive 
    { 
        get{ return _isAlive;} 
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
        } 
    }
    
    public bool LockVelocity 
    {   // Quando está fazendo uma ação, locka o movimento.
        get{ return animator.GetBool(AnimationStrings.lockVelocity); } 
        set{ animator.SetBool(AnimationStrings.lockVelocity,value); } 
    }

    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }
    
    private void Update() 
    {
        if(isInvincible)
        {
            if(timeSinceHit > invencibleTimer)
            { //Remover invencibilidade
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit +=  Time.deltaTime;
        }
    }

    
    public bool Hit(int damage, Vector2 knockback)
    {   //Returna se conseguiu dar dano ou não
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);

            return true;
        }

        //Caso nao consiga acertar
        return false;
    }

    public bool Heal(int healthRecupera)
    {
        if(IsAlive)
        {
            int Curou = Health;
            Health = Mathf.Clamp(Health + healthRecupera, 0, MaxHealth);
            if(Curou != Health) // Se ele curou então é verdadeiro 
                return true;
        }
        // Caso nao tenha curado é falso
        return false;
    }
}

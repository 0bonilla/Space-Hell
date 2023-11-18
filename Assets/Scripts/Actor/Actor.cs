using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable
{

    public int MaxLife => stats.MaxLife;
    public int CurrentLife => currentLife;
    public float NextShot => nextShot;

    [SerializeField] public ActorStats stats;

    public float nextShot;

    public int currentLife;

    public bool Currentinvincible;

    public float invincibleTime;

    public bool isDeath;

    public GameObject bullet;


    protected void Start()
    {
        currentLife = MaxLife;
    }

    public virtual void TakeDamage(int damage)
    {
        if (!Currentinvincible)
        {
            currentLife -= damage;
            Debug.Log($"{name} tiene {currentLife}!");
        }

        if (currentLife <= 0)
        {
            Debug.Log($"{name} murio");
            Die();
        }
    }

    public virtual void Die()
    {
        isDeath = true;
        Destroy(gameObject);
    }
}

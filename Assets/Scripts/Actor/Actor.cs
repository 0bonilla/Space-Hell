using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IDamagable
{

    public int MaxLife => stats.MaxLife;
    public int CurrentLife => currentLife;
    public float NextShot => nextShot;

    [SerializeField] protected ActorStats stats;

    public float nextShot;

    private int currentLife;

    public GameObject bullet;

    protected void Start()
    {
        currentLife = MaxLife;
    }

    public virtual void TakeDamage(int damage)
    {
        currentLife -= damage;
        Debug.Log($"{name} tiene {currentLife}!");

        if (currentLife <= 0)
        {
            Debug.Log($"{name} murio");
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}

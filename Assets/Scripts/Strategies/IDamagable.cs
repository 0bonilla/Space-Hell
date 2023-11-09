using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int MaxLife { get; }
    int CurrentLife { get; }

    void TakeDamage(int damage);

    void Die();
}

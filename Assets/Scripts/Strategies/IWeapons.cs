using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    GameObject Bullet { get; }

    Transform Shoot { get; }
    int Damage { get; }

    int MagSize { get; }

    void Attack();

    void Reload();
}

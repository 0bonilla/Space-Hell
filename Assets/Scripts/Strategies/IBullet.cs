using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    float Speed { get; }
    float LifeTime { get; }
    LayerMask HittableLayer { get; }

    IWeapon Owner { get; }
    void init();
    void Travel();

    void SetOwner(IWeapon weapon);
}

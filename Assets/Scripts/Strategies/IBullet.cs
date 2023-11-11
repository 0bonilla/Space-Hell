using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBullet
{
    float Speed { get; }
    IWeapon Owner { get; }
    LayerMask HittableLayer { get; }
    void Travel();

    void SetOwner(IWeapon weapon);
}

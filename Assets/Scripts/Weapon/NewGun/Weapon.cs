using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected int damage;
    [SerializeField] protected int magSize;
    [SerializeField] protected int bulletCount;
    [SerializeField] protected Transform shoot;

    public GameObject Bullet => bullet;

    public int Damage => damage;

    public int MagSize => magSize;

    public Transform Shoot => shoot;

    public virtual void Attack() => Debug.Log("SHOOT!!");

    public void Reload() => bulletCount = magSize;
}

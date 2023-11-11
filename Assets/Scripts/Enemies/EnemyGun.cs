using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour, IWeapon
{
    public GameObject Bullet => bullet;
    public int Damage => damage;
    public Transform Shoot => shoot;
    public int MagSize => throw new System.NotImplementedException();

    [SerializeField] private GameObject bullet;
    [SerializeField] private int damage = 1;
    [SerializeField] protected Transform shoot;

    public void Attack()
    {
        GameObject b = Instantiate(bullet, shoot.transform.position, shoot.transform.rotation);
        b.GetComponent<BallController>().SetOwner(this);
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }
}

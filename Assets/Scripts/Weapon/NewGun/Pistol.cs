using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public GameObject Bullet => bullet;

    public int Damage => damage;

    public int MagSize => magSize;

    public Transform Shoot => shoot;

    [SerializeField] private GameObject bullet;
    [SerializeField] private int damage = 3;
    [SerializeField] private int magSize = 30;
    [SerializeField] private int bulletCount;
    [SerializeField] protected Transform shoot;
    private float cooldown;
    [SerializeField] private float cooldownTime = 0.4f;


    private void Start()
    {
        Reload();
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
    }

    public void Reload()
    {
        bulletCount = magSize;
    }

    public void Attack()
    {
        if (bulletCount > 0 && cooldown > cooldownTime)
        {
            GameObject b = Instantiate(bullet, shoot.transform.position, shoot.transform.rotation);
            b.GetComponent<BallController>().SetOwner(this);
            bulletCount--;
            cooldown = 0;
        }
    }
}

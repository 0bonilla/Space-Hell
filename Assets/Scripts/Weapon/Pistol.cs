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
    [SerializeField] private int magSize;
    public int bulletCount;
    [SerializeField] protected Transform shoot;
    private float cooldown;
    [SerializeField] private float cooldownTime = 0.4f;
    [SerializeField] private int pistolType;

    private BulletCounter Counter;

    private void Start()
    {
        Reload();

        Counter = GameObject.Find("Canvas").GetComponent<BulletCounter>();
    }

    private void Update()
    {
        cooldown += Time.deltaTime;
        Counter.UpdateAmmo(bulletCount);
    }

    public void Reload()
    {
        bulletCount = magSize;
    }

    public void Attack()
    {
        if (bulletCount > 0 && cooldown > cooldownTime)
        {
            if(pistolType == 0)
                SoundManager.Instance.PlayPlayerSFX("PlayerShootPistol");
            else if (pistolType == 1)
                SoundManager.Instance.PlayPlayerSFX("PlayerShootRifle");

            GameObject b = Instantiate(bullet, shoot.transform.position, shoot.transform.rotation);
            b.GetComponent<BallController>().SetOwner(this);
            bulletCount--;
            cooldown = 0;
        }

    }
}

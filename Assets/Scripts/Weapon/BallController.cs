using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class BallController : MonoBehaviour, IBullet
{
    [SerializeField] private float speed;
    [SerializeField] public IWeapon owner;
    [SerializeField] public LayerMask hittableLayer;

    public float Speed => speed;

    public IWeapon Owner =>  owner;

    public LayerMask HittableLayer => hittableLayer;

    void Update()
    {
        Travel();
    }

    public void Travel() => transform.position += transform.right * Time.deltaTime * speed;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & hittableLayer) != 0)
        {
            other.GetComponent<Actor>()?.TakeDamage(owner.Damage);
            Destroy(gameObject);
        }

        if (other.tag == "WallsDoors")
        {
            Destroy(gameObject);
        }
    }

    public void SetOwner(IWeapon weapon) => owner = weapon;

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class BallController : MonoBehaviour, IBullet
{
    public string[] validTags;
    [SerializeField] private float speed;
    [SerializeField] private IWeapon owner;

    private new Collider collider;
    private Rigidbody rb;

    public float Speed => speed;

    public IWeapon Owner =>  owner;

    void Update()
    {
        Travel();
    }

    public void Travel() => transform.position += transform.right * Time.deltaTime * speed;

    void OnTriggerEnter2D(Collider2D myTrigger)
    {
        if (validTags.Contains(myTrigger.gameObject.tag))
        {
            Destroy(this.gameObject);
        }
    }

    public void SetOwner(IWeapon weapon) => owner = weapon;

}
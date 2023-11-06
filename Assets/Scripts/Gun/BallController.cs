using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BallController : MonoBehaviour
{
    public string[] validTags;
    public float speed;
    public Rigidbody2D rb;
    void Update()
    {
        Travel();
    }

    void Travel() => transform.position += transform.right * Time.deltaTime * speed;

    void OnTriggerEnter2D(Collider2D myTrigger)
    {
        if (validTags.Contains(myTrigger.gameObject.tag))
        {
            Destroy(this.gameObject);
        }
    }
}
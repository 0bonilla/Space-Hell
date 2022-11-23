using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBall : MonoBehaviour
{
    public float m_Thrust = 0.2f;
    public Rigidbody2D rb;
    void Start()
    {

    }

    void Update()
    {
        //Apply a force to this Rigidbody in direction of this GameObjects up axis
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * m_Thrust, ForceMode2D.Impulse);
    }

    void OnTriggerEnter2D(Collider2D myTrigger)
    {
        string[] validTags = { "WallsDoors", "Object", "Player"};
        if (validTags.Contains(myTrigger.gameObject.tag))
        {
            Destroy(this.gameObject);
        }
    }
}

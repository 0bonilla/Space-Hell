using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyBall : BallController
{
    void OnTriggerEnter2D(Collider2D myTrigger)
    {
        if (validTags.Contains(myTrigger.gameObject.tag))
        {
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPivot : MonoBehaviour
{
    private GameObject Player;
    public GameObject enemy;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        spriteRenderer = enemy.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Player.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localRotation = Quaternion.Euler(180, 0, -1 * angle);
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPivot: MonoBehaviour
{
    public GameObject Gun;
    private PlayerController player;
    private SpriteRenderer mySpriteRenderer;
    public GameObject aim;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        mySpriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = aim.transform.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            transform.localRotation = Quaternion.Euler(180, 0, -1 * angle);
            mySpriteRenderer.flipX = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
        }
    }
}

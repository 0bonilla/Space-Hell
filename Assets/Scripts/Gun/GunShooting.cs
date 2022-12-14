using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    public GameObject bala;
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Disparo de arma
        timer += Time.deltaTime;
        if (timer > 0.3 && Input.GetButton("Fire1"))
        {
            Instantiate(bala, transform.position, transform.rotation);
            timer = 0;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 10f);

        if (hit)
        {
            Debug.Log("FIRE");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.right) * 10;
        Gizmos.DrawRay(transform.position, direction);
    }
}

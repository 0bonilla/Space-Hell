using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    private float Shoot = 0;
    private float ran = 0;
    private bool canShoot;
    [SerializeField] private GameObject bala;
    // Start is called before the first frame update
    void Start()
    {
        ran = Random.Range(2, 5);

    }

    // Update is called once per frame
    void Update()
    {
        Shoot += Time.deltaTime;
        if (Shoot > ran && canShoot)
        {
            Instantiate(bala, transform.position, transform.rotation);
            ran = Random.Range(1.0f, 3.0f);
            Shoot = 0;
            
        }
    }

    public void EnableShooting(bool IsEnabled)
    {
        canShoot = IsEnabled;
    }
}

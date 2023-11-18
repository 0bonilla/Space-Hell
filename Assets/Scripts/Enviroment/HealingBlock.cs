using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingBlock : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    [SerializeField] private int Healing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            if(player.currentLife + Healing > player.MaxLife)
            {
                player.currentLife = player.MaxLife;
            }
            else
            {
                player.currentLife += Healing;
            }
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLights : MonoBehaviour
{
    private PlayerController player;
    public Transform aim;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(aim);
        if (player.isDeath)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}

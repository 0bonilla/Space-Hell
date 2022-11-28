using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private PlayerController player;
    public Renderer Rend;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        Rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDeath)
        {
            Rend.enabled = false;
        }
        else
        {
            Rend.enabled = true;
        }
    }
}

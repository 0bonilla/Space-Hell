using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;

    [SerializeField] private PlayerController player;

    // Update is called once per frame
    void Update()
    {
        if(!player.canDash)
        {
            cooldownImage.fillAmount += Time.deltaTime / player.dashingCooldown;
        }

        if (player.isDashing)
        {
            cooldownImage.fillAmount = 0;
        }
    }
}

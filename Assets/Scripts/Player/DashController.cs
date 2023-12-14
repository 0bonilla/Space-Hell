using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{
    [SerializeField] private Image cooldownImage;

    [SerializeField] private PlayerController player;

    public bool isDashing;
    public float dashingPower = 1f;
    [SerializeField] private float dashingTime = 0.3f;
    public float dashingCooldown = 2f;

    // Update is called once per frame
    void Update()
    {
        if(!player.canDash)
        {
            cooldownImage.fillAmount += Time.deltaTime / dashingCooldown;
        }

        if (isDashing)
        {
            SoundManager.Instance.PlayPlayerSFX("Dash");
            cooldownImage.fillAmount = 0;
        }
    }

    public IEnumerator Dash()
    {
        player.canDash = false;
        isDashing = true;
        dashingPower = 2.5f;
        player.trail.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        dashingPower = 1;
        player.trail.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        player.canDash = true;
    }
}

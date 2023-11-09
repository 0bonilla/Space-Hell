using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    Rigidbody2D body;
    //Movement
    float horizontal;
    float vertical;
    public float runSpeed = 20f;

    //Dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 1f;
    [SerializeField] private float dashingTime = 0.3f;
    private float dashingCooldown = 2f;

    //Range
    public int playerRange;
    public int playerSafeRange;

    //Health
    public int PlayerHP;
    public int PlayerTotalHP;
    public bool Defeat;
    private bool GotHit;
    private float invincible;
    public float invincibleTime;

    //Animations
    private Animator Animator;
    private bool Mov;
    public bool isDeath;

    //Render
    public Renderer Rend;
    private bool flashingRender = true;
    [SerializeField] private TrailRenderer trail;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDeath == false)
        {
            CharacterMovement();
            Iframes();
        }
        
        Animator.SetBool("Movement", Mov == true);
        Animator.SetBool("IsDeath", isDeath == true);
    }


    void CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0 && isDashing == false)
        {
            dashingPower = 0.7f;
        }
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            Mov = true;
        }
        else
        {
            Mov = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed * dashingPower, vertical * runSpeed * dashingPower);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        dashingPower = 2.5f;
        trail.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        dashingPower = 1;
        trail.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private bool Iframes()
    {
        if (GotHit && invincible < invincibleTime)
        {
            invincible += Time.deltaTime;
            flashingRender = !flashingRender;
            Rend.enabled = flashingRender;
            return true;   
        }
        else
        {
            invincible = 0;
            Rend.enabled = true;
            GotHit = false;
            return false;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBall" && !GotHit)
        {
            PlayerHP--;
            GotHit = true;
            audioSource.Play(0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !GotHit)
        {
            PlayerHP--;
            GotHit = true;
        }
    }

    //private void OnDrawGizmos()
    //{
        
    //    //Dibujo rango de detección del jugador 
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawWireSphere(transform.position, playerRange);

    //    //Dibujo rango el cual el enemigo deja de seguir al player
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, playerSafeRange);
        
    //}

    public void Die()
    {
        base.Die();
        isDeath = true;
        Defeat = true;
        Debug.Log("askldnadas");
    }
}

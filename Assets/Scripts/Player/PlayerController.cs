using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int speed = 5;
    private float DashSpeed = 1;
    private float DashTimer;
    private float DashUse;
    public float DashLimit;

    public int playerRange;
    public int playerSafeRange;

    public int PlayerHP;
    public int PlayerTotalHP;
    public bool Defeat;

    private Animator Animator;
    private bool Mov;
    private bool dashAni;
    public bool isDeath;

    private bool GotHit;
    private float invincible;
    public float invincibleTime;

    public Renderer Rend;
    private bool flashingRender = true;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isDeath == false)
        {
            DashControl();
            CharacterMovement();
            Iframes();
        }
        

        Animator.SetBool("Movement", Mov == true);
        Animator.SetBool("DashAni", dashAni == true);
        Animator.SetBool("IsDeath", isDeath == true);

        if (PlayerHP <= 0)
        {
            isDeath = true;
            Defeat = true;
        }
        else
        {
            isDeath = false;
        }
    }


    void CharacterMovement()
    {
        if (Input.GetKey("w"))
        {
            transform.Translate(0, speed * DashSpeed * Time.deltaTime, 0);
            Mov = true;
        }
        else if (Input.GetKey("a"))
        {
            transform.Translate(speed * DashSpeed * Time.deltaTime * -1, 0, 0);
            Mov = true;
        }
        else if(Input.GetKey("s"))
        {
            transform.Translate(0, speed * DashSpeed * Time.deltaTime * -1, 0);
            Mov = true;
        }
        else if(Input.GetKey("d"))
        {
            transform.Translate(speed * DashSpeed * Time.deltaTime, 0, 0);
            Mov = true;
        }
        else
        {
            Mov = false;
        }
    }

    void DashControl()
    {
        if (DashTimer < 2)
        {
            //Para que dash no se pase de 2
            DashTimer += Time.deltaTime;
        }

        if (Input.GetButton("Debug Multiplier") && DashTimer >= 2)
        {
            //Logica del Dash
            DashSpeed = 2.5f;
            dashAni = true;
            DashUse += Time.deltaTime;
            if (DashUse > DashLimit)
            {
                DashUse = 0;
                DashTimer = 0;
                
            }
        }
        else
        {
            DashSpeed = 1;
            dashAni = false;
        }
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

    private void OnDrawGizmos()
    {
        
        //Dibujo rango de detección del jugador 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, playerRange);

        //Dibujo rango el cual el enemigo deja de seguir al player
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerSafeRange);
        
    }
}

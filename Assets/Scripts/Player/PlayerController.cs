using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int speed = 5;
    private float DashSpeed = 1;
    private float DashTimer;
    private float DashUse;
    [SerializeField] private float DashLimit;
    public int playerRange;
    public int playerSafeRange;
    public int PlayerHP;
    private Animator Animator;
    private bool Mov;
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DashControl();
        CharacterMovement();
        Animator.SetBool("Movement", Mov == true);

        if (PlayerHP <= 0)
        {
            Destroy(this.gameObject);
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBall")
        {
            PlayerHP--;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            PlayerHP--;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Actor
{
    Rigidbody2D body;
    //Movement
    float horizontal;
    float vertical;
    private float fixedMovement;

    //Dash
    public bool canDash = true;
    private bool isDashing;
    private float dashingPower = 1f;
    [SerializeField] private float dashingTime = 0.3f;
    private float dashingCooldown = 2f;

    //Range
    public int playerRange;
    public int playerSafeRange;

    //Health
    [SerializeField] private float FlashTime;

    //Animations
    private Animator Animator;
    private bool Mov;
    public bool isDeath;

    //Render
    private Renderer Rend;
    private bool flashingRender = true;
    [SerializeField] private TrailRenderer trail;

    //IWeapon
    [SerializeField] private IWeapon currentWeapon;
    [SerializeField] private List<GameObject> weaponList;

    AudioSource audioSource;
    // Start is called before the first frame update
    new void Start()
    {
        Animator = GetComponent<Animator>();
        Rend = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
        body = GetComponent<Rigidbody2D>();
        SwitchWeapon(0);

        FlashTime = invincibleTime;
        currentLife = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {     
        Animator.SetBool("Movement", Mov == true);
        Animator.SetBool("IsDeath", isDeath == true);

        if (Input.GetKeyDown(KeyCode.Mouse0)) currentWeapon.Attack();
        if (Input.GetKeyDown(KeyCode.R)) currentWeapon.Reload();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) StartCoroutine(Dash());

        Flash();
    }

    private void FixedUpdate()
    {
        Debug.Log(FlashTime);
        if (isDeath == false)
        {
            CharacterMovement();
        }
    }

    void CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * stats.MovementSpeed * dashingPower * fixedMovement, vertical * stats.MovementSpeed * dashingPower * fixedMovement);

        if (horizontal != 0 || vertical != 0)
        {
            Mov = true;
        }
        else
        {
            Mov = false;
        }
        
        if (horizontal != 0 && vertical != 0 && isDashing == false)
        {
            fixedMovement = 0.7f;
        }
        else
        {
            fixedMovement = 1;
        }
    }

    private void SwitchWeapon(int index)
    {
        foreach (GameObject weapon in weaponList)
        {
            weapon.gameObject.SetActive(false);
        }
        weaponList[index].SetActive(true);
        currentWeapon = weaponList[index].GetComponent<IWeapon>(); ;
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

    private IEnumerator Iframes()
    {
        Currentinvincible = true;
        audioSource.Play(0);
        Debug.Log("soy invencible");
        yield return new WaitForSeconds(FlashTime);
        Debug.Log("no soy invencible");
        Currentinvincible = false;
        Rend.enabled = true;
    }

    public void Flash() 
    {
        if (Currentinvincible)
        {
            flashingRender = !flashingRender;
            Rend.enabled = flashingRender;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBall" && !Currentinvincible)
        {
            StartCoroutine(Iframes());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !Currentinvincible)
        {
            StartCoroutine(Iframes());
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
}

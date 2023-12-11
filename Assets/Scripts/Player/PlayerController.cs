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
    private float speed;

    //Dash
    public bool canDash = true;
    public bool isDashing;
    private float dashingPower = 1f;
    [SerializeField] private float dashingTime = 0.3f;
    public float dashingCooldown = 2f;

    //Range
    public int playerRange;
    public int playerSafeRange;

    //Health
    [SerializeField] private float FlashTime;

    //Animations
    private Animator Animator;
    private bool Mov;

    //Render
    private Renderer Rend;
    private bool flashingRender = true;
    [SerializeField] private TrailRenderer trail;

    //IWeapon
    [SerializeField] private IWeapon currentWeapon;
    [SerializeField] private List<GameObject> weaponList;
    private int weaponIndex;

    //Light
    [SerializeField] private new GameObject light;
    public bool LightActive;


    // Start is called before the first frame update
    new void Start()
    {
        weaponIndex = 0;
        Animator = GetComponent<Animator>();
        Rend = GetComponent<Renderer>();
        body = GetComponent<Rigidbody2D>();
        SwitchWeapon(weaponIndex);

        FlashTime = invincibleTime;
        currentLife = MaxLife;

        LightActive = true;
        speed = stats.MovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {     
        if(currentLife <= 0)
        {
            isDeath = true;
        }

        Animator.SetBool("Movement", Mov == true);
        Animator.SetBool("IsDeath", isDeath == true);

        if (Input.GetKeyDown(KeyCode.Mouse0) && !gameManager.Instance.onMenu)
        {
            currentWeapon.Attack();
        }

        if (Input.GetKeyDown(KeyCode.R)) currentWeapon.Reload();
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            SoundManager.Instance.PlayPlayerSFX("Dash");
            StartCoroutine(Dash());
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponIndex = 0;
            SwitchWeapon(weaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponIndex = 1;
            SwitchWeapon(weaponIndex);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            TurnLight();
        }

        Flash();
    }

    private void FixedUpdate()
    {
        if (isDeath == false)
        {
            CharacterMovement();
        }
    }

    void CharacterMovement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(horizontal * speed * dashingPower * fixedMovement, vertical * stats.MovementSpeed * dashingPower * fixedMovement);

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
        //Debug.Log("soy invencible");
        yield return new WaitForSeconds(FlashTime);
        //Debug.Log("no soy invencible");
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

    public void TurnLight()
    {
        LightActive = !LightActive;

        if (LightActive)
        {
            light.SetActive(true);
            speed = stats.MovementSpeed;
        }
        else
        {
            light.SetActive(false);
            speed = 3.5f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBall" && !Currentinvincible)
        {
            SoundManager.Instance.PlayPlayerSFX("PlayerDamage");
            StartCoroutine(Iframes());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !Currentinvincible)
        {
            StartCoroutine(Iframes());
            currentLife--;
        }
    }

    public override void Die()
    {
        base.Die();
        isDeath = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Pathfinding;

public class EnemyFollowPlayer : Actor
{
    private PlayerController player;
    private EnemyGun ScriptGun;
    public GameObject Objectgun;
    public Transform target;
    [SerializeField] private AIPath aiPath;

    private float cooldown;

    private float PlayerRange;
    private float PlayerSafeRange;
    public LayerMask playerLayer;
    private bool playerInRange;
    private bool playerInSafeRange;

    private Animator Animator;
    private bool Mov;
    private bool AnimGotHit;

    // Use this for initialization
    new void Start()
    {
        player = FindObjectOfType<PlayerController>();
        PlayerRange = player.playerRange;
        PlayerSafeRange = player.playerSafeRange;
        Animator = GetComponent<Animator>();
        ScriptGun = Objectgun.GetComponent<EnemyGun>();
        aiPath = GetComponent<AIPath>();
        aiPath.maxSpeed = stats.MovementSpeed;
        aiPath.canMove = false;

        currentLife = MaxLife;
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, PlayerRange, playerLayer);
        playerInSafeRange = Physics2D.OverlapCircle(transform.position, PlayerSafeRange, playerLayer);

        Animator.SetBool("Movement", Mov == true);
        Animator.SetBool("GotHit", AnimGotHit == true);
        Animator.SetBool("IsDeath", isDeath == true);

        cooldown += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (isDeath == false && playerInRange)
        {
            aiPath.canMove = true;
            Mov = true;
        }
        DamageAnimation(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            DamageAnimation(true);
        }

        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            //player.GetDamage(1);
        }
    }


    private void DamageAnimation(bool gotHit)
    {
        AnimGotHit = gotHit;
    }
}

using UnityEngine;
using System.Collections;
 
public class EnemyBehavior: MonoBehaviour
{
    private PlayerController player;
    private EnemyGun ScriptGun;
    public GameObject Objectgun;
    public Transform target;

    public float moveSpeed;
    public int EnemySHP;

    private float PlayerRange;
    private float PlayerSafeRange;
    public LayerMask playerLayer;
    private bool playerInRange;
    private bool playerInSafeRange;

    private Animator Animator;
    private bool Mov;
    private bool AnimGotHit;
    private bool isDeath;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        PlayerRange = player.playerRange;
        PlayerSafeRange = player.playerSafeRange;
        Animator = GetComponent<Animator>();
        ScriptGun = Objectgun.GetComponent<EnemyGun>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, PlayerRange, playerLayer);

        playerInSafeRange = Physics2D.OverlapCircle(transform.position, PlayerSafeRange, playerLayer);

        Animator.SetBool("Movement", Mov == true);
        Animator.SetBool("GotHit", AnimGotHit == true);
        Animator.SetBool("IsDeath", isDeath == true);

        if(isDeath == false)
        {
            if (playerInRange && !playerInSafeRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
                Mov = true;
                ScriptGun.EnableShooting(true);
            }
            else
            {
                Mov = false;
            }
        }
        

        if (EnemySHP <= 0)
        {
            isDeath = true;
            Destroy(gameObject);
        }

        DamageAnimation(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            EnemySHP--;
            DamageAnimation(true);
        }
        else
        {
            
        }
    }

    private void DamageAnimation(bool gotHit)
    {
        AnimGotHit = gotHit;
    }

}
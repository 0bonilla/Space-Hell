
using UnityEngine;
using System.Collections;
 
public class EnemyBehavior: MonoBehaviour
{
    private PlayerController player;
    public GameObject Objectgun;
    public float moveSpeed;
    private float PlayerRange;
    private float PlayerSafeRange;
    public LayerMask playerLayer;
    private bool playerInRange;
    private bool playerInSafeRange;
    private EnemyGun ScriptGun;
    public Transform target;
    public int EnemySHP;
    private Animator Animator;
    private bool Mov;

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

        if (EnemySHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ball")
        {
            EnemySHP--;
        }
    }

}
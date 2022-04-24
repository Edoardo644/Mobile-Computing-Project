using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    //attack
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int dmg;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private EdoHealth playerH;

    //moving
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    public bool movingLeft;
    public bool move = true;
    public bool shield;
    private float rightEdge;
    private float leftEdge;


    private void Awake()
    {
        rightEdge = transform.position.x + moveDistance;
        leftEdge = transform.position.x - moveDistance;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (move)
        {
            anim.SetBool("moving", move);
            if (PlayerInsight() && !playerH.dead)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    cooldownTimer = 0;
                    anim.SetTrigger("meleeAttack");
                }
            }
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    movingLeft = false;
                }
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                    transform.localScale = Vector3.one;
                }
                else
                {
                    movingLeft = true;
                }
            }
        }

        cooldownTimer += Time.deltaTime;

        
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(box.bounds.size.x * range, box.bounds.size.y), 0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            playerH = hit.transform.GetComponent<EdoHealth>();
        }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, new Vector2(box.bounds.size.x * range, box.bounds.size.y));
    }

    private void DamagePlayer()
    {
        if (PlayerInsight())
        {
            playerH.TakeDamage(dmg);
        }
    }

    private void Moving()
    {
        move = true;
    }

}

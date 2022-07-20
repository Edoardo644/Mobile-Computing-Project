using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    //attack
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EdoHealth player;
    [SerializeField] private int dmg;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    //moving
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    public bool movingLeft;
    public bool move = true;
    private float rightEdge;
    private float leftEdge;

    private Animator anim;

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
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                    transform.localScale = new Vector3(1.5f, 1.5f, 1);
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
                    transform.localScale = new Vector3(-1.5f, 1.5f, 1);
                }
                else
                {
                    movingLeft = true;
                }
            }
        }

        if (PlayerInsight() && !player.dead)
        {
            move = false;
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }

        }

        cooldownTimer += Time.deltaTime;
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance,
            new Vector2(box.bounds.size.x * range, box.bounds.size.y), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance, new Vector2(box.bounds.size.x * range, box.bounds.size.y));
    }

    public void DamagePlayer()
    {
        if (PlayerInsight())
        {
            player.TakeDamage(dmg);
        }
    }

    public void Moving()
    {
        move = true;
    }

    /* public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        //hurt animation
        anim.SetTrigger("hurt");
        move = false;
        anim.SetBool("moving", move);

        if (currentHealth <= 0)
        {
            Die();
        }


    }

    void Die()
    {
        Debug.Log("Enemy Died");
        //die animation
        anim.SetTrigger("die");

        //disable enemy
        this.enabled = false;
    } */

}

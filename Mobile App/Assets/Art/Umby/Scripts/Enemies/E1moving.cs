using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1moving : MonoBehaviour
{
    //attack
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private OldMoving player;
    [SerializeField] private Health playerH;
    private float cooldownTimer = Mathf.Infinity;
    
    //moving
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    public bool movingLeft;
    public bool move = true;
    private float rightEdge;
    private float leftEdge;

    private Animator anim;
    private Rigidbody2D body;

    private void Awake()
    {
        rightEdge = transform.position.x + moveDistance;
        leftEdge = transform.position.x - moveDistance;

        body = GetComponent<Rigidbody2D>();
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
                    transform.localScale = Vector3.one;
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
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    movingLeft = true;
                }
            }
        }

        cooldownTimer += Time.deltaTime;

        if (PlayerInsight() && !playerH.dead)
        {
            move = false;
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
        }
    }

    private void Attacking()
    {
        if (PlayerInsight())
        {
            playerH.TakeDamage(1);
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * (-transform.localScale.x) * colliderDistance, 
            new Vector2(box.bounds.size.x * range, box.bounds.size.y), 0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * (-transform.localScale.x) * colliderDistance, new Vector2(box.bounds.size.x * range, box.bounds.size.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerH.TakeDamage(1);
        }
    }

    private void Die()
    {
        move = false;
        anim.SetTrigger("Die");
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Physics2D.IgnoreLayerCollision(6, 7, true);
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }

    private void Moving()
    {
        move = true;
    }

    private void ColliderActivate()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}

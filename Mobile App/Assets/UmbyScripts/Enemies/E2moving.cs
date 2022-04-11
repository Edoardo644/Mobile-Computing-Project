using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2moving : MonoBehaviour
{
    //attack
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer = Mathf.Infinity;

    //moving
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    public bool move = true;
    private float rightEdge;
    private float leftEdge;

    private Animator anim;
    //private Rigidbody2D body;

    private void Awake()
    {
        rightEdge = transform.position.x + moveDistance;
        leftEdge = transform.position.x - moveDistance;

        //body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        if (PlayerInsight())
        {
            //move = false;
            if (cooldownTimer >= attackCooldown)
            {
                move = false;
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * (-transform.localScale.x) * colliderDistance,
            new Vector2(box.bounds.size.x * range, box.bounds.size.y), 90, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * (-transform.localScale.x) * colliderDistance, new Vector2(box.bounds.size.x * range, box.bounds.size.y));
    }

    private void Shoot()
    {
        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<Fireball>().SetDirection(Mathf.Sign(-transform.localScale.x));
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
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

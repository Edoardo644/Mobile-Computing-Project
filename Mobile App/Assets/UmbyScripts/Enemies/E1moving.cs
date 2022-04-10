using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1moving : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private OldMoving player;
    private float cooldownTimer = Mathf.Infinity;
    private Animator anim;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInsight())
        {
            if(cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("Attack");
            }
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

    private void Charge()
    {
        body.velocity = new Vector2(5f * (-transform.localScale.x), body.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.jump == true)
        {
            //to fix
            anim.SetTrigger("Die");
        }
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }
}

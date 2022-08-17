using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    //attack
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EdoHealth player;
    [SerializeField] private PlayerCombat playerCombat;
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

    //health
    [SerializeField] private float maxHealth;
    private float currentHealth;
    private bool dead;

    private Animator anim;

    private void Awake()
    {
        rightEdge = transform.position.x + moveDistance;
        leftEdge = transform.position.x - moveDistance;
        currentHealth = maxHealth;

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
                    transform.localScale =  Vector3.one;
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
                anim.SetTrigger("attack");
                cooldownTimer = 0;
                FindObjectOfType<AudioManager>().Play("Sword");
            }
        }

        anim.SetBool("moving", move);
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

    private void DamagePlayer()
    {
        if (PlayerInsight() && !playerCombat.rolling)
        {
            player.TakeDamage(dmg);
        }
    }

    private void Moving()
    {
        move = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        FindObjectOfType<AudioManager>().Play("Hit");

        if(currentHealth > 0)
        {
            //hurt animation
            anim.SetTrigger("hurt");
            move = false;
        }
        else if (!dead)
        {
            Die();
        }

     
    }
    
    private void Die()
    {
        Debug.Log("Enemy Died");
        //die animation
        anim.SetTrigger("die");

        //disable enemy
        move = false;
        GetComponent<BoxCollider2D>().enabled = false;
        dead = true;
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}
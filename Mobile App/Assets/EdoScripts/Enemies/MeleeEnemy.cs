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
    [SerializeField] private int dmg;


    private EdoHealth playerH;

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
            //move = false;
            anim.SetBool("moving", false);
            //anim.SetBool("meleeAttack", true);
            anim.SetTrigger("attack");
        }
        else
        {
            //move = true;
            anim.SetBool("moving", true);
            //anim.SetBool("meleeAttack", false);
        }

        
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(box.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance,
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
        Gizmos.DrawWireCube(box.bounds.center + transform.right * range * (transform.localScale.x) * colliderDistance, new Vector2(box.bounds.size.x * range, box.bounds.size.y));
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

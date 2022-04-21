using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossmoving : MonoBehaviour
{
    // transforms
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform topEdge;
    [SerializeField] private Transform enemy;

    // moving parameters
    [SerializeField] private float speed;
    [SerializeField] private float speedy;
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private Vector3 initScale;
    private bool movingLeft = true;
    private bool movingTop = true;
    public bool move = true;

    //attack
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float attackTimer = 0;
    private float attackDuration = 0;
    private float cooldownTimer = Mathf.Infinity;

    //other
    [SerializeField] private Health player;
    private Animator anim;
    private Rigidbody2D body;

    private void Awake()
    {
        initScale = enemy.localScale;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (move)
        {
            Moving();               
        }
        
        if (attackTimer > idleDuration * 3)
        {
            move = false;
            Attack();
        }
    }

    private void Moving()
    {
        if (movingLeft)
        {
            if (transform.position.x > leftEdge.position.x)
            {
                MoveInDirectionX(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else
        {
            if (transform.position.x < rightEdge.position.x)
            {
                MoveInDirectionX(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void MoveInDirectionX(float dir)
    {
        idleTimer = 0;

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * (-dir), initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * dir * speed, enemy.position.y, enemy.position.z);
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }

    private void Attack()
    {
        attackDuration += Time.deltaTime;
        AttackMoving();
        if(cooldownTimer > attackCooldown)
        {
            Shoot();
        }
        if(attackDuration > 4)
        {
            attackTimer = 0;
            attackDuration = 0;
            move = true;
        }
    }

    private void AttackMoving()
    {
        if (movingTop)
        {
            if (transform.position.y < topEdge.position.y)
            {
                MoveInDirectionY(1);
            }
            else
            {
                movingTop = false;
            }
        }
        else
        {
            if (transform.position.y > rightEdge.position.y)
            {
                MoveInDirectionY(-1);
            }
            else
            {
                movingTop = true;
            }
        }
    }

    private void MoveInDirectionY(float dir)
    {
        enemy.position = new Vector3(enemy.position.x, enemy.position.y + Time.deltaTime * dir * speedy, enemy.position.z);
    }

    private void Shoot()
    {
        cooldownTimer = 0;
        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<BossFireball>().SetDirection(Mathf.Sign(transform.localScale.x));
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.TakeDamage(1);
        }
    }
}

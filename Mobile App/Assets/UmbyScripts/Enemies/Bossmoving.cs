using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bossmoving : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform bottomEdge;
    [SerializeField] private Transform enemy;

    [SerializeField] private float speedX;
    [SerializeField] private float idleDuration;
    private float idleTimer;
    private Vector3 initScale;

    private bool movingLeft = true;
    private bool movingDown = true;
    public bool move = true;

    //attack
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] bombs;
    private float attackTimer = 0;
    private float cooldownTimer = Mathf.Infinity;

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
        
        if (attackTimer > idleDuration * 2)
        {
            move = false;
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Attack();
            }
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
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * dir * speedX, enemy.position.y, enemy.position.z);
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
        if(transform.position.y < bottomEdge.position.y)
        {
            enemy.position = new Vector3(enemy.position.x, enemy.position.y + Time.deltaTime * speedX, enemy.position.z);
        }
        else
        {
            Moving();
            Shoot();
        }
    }

    private void Shoot()
    {
        bombs[findBomb()].transform.position = firePoint.position;
        gameObject.SetActive(true);
    }

    private int findBomb()
    {
        for (int i = 0; i < bombs.Length; i++)
        {
            if (!bombs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}

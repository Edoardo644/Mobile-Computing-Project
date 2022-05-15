using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDmg;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
           
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Roll();

        }

    }

    public void Roll()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        animator.SetTrigger("Roll");

    }

    void unRoll()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    public void Attack()
    {

        //play an attack animation
        animator.SetTrigger("Attack");

        //Detect eniemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        //Damage enemies in range
        foreach(Collider2D enemy in hitEnemies)
        {
            // enemy.GetComponent<MeleeHealth>().TakeDamage(attackDmg);
            enemy.GetComponent<DBossHealth>().TakeDamage(attackDmg);
        }
    }

    void onDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}

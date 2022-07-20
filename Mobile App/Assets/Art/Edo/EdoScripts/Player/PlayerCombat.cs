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
    [SerializeField] private EdoHealth player;
    public bool rolling;

    [SerializeField] private float attackCoolDown;
    private float coolDownTimer = Mathf.Infinity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Attack();
           
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Roll();

        }

        coolDownTimer += Time.deltaTime;

    }

    public void Roll()
    {
        if (!player.dead && !rolling)
        {
            Physics2D.IgnoreLayerCollision(6, 7, true);
            animator.SetTrigger("Roll");
            rolling = true;
        }
        

    }

    void unRoll()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
        rolling = false;
    }

    public void Attack()
    {
        if (!player.dead && coolDownTimer > attackCoolDown)
        {
            //play an attack animation
            animator.SetTrigger("Attack");
            coolDownTimer = 0;
            FindObjectOfType<AudioManager>().Play("Sword");

            //Detect eniemies in range of attack
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Damage enemies in range
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.GetComponent<MeleeEnemy>() != null)
                    enemy.GetComponent<MeleeEnemy>().TakeDamage(1);
                if (enemy.GetComponent<DBossHealth>() != null)
                    enemy.GetComponent<DBossHealth>().TakeDamage(1);
            }
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

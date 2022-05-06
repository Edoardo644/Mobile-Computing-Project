using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    [SerializeField] private float attackCoolDown;
    private Animator anim;
    private movement playerMovement;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float range;
    [SerializeField] private Transform attackPoint;
    private float coolDownTimer = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<movement>();
    }
    // Update is called once per frame
    void Update()
    {
        //Attack
        if (Input.GetKeyDown(KeyCode.S))
        {
            Attack();
        }

        coolDownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        if (coolDownTimer > attackCoolDown)
        {
            anim.SetTrigger("attack");
            coolDownTimer = 0;

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, range, enemyLayer);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<health>().TakeDamage(1);
            }
        }
    }

    public void onDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}

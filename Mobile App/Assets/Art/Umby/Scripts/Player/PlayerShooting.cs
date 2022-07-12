using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Health player;
    private Animator anim;
    [SerializeField] private GameObject[] arrows;
    private OldMoving playerMoving;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMoving = GetComponent<OldMoving>();
    }
    // Update is called once per frame
    void Update()
    {
        //Attack
        if(Input.GetKeyDown(KeyCode.S))
        {
            Attack();
        }

        coolDownTimer += Time.deltaTime;
    }

    public void Attack()
    {
        if(coolDownTimer > attackCoolDown && playerMoving.CanAttack() && !player.dead)
        {
            anim.SetTrigger("Shoot");
            coolDownTimer = 0;
        }
    }

    private void Shoot()
    {
        FindObjectOfType<AudioManager>().Play("Arrow");
        arrows[findArrow()].transform.position = firePoint.position;
        arrows[findArrow()].GetComponent<Arrow>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findArrow()
    {
        for(int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}

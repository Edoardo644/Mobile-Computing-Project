using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject[] arrows;
    private PlayerMoving playerMoving;
    private float coolDownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMoving = GetComponent<PlayerMoving>();
    }
    // Update is called once per frame
    void Update()
    {
        //Attack
        if(Input.GetKeyDown(KeyCode.S) && coolDownTimer > attackCoolDown && playerMoving.CanAttack())
        {
            Shoot();
        }

        coolDownTimer += Time.deltaTime;
    }

    private void Shoot()
    {
        anim.SetTrigger("Shoot");
        coolDownTimer = 0;

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

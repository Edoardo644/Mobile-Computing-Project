using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    private Animator anim;
    private PlayerMoving playerMoving;
    private float coolDownTimer = Mathf.Infinity;
    [SerializeField] private GameObject arrowPrefab;

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
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        anim.SetTrigger("Shoot");
        coolDownTimer = 0;

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    [SerializeField] private float attackCoolDown;
    private Animator anim;
    private movement playerMovement;
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
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    /* [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private Animator anim;
    private bool jump;
    private float horizontalInput; */

    [SerializeField] public CharacterController2D controller;
    [SerializeField] public float RunSpeed;
    private float horizontalInput = 0f;

    /* private void Awake()
    {
        //get parameters
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } */

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * RunSpeed;

        //flip player
        /* body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        
        else if(horizontalInput< -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } */
            

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
            
        //set parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Jump", jump);
    }

    private void Jump()
    {

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime, false, jump);
        jump = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            jump = false;
        }
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 && jump == false;
    }
}

        

    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdoPlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Rigidbody2D body;
    private float horizontalInput;

    public Joystick joystick;
    //private Collider2D boxCollider;
    //private Collider2D circleCollider;
    private Animator animator;
    private bool Grounded;

    private void Awake()
    {
        //Grab references from object
        body = GetComponent<Rigidbody2D>();
       // boxCollider = GetComponent<BoxCollider2D>();
        //circleCollider = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        if(joystick.Horizontal >= 0.2f)
        {
            horizontalInput = speed;
        } else if(joystick.Horizontal <= -0.2f)
        {
            horizontalInput = -speed;
        }else
        {
            horizontalInput = 0f;
        }

        float verticalMove = joystick.Vertical;
       

        //moving left and right
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //flipping left and right while moving
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
       else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);


        //jumping
        if ((verticalMove>= .5f && Grounded) || (Input.GetKeyDown(KeyCode.W) && Grounded))
        {
            Jump();
        }


        //Set animator parameters
        animator.SetBool("Run", horizontalInput != 0);
        animator.SetBool("Grounded", Grounded);

    }

    public void Jump()
    {
        //speed andra' modificato con un altra variabile per sistemare il salto

        if (Grounded == true)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            animator.SetTrigger("Jump");
            Grounded = false;

        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            Grounded = true;

    }
}

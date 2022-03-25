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
    [SerializeField] public float runSpeed = 45f;
    [SerializeField] public Animator anim;
    private float horizontalInput = 0f;
    private bool jump = false;

    /* private void Awake()
    {
        //get parameters
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    } */

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * runSpeed;

        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));

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
            jump = true;
            anim.SetBool("Jump", true);
        }
            
        //set parameters
        //anim.SetBool("Jump", jump);
    }

    public void OnLanding()
    {
        anim.SetBool("Jump", false);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalInput * Time.fixedDeltaTime, false, jump);
        jump = false; 
    }

    /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Tilemap")
        {
            jump = false;
        }
    } */

    public bool CanAttack()
    {
        return horizontalInput == 0 && jump == false;
    }
}

        

    

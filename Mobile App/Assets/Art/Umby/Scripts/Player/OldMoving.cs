using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMoving : MonoBehaviour
{
    [SerializeField] private float speed = 4.3f;
    [SerializeField] private float jumpPower = 6.8f;
    [SerializeField] private Joystick joystick;
    private Rigidbody2D body;
    private Animator anim;
    public bool jump;
    private float horizontalInput;
    private float horizontalMove;
    
    public int coins = 0;
    public int gems = 0;

    private void Awake()
    {
        //get parameters
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = joystick.Horizontal;
        horizontalMove = horizontalInput * speed;

        if(joystick.Horizontal >= 0.2f)
        {
            horizontalMove = speed;
        } else if(joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -speed;
        }
        else
        {
            horizontalMove = 0f;
        }

        body.velocity = new Vector2(horizontalMove, body.velocity.y);

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumping();
        }

        //set parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Jump", jump);
    }

    public void Jumping()
    {
        if (!jump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jump = true;
            anim.SetTrigger("Jumping");
            //anim.SetBool("Jump", true);
        }
    }

    public bool CanAttack()
    {
        return horizontalInput == 0 || jump;
    }
}

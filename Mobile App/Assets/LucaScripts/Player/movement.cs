using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float jspeed;
    [SerializeField] private health player;
    [SerializeField] private Joystick joystick;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;
    private float horizontalInput;
    private float horizontalMove;

    public int coins;
    public int totalCoins;
    [SerializeField] private Transform coinHolder;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        totalCoins = coinHolder.childCount;
    }

    private void Update()
    {
        horizontalInput = joystick.Horizontal;
        horizontalMove = horizontalInput * speed;

        if (joystick.Horizontal >= 0.2f)
        {
            horizontalMove = speed;
        }
        else if (joystick.Horizontal <= -0.2f)
        {
            horizontalMove = -speed;
        }
        else
        {
            horizontalMove = 0f;
        }

        body.velocity = new Vector2(horizontalMove, body.velocity.y);

        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("grounded", grounded);
    }

    public void Jump()
    {
        if (grounded && !player.dead)
        {
            body.velocity = new Vector2(body.velocity.x, jspeed);
            anim.SetTrigger("jump");
            grounded = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tilemap" || collision.gameObject.tag == "Platform")
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            player.TakeDamage(1);
        }
    }
}
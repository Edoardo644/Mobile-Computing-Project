using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldMoving : MonoBehaviour
{
    [SerializeField] private float speed = 4.3f;
    [SerializeField] private float jumpPower = 6.8f;
    private Rigidbody2D body;
    private Animator anim;
    private bool jump;
    private float horizontalInput;

    private void Awake()
    {
        //get parameters
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            jump = true;
            anim.SetTrigger("Jumping");
            //anim.SetBool("Jump", true);
        }

        //set parameters
        anim.SetBool("Run", horizontalInput != 0);
        anim.SetBool("Jump", jump);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulLogic : MonoBehaviour
{
    //attack
    [SerializeField] private BoxCollider2D box;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private EdoHealth player;
    [SerializeField] private int dmg;


    private EdoHealth playerH;

    //moving
    [SerializeField] private float moveDistance;
    [SerializeField] private float speed;
    public bool movingLeft;
    public bool move = true;
    private float rightEdge;
    private float leftEdge;

    private Animator anim;

    private void Awake()
    {
        rightEdge = transform.position.x + moveDistance;
        leftEdge = transform.position.x - moveDistance;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (move)
        {
            if (movingLeft)
            {
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
                    transform.localScale = Vector3.one;
                }
                else
                {
                    movingLeft = false;
                }
            }
            else
            {
                if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    movingLeft = true;
                }
            }
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<EdoHealth>().TakeDamage(dmg);
            anim.SetTrigger("death");
            move = false;
            FindObjectOfType<AudioManager>().Play("Explode");
        }
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }

}

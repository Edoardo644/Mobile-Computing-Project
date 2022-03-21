using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed;
    private BoxCollider2D box;
    private Animator anim;
    private bool hit;
    private float direction;
    private float lifetime;

    private void Awake()
    {
        //get parameters
        box = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        float localScaleX = transform.localScale.x;
        gameObject.SetActive(true);
        hit = false;
        if(Mathf.Sign(localScaleX) != dir)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PolygonCollider2D grid;
    [SerializeField] private Health player;
    private BoxCollider2D box;
    private bool hit;
    private float direction;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, -(speed * Time.deltaTime), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != grid && collision.tag != "Coin" && collision.tag != "Gem" && collision.tag != "Health")
        {
            hit = true;
            box.enabled = false;
            anim.SetTrigger("Explode");
            // Deactivate();
        }

        if (collision.tag == "Player")
        {
            player.TakeDamage(1);
        }
    }

    public void SetDirection(float dir)
    {
        direction = dir;
        gameObject.SetActive(true);
        hit = false;
        box.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != dir)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(-localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

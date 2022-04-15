using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private PolygonCollider2D grid;
    [SerializeField] private Health player;
    private CircleCollider2D box;
    private bool hit;
    private float direction;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        box = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        transform.Translate(0, -(speed * Time.deltaTime), 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != grid && collision.tag != "Platform")
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

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

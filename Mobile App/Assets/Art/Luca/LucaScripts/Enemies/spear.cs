using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spear : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Spear Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private float timer;
    private Animator anim;
    private SpriteRenderer spriteRend;
    private BoxCollider2D box;
    private float t;

    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        t += Time.deltaTime;

        if (t >= timer)
        {
            StartCoroutine(ActivateSpear());
            t = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active)
        {
            collision.GetComponent<health>().TakeDamage(damage);
        }
    }

    private IEnumerator ActivateSpear()
    {
        yield return new WaitForSeconds(activationDelay);
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        anim.SetBool("activated", false);
    }

    private void ActivateCollider()
    {
        box.enabled = true;
    }

    private void DisableCollider()
    {
        box.enabled = false;
    }
}

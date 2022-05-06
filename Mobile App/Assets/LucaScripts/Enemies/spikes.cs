using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{
    [SerializeField] private float damage;

    [Header("Spikes Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!triggered)
            {
                StartCoroutine(ActivateSpikes());
            }
            if (active)
            {
                collision.GetComponent<health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateSpikes()
    {
        triggered = true;
        yield return new WaitForSeconds(activationDelay);
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        triggered = false;
        active = false;
        anim.SetBool("activated", false);
    }
}

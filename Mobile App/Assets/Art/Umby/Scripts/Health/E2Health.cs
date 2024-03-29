using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;
    private E2moving enemy;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer rend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        enemy = GetComponent<E2moving>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        FindObjectOfType<AudioManager>().Play("Hurt");

        if (currentHealth > 0)
        {
            //hurt
            enemy.move = false;
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
        }

        else
        {
            //die
            if (!dead)
            {
                anim.SetTrigger("Die");
                enemy.move = false;
                GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                GetComponent<BoxCollider2D>().isTrigger = false;
                Physics2D.IgnoreLayerCollision(6, 7, true);
                dead = true;
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        for (int i = 0; i < numOfFlashes; i++)
        {
            rend.color = new Color(0.963014f, 0.3433962f, 1, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
            rend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arrow")
        {
            TakeDamage(1);
        }
    }

    private void ColliderActivate()
    {
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}

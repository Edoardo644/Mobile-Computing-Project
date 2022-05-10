using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private OldMoving player;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numOfFlashes;
    private SpriteRenderer rend;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        player = GetComponent<OldMoving>();
    }

    private void Update()
    {
        if (GetComponent<Transform>().position.y <= -10f)
        {
            TakeDamage(3);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //hurt
            anim.SetTrigger("Hurt");
            StartCoroutine(Invulnerability());
        }

        else
        {
            //die
            if (!dead)
            {
                anim.SetTrigger("Die");
                GetComponent<OldMoving>().enabled = false;
                GetComponent<CapsuleCollider2D>().enabled = false;
                dead = true;
            }
        }
    }

    public void TakeHealth(float damage)
    {
        if(currentHealth < startingHealth && currentHealth > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + damage, 0, startingHealth);
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

    /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "AltEnemy" && player.jump == false)
        {
            TakeDamage(1);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            TakeDamage(1);
        }
    } */
}

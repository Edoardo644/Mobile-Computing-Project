using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    private OldMoving player;
    private Rigidbody2D body;
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
        body = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        player = GetComponent<OldMoving>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /* if(collision.gameObject.tag == "Tilemap" && dead == true)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        } */

        if(collision.gameObject.tag == "Enemy" || (collision.gameObject.tag == "AltEnemy" && player.jump == false))
        {
            TakeDamage(1);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
}

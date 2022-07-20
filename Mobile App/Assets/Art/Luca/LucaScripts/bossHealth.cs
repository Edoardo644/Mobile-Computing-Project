using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    public bool dead { get; private set; }

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        FindObjectOfType<AudioManager>().Play("Hit");

        if (currentHealth > 0)
        {
            anim.SetTrigger("hit");
            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                GetComponent<boss>().enabled = false;
                dead = true;
            }
        }

    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(1);
        }
    }
    */

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        box.enabled = false;

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        
        Physics2D.IgnoreLayerCollision(6, 7, false);
        box.enabled = true;
    }
}

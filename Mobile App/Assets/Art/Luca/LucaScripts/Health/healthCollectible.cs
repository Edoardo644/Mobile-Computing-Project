using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthCollectible : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<health>().AddHealth(healthValue);
            gameObject.SetActive(false);
            FindObjectOfType<AudioManager>().Play("Heart");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickUp : MonoBehaviour
{
    [SerializeField] private Health player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "Arrow")
        {
            player.TakeHealth(1);
            Destroy(gameObject);
        }
    }
}

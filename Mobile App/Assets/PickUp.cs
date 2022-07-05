using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    [SerializeField] private EdoHealth player;
    [SerializeField] private TextMeshProUGUI counter;
    [SerializeField] private TextMeshProUGUI counter2;

    [SerializeField] private TextMeshProUGUI counter3;
    [SerializeField] private TextMeshProUGUI counter4;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Pickup();
            Destroy(gameObject);
        }
    }

    public void Pickup()
    {
        player.coins = player.coins + 1;
        counter.text = player.coins.ToString();
        counter2.text = player.coins.ToString();

        counter3.text = player.coins.ToString() + " / " + player.totalCoins.ToString();
        counter4.text = player.coins.ToString() + " / " + player.totalCoins.ToString();
    }
}

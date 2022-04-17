using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinPicker : MonoBehaviour
{
    [SerializeField] private OldMoving player;
    [SerializeField] private TextMeshProUGUI counter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.tag == "Arrow")
        {
            Pickup();
            Destroy(gameObject);
        }
    }

    public void Pickup()
    {
        player.coins += 1;
        counter.text = player.coins.ToString();
    }
}
